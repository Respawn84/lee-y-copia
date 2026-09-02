#!/usr/bin/env python3
"""
broker.py v1 - Puente serie 286 <-> Claude API

Sustituye el eco de la v0 por conversacion real contra la API de Claude.
Usa la tool de busqueda web nativa de la API para preguntas factuales,
y deja que el modelo charle libremente para el resto.

Requisitos:
    pip3 install pyserial anthropic --break-system-packages

Configuracion:
    Copia api.env.example a api.env y pon tu ANTHROPIC_API_KEY real.
    api.env NUNCA debe compartirse ni subirse a git.

Uso:
    python3 broker_v1.py
"""

import re
import sys
import time
import textwrap
from pathlib import Path

import serial
from anthropic import Anthropic

# --------------------------------------------------------------------------
# Configuracion
# --------------------------------------------------------------------------

PORT = "/dev/ttyUSB0"
BAUDRATE = 9600
BYTESIZE = serial.EIGHTBITS
PARITY = serial.PARITY_NONE
STOPBITS = serial.STOPBITS_ONE
LINE_TERMINATORS = (b"\r", b"\n")

ENV_FILE = Path(__file__).parent / "api.env"

MODEL = "claude-sonnet-4-6"

# Ancho de pantalla DOS tipico (80 columnas). Se deja por debajo del ancho
# util de la ventana de CHAT.EXE (71 columnas) para que el cliente DOS
# nunca tenga que partir una linea por su cuenta ademas del salto de
# linea real: si los dos anchos no coincidian, una misma linea del broker
# se cortaba dos veces en el lado DOS, justo donde mas se perdian bytes.
SCREEN_WIDTH = 70
# Lineas por "pagina" antes de parar y esperar ENTER (25 filas menos margen)
LINES_PER_PAGE = 20

# Retardo entre bytes al escribir por el puerto serie (segundos).
# A 9600 baudios un byte tarda ~1.04 ms en el cable, pero algunos
# adaptadores USB-serie entregan los primeros bytes de cada escritura en
# rafaga (por el buffering interno / polling USB de 1 ms) en vez de
# repartirlos al ritmo exacto del baudrate, especialmente justo al
# empezar una transmision nueva tras un rato inactivo. La UART del 286
# no tiene FIFO: un byte no leido a tiempo se pierde sin mas. Forzando
# aqui un ritmo de escritura mas lento que el baudrate real nos aseguramos
# de que el cable nunca vaya mas rapido de lo que el 286 puede leer,
# pase lo que pase con el adaptador.
CHAR_DELAY = 0.003

# Retardo extra (segundos) tras el CRLF de cada linea, ademas del
# CHAR_DELAY normal entre bytes. Al recibir el '\n' final de una linea,
# CHAT.EXE hace scroll de su ventana (mueve la VRAM una fila hacia
# arriba) antes de poder leer el siguiente byte: es trabajo real que en
# el peor caso puede tardar mas que un CHAR_DELAY suelto, y coincide con
# el primer caracter de la siguiente linea. Este margen extra solo se
# paga una vez por linea (no por caracter), asi que apenas ralentiza el
# conjunto.
LINE_DELAY = 0.015

SYSTEM_PROMPT = """Estas hablando por un puente serie con un ordenador
Intel 286 de 1990 corriendo DOS, conectado por RS232 a 9600 baudios.
La persona al otro lado escribe en un programa de terminal (Telix).

Reglas de estilo, MUY IMPORTANTES:
- Responde en texto plano, SIN markdown (nada de **negrita**, `codigo`,
  # titulos, listas con guiones ni tablas). La pantalla es de texto DOS.
- Se conciso. Frases cortas. Evita parrafos larguisimos.
- Puedes charlar libremente en temas ligeros, opiniones, humor, saludos.
- Para CUALQUIER dato factual verificable (fechas, cifras, nombres,
  eventos historicos, noticias, tiempo, resultados deportivos...) usa
  SIEMPRE la herramienta de busqueda web antes de responder, aunque
  creas saber la respuesta. No respondas de memoria en esos casos.
- No expliques que has buscado, simplemente da la respuesta con
  naturalidad.
"""


