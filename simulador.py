#!/usr/bin/env python3
"""
simulador.py -- Prueba el menu del broker sin el 286 ni el cable.

Sustituye el puerto serie por la consola: lo que el broker mandaria al
286 se imprime aqui, y lo que escribas se le entrega como si viniera
del 286 (terminado en CR LF, igual que hace CHAT.EXE). Los retardos de
escritura se ponen a cero para no esperar a 9600 baudios de mentira.

    python3 simulador.py

Si no hay api.env, las opciones que usan la IA avisan del fallo pero el
resto del menu y las aplicaciones locales funcionan igual.
"""

import sys

import config
import ia

config.CHAR_DELAY = 0.0
config.LINE_DELAY = 0.0


class PuertoFalso:
    """Imita lo justo de serial.Serial que usa Terminal."""

    def __init__(self):
        self.buf = bytearray()

    def read(self, n=1):
        while not self.buf:
            sys.stdout.flush()
            try:
                linea = input()
            except EOFError:
                # Sin mas entrada: en el cable real no llegar nada solo
                # significa esperar, pero aqui es el final de la prueba.
                raise KeyboardInterrupt
            self.buf.extend(linea.encode("cp437", errors="replace") + b"\r\n")
        salida = bytes(self.buf[:n])
        del self.buf[:n]
        return salida

    def write(self, data):
        sys.stdout.write(data.decode("cp437", errors="replace"))
        return len(data)

    def reset_input_buffer(self):
        self.buf.clear()

    def close(self):
        pass


def main():
    import broker_v2

    broker_v2.open_port = PuertoFalso

    if not config.ENV_FILE.exists():
        print("[simulador] Sin api.env: las opciones con IA daran error.\n")

        def _sin_ia(*args, **kwargs):
            raise ia.IAError("simulador sin api.env")

        ia.init = lambda: None
        ia.consulta = _sin_ia
        ia.chat = _sin_ia

    broker_v2.main()


if __name__ == "__main__":
    main()