def load_api_key() -> str:
    if not ENV_FILE.exists():
        print(f"[broker v1] ERROR: no encuentro {ENV_FILE}", file=sys.stderr)
        print("[broker v1] Copia api.env.example a api.env y pon tu clave.", file=sys.stderr)
        sys.exit(1)

    for line in ENV_FILE.read_text().splitlines():
        line = line.strip()
        if not line or line.startswith("#"):
            continue
        if "=" in line:
            key, _, value = line.partition("=")
            if key.strip() == "ANTHROPIC_API_KEY":
                value = value.strip()
                if not value or "tu-clave-aqui" in value:
                    print("[broker v1] ERROR: rellena tu clave real en api.env", file=sys.stderr)
                    sys.exit(1)
                return value

    print("[broker v1] ERROR: ANTHROPIC_API_KEY no encontrada en api.env", file=sys.stderr)
    sys.exit(1)


def open_port() -> serial.Serial:
    return serial.Serial(
        port=PORT,
        baudrate=BAUDRATE,
        bytesize=BYTESIZE,
        parity=PARITY,
        stopbits=STOPBITS,
        timeout=1,
        write_timeout=2,
    )


# Secuencias de escape ANSI/VT100 tipicas: ESC seguido de '[' o ']' y una
# cadena de parametros terminada en una letra (CSI) o en BEL/ST (OSC).
_ANSI_ESCAPE_RE = re.compile(
    rb"""
    \x1b            # ESC
    (?:
        \[ [0-?]* [ -/]* [@-~]     # secuencia CSI: ESC [ ... letra final
        |
        \] .*? (?:\x07|\x1b\\)     # secuencia OSC: ESC ] ... BEL o ESC\
        |
        [@-Z\\-_]                  # secuencias cortas de 2 bytes (ESC + letra)
    )
    """,
    re.VERBOSE,
)


def sanitize_bytes(raw: bytes) -> bytes:
    """
    Quita secuencias de escape ANSI/VT100 y bytes de control sueltos
    (todo lo que no sea texto imprimible en CP437 o CR/LF/TAB) antes
    de decodificar, para que nada de "ruido" de Telix llegue a la API.
    """
    raw = _ANSI_ESCAPE_RE.sub(b"", raw)
    # Filtra bytes de control salvo CR (0x0D), LF (0x0A) y TAB (0x09).
    # Deja pasar todo el rango imprimible CP437, incluida la zona alta
    # (0x80-0xFF) donde viven acentos y caracteres extendidos.
    cleaned = bytes(
        b for b in raw
        if b in (0x09, 0x0A, 0x0D) or 0x20 <= b <= 0xFF
    )
    return cleaned


def read_line(ser: serial.Serial) -> bytes:
    """Lee bytes hasta CR/LF. Devuelve b'' si no hay nada tras el timeout."""
    buf = bytearray()
    while True:
        b = ser.read(1)
        if not b:
            return bytes(buf) if buf else b""
        if b in LINE_TERMINATORS:
            if buf:
                return bytes(buf)
            continue
        buf += b


def write_paced(ser: serial.Serial, data: bytes) -> None:
    """
    Escribe 'data' byte a byte con CHAR_DELAY entre cada uno, en vez de un
    unico ser.write() con todo el bloque. Un ser.write() grande deja que
    el adaptador USB-serie decida como repartir los bytes en el tiempo, y
    varios adaptadores sueltan los primeros bytes de cada escritura en
    rafaga en vez de respetar el baudrate configurado. Pausando aqui nos
    aseguramos de que cada byte sale al cable con margen de sobra para que
    la UART sin FIFO del 286 lo pueda leer antes de que llegue el siguiente.
    """
    for b in data:
        ser.write(bytes((b,)))
        time.sleep(CHAR_DELAY)


def send_text(ser: serial.Serial, text: str) -> None:
    """Manda texto codificado en CP437, con CRLF, sin paginar (lineas sueltas)."""
    for line in text.splitlines():
        write_paced(ser, line.encode("cp437", errors="replace") + b"\r\n")
        time.sleep(LINE_DELAY)


def send_paginated(ser: serial.Serial, text: str) -> None:
    """
    Envia texto largo troceado a SCREEN_WIDTH columnas, en bloques de
    LINES_PER_PAGE, esperando ENTER desde la 286 entre bloque y bloque
    (estilo 'more' de BBS).
    """
    wrapped_lines = []
    for paragraph in text.split("\n"):
        if not paragraph.strip():
            wrapped_lines.append("")
            continue
        wrapped_lines.extend(textwrap.wrap(paragraph, width=SCREEN_WIDTH) or [""])

    for i in range(0, len(wrapped_lines), LINES_PER_PAGE):
        chunk = wrapped_lines[i:i + LINES_PER_PAGE]
        for line in chunk:
            write_paced(ser, line.encode("cp437", errors="replace") + b"\r\n")
            time.sleep(LINE_DELAY)

        is_last_chunk = (i + LINES_PER_PAGE) >= len(wrapped_lines)
        if not is_last_chunk:
            write_paced(ser, b"-- MAS (pulsa ENTER) --\r\n")
            time.sleep(LINE_DELAY)
            # Bloqueante: esperamos a que la 286 mande una linea (aunque este vacia)
            while not read_line(ser):
                pass


def main():
    api_key = load_api_key()
    client = Anthropic(api_key=api_key)

    print(f"[broker v1] Abriendo {PORT} a {BAUDRATE} baudios 8N1...")
    try:
        ser = open_port()
    except serial.SerialException as e:
        print(f"[broker v1] ERROR abriendo el puerto: {e}", file=sys.stderr)
        sys.exit(1)

    print("[broker v1] Puerto abierto. Conversacion activa (Ctrl+C para salir)...")
    send_text(ser, "Broker conectado. Escribe y pulsa ENTER.")

    conversation = []  # historial de la sesion: [{"role": ..., "content": ...}, ...]

    try:
        while True:
            line_bytes = read_line(ser)
            if not line_bytes:
                continue

            clean_bytes = sanitize_bytes(line_bytes)
            if clean_bytes != line_bytes:
                print(f"[broker v1] (filtrado ruido/ANSI: {line_bytes!r} -> {clean_bytes!r})")

            user_text = clean_bytes.decode("cp437", errors="replace").strip()
            if not user_text:
                # Probablemente era pura secuencia ANSI/ruido; la ignoramos
                # en vez de mandar un mensaje vacio a la API.
                continue

            ts = time.strftime("%H:%M:%S")
            print(f"[{ts}] 286 dice: {user_text!r}")

            conversation.append({"role": "user", "content": user_text})

            try:
                response = client.messages.create(
                    model=MODEL,
                    max_tokens=1024,
                    system=SYSTEM_PROMPT,
                    messages=conversation,
                    tools=[{"type": "web_search_20250305", "name": "web_search"}],
                )
            except Exception as e:
                print(f"[broker v1] ERROR llamando a la API: {e}", file=sys.stderr)
                send_text(ser, "ERROR: fallo al consultar la IA. Intenta de nuevo.")
                conversation.pop()  # no dejamos el turno fallido en el historial
                continue

            # El texto final puede venir repartido en varios bloques de tipo "text"
            # (los bloques tool_use/tool_result no llevan texto legible directamente).
            reply_text = "".join(
                block.text for block in response.content if block.type == "text"
            ).strip()

            if not reply_text:
                reply_text = "(sin respuesta de texto)"

            print(f"[{ts}] Claude responde: {reply_text[:200]}...")
            conversation.append({"role": "assistant", "content": reply_text})

            send_paginated(ser, reply_text)

            # Limitar historial para no disparar coste/latencia indefinidamente
            if len(conversation) > 20:
                conversation = conversation[-20:]

    except KeyboardInterrupt:
        print("\n[broker v1] Cerrando por Ctrl+C.")
    finally:
        ser.close()


if __name__ == "__main__":
    main()
