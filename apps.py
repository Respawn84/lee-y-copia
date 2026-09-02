#!/usr/bin/env python3
"""
apps.py -- Aplicaciones locales que se ejecutan en la Raspberry y
devuelven el resultado al 286.

Cada aplicacion es una funcion que recibe el Terminal y hace lo suyo
de forma bloqueante (term.ask / term.wait_line para pedir datos,
term.print / term.page para responder). Cuando la funcion vuelve, el
broker repinta el menu de aplicaciones.

Para anadir una aplicacion nueva basta con escribir la funcion y
meterla en la lista APPS del final del fichero.
"""

import ast
import math
import operator
import os
import random
import shutil
import socket
import time

import config
import ia
import prompts


# ==========================================================================
# 1. Reloj y fecha - sincronizar el 286
# ==========================================================================

DIAS = ["lunes", "martes", "miercoles", "jueves", "viernes", "sabado", "domingo"]
MESES = ["enero", "febrero", "marzo", "abril", "mayo", "junio", "julio",
         "agosto", "septiembre", "octubre", "noviembre", "diciembre"]


def app_reloj(term):
    """
    El 286 pierde la hora en cuanto se le acaba la pila de la BIOS.
    Aqui le damos la hora buena de la Raspberry ya escrita en el
    formato que espera DOS, para poder teclearla tal cual.
    """
    ahora = time.localtime()

    term.titulo("Reloj de la Raspberry")
    # Nombres propios en vez de %A/%B: el locale de la Raspberry suele
    # estar en ingles y no queremos depender de como este configurada.
    term.print(f"Fecha: {DIAS[ahora.tm_wday]} {ahora.tm_mday} de "
               f"{MESES[ahora.tm_mon - 1]} de {ahora.tm_year}")
    term.print(time.strftime("Hora:  %H:%M:%S", ahora))
    term.print("")
    term.print("Para poner en hora el 286, sal al DOS y teclea:")
    term.print("")
    term.print(time.strftime("   DATE %d-%m-%Y", ahora))
    term.print(time.strftime("   TIME %H:%M:%S", ahora))
    term.print("")
    term.print("(Si tu DOS esta en formato americano, la fecha es")
    term.print(time.strftime("   DATE %m-%d-%Y", ahora) + " )")
    term.pausa()


# ==========================================================================
# 2. Calculadora
# ==========================================================================

_OPS_BIN = {
    ast.Add: operator.add,
    ast.Sub: operator.sub,
    ast.Mult: operator.mul,
    ast.Div: operator.truediv,
    ast.FloorDiv: operator.floordiv,
    ast.Mod: operator.mod,
    ast.Pow: operator.pow,
}
_OPS_UN = {ast.UAdd: operator.pos, ast.USub: operator.neg}

_FUNCS = {
    "sqrt": math.sqrt, "raiz": math.sqrt,
    "sin": math.sin, "cos": math.cos, "tan": math.tan,
    "asin": math.asin, "acos": math.acos, "atan": math.atan,
    "log": math.log, "log10": math.log10, "exp": math.exp,
    "abs": abs, "round": round, "int": int,
    "rad": math.radians, "grad": math.degrees,
}
_CONSTS = {"pi": math.pi, "e": math.e}


def _evaluar(nodo):
    """Evalua un arbol AST permitiendo solo aritmetica y funciones de _FUNCS."""
    if isinstance(nodo, ast.Expression):
        return _evaluar(nodo.body)
    if isinstance(nodo, ast.Constant):
        if isinstance(nodo.value, (int, float)):
            return nodo.value
        raise ValueError("solo numeros")
    if isinstance(nodo, ast.BinOp) and type(nodo.op) in _OPS_BIN:
        return _OPS_BIN[type(nodo.op)](_evaluar(nodo.left), _evaluar(nodo.right))
    if isinstance(nodo, ast.UnaryOp) and type(nodo.op) in _OPS_UN:
        return _OPS_UN[type(nodo.op)](_evaluar(nodo.operand))
    if isinstance(nodo, ast.Name) and nodo.id in _CONSTS:
        return _CONSTS[nodo.id]
    if isinstance(nodo, ast.Call) and isinstance(nodo.func, ast.Name):
        if nodo.func.id in _FUNCS and not nodo.keywords:
            return _FUNCS[nodo.func.id](*[_evaluar(a) for a in nodo.args])
    raise ValueError("expresion no permitida")


def calcular(expresion: str):
    """Evalua una expresion aritmetica de forma segura (sin eval())."""
    expresion = expresion.replace(",", ".").replace("^", "**")
    arbol = ast.parse(expresion, mode="eval")
    return _evaluar(arbol)


def app_calculadora(term):
    term.titulo("Calculadora")
    term.print("Escribe una operacion y pulsa ENTER. 0 para salir.")
    term.print("Admite + - * / % ^ parentesis y sqrt cos sin log pi e")

    while True:
        expr = term.ask("")
        if expr == "0" or expr.upper() in ("SALIR", "FIN"):
            return
        if not expr:
            continue
        try:
            resultado = calcular(expr)
        except ZeroDivisionError:
            term.print("Error: division por cero.")
            continue
        except Exception:
            term.print("Error: expresion no valida.")
            continue

        if isinstance(resultado, float):
            texto = f"{resultado:.10g}"
        else:
            texto = str(resultado)
        term.print(f"= {texto}")


# ==========================================================================
# 3. Conversor de unidades
# ==========================================================================

_CONVERSIONES = [
    ("1", "Celsius -> Fahrenheit", lambda v: (v * 9 / 5 + 32, "F")),
    ("2", "Fahrenheit -> Celsius", lambda v: ((v - 32) * 5 / 9, "C")),
    ("3", "Kilometros -> Millas", lambda v: (v * 0.621371, "millas")),
    ("4", "Millas -> Kilometros", lambda v: (v / 0.621371, "km")),
    ("5", "Kilogramos -> Libras", lambda v: (v * 2.20462, "lb")),
    ("6", "Libras -> Kilogramos", lambda v: (v / 2.20462, "kg")),
    ("7", "Metros -> Pies", lambda v: (v * 3.28084, "pies")),
    ("8", "Pies -> Metros", lambda v: (v / 3.28084, "m")),
    ("9", "Pulgadas -> Centimetros", lambda v: (v * 2.54, "cm")),
    ("A", "Litros -> Galones (US)", lambda v: (v * 0.264172, "gal")),
    ("B", "Bytes -> KB / MB", None),
]


def app_conversor(term):
    opciones = [(c, n) for c, n, _ in _CONVERSIONES] + [("0", "Volver")]
    while True:
        sel = term.menu("CONVERSOR DE UNIDADES", opciones)
        if sel == "0":
            return

        entrada = term.ask("Valor a convertir:")
        if entrada == "0" or not entrada:
            continue
        try:
            valor = float(entrada.replace(",", "."))
        except ValueError:
            term.print("Eso no es un numero.")
            continue

        if sel == "B":
            term.print(f"{valor:.0f} bytes = {valor/1024:.3f} KB = "
                       f"{valor/1048576:.6f} MB")
            continue

        funcion = next(f for c, _, f in _CONVERSIONES if c == sel)
        resultado, unidad = funcion(valor)
        term.print(f"= {resultado:.4f} {unidad}")


# ==========================================================================
# 4. Bloc de notas en la Raspberry
# ==========================================================================

def _leer_notas():
    if not config.NOTAS_FILE.exists():
        return []
    return [l for l in config.NOTAS_FILE.read_text(encoding="utf-8").splitlines() if l.strip()]


def _guardar_notas(notas):
    config.NOTAS_FILE.write_text("\n".join(notas) + "\n", encoding="utf-8")


def app_notas(term):
    """Notas persistentes en la Raspberry: sobreviven al apagar el 286."""
    while True:
        sel = term.menu("BLOC DE NOTAS (en la Raspberry)", [
            ("1", "Ver notas"),
            ("2", "Anadir nota"),
            ("3", "Borrar nota"),
            ("0", "Volver"),
        ])

        if sel == "0":
            return

        notas = _leer_notas()

        if sel == "1":
            if not notas:
                term.print("No hay ninguna nota guardada.")
                continue
            term.page("\n".join(f"{i+1}) {n}" for i, n in enumerate(notas)))

        elif sel == "2":
            if len(notas) >= config.MAX_NOTAS:
                term.print(f"Limite de {config.MAX_NOTAS} notas alcanzado.")
                continue
            texto = term.ask("Escribe la nota (ENTER vacio para cancelar):")
            if not texto:
                continue
            marca = time.strftime("%d/%m/%Y %H:%M")
            notas.append(f"[{marca}] {texto}")
            _guardar_notas(notas)
            term.print("Nota guardada.")

        elif sel == "3":
            if not notas:
                term.print("No hay ninguna nota que borrar.")
                continue
            term.page("\n".join(f"{i+1}) {n}" for i, n in enumerate(notas)))
            num = term.ask("Numero de nota a borrar (0 = cancelar):")
            if not num.isdigit() or num == "0":
                continue
            idx = int(num) - 1
            if 0 <= idx < len(notas):
                borrada = notas.pop(idx)
                _guardar_notas(notas)
                term.print(f"Borrada: {borrada[:50]}")
            else:
                term.print("Ese numero no existe.")


# ==========================================================================
# 5. Estado de la Raspberry
# ==========================================================================

def _uptime() -> str:
    try:
        with open("/proc/uptime") as f:
            segundos = float(f.read().split()[0])
    except OSError:
        return "desconocido"
    dias, resto = divmod(int(segundos), 86400)
    horas, resto = divmod(resto, 3600)
    minutos = resto // 60
    return f"{dias}d {horas}h {minutos}m"


def _temperatura() -> str:
    ruta = "/sys/class/thermal/thermal_zone0/temp"
    try:
        with open(ruta) as f:
            return f"{int(f.read().strip()) / 1000:.1f} C"
    except (OSError, ValueError):
        return "n/d"


def _memoria() -> str:
    try:
        datos = {}
        with open("/proc/meminfo") as f:
            for linea in f:
                clave, _, valor = linea.partition(":")
                datos[clave] = int(valor.split()[0])
        total = datos["MemTotal"] / 1024
        libre = datos.get("MemAvailable", datos["MemFree"]) / 1024
        return f"{total - libre:.0f} MB usados de {total:.0f} MB"
    except (OSError, KeyError, ValueError):
        return "n/d"


def _ip() -> str:
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        # No manda nada: solo obliga al kernel a elegir la interfaz de salida.
        s.connect(("8.8.8.8", 80))
        return s.getsockname()[0]
    except OSError:
        return "sin red"
    finally:
        s.close()


def app_sistema(term):
    uso = shutil.disk_usage("/")

    term.titulo("Estado de la Raspberry")
    term.print(f"Host:     {socket.gethostname()}")
    term.print(f"IP:       {_ip()}")
    term.print(f"Encendida:{_uptime()}")
    term.print(f"CPU temp: {_temperatura()}")
    term.print(f"Memoria:  {_memoria()}")
    term.print(f"Disco:    {uso.used // 2**30} GB usados de {uso.total // 2**30} GB")
    term.print(f"Carga:    {', '.join(f'{c:.2f}' for c in os.getloadavg())}")
    term.print(f"Puerto:   {config.PORT} a {config.BAUDRATE} baudios")
    term.pausa()


# ==========================================================================
# 6. Adivina el numero
# ==========================================================================

def app_adivina(term):
    secreto = random.randint(1, 100)
    intentos = 7

    term.titulo("Adivina el numero")
    term.print(f"Pienso un numero del 1 al 100. Tienes {intentos} intentos.")
    term.print("Escribe 0 para rendirte.")

    for restantes in range(intentos, 0, -1):
        entrada = term.ask(f"Intento ({restantes} restantes):")
        if entrada == "0":
            term.print(f"Te rindes. Era el {secreto}.")
            term.pausa()
            return
        if not entrada.isdigit():
            term.print("Escribe un numero del 1 al 100.")
            continue

        numero = int(entrada)
        if numero == secreto:
            term.print(f"ACERTASTE! Era el {secreto}.")
            term.pausa()
            return
        term.print("Mi numero es MAYOR." if numero > secreto else "Mi numero es MENOR.")

    term.print(f"Se acabaron los intentos. Era el {secreto}.")
    term.pausa()


# ==========================================================================
# 7. Efemerides de hoy (usa la IA)
# ==========================================================================

def app_efemerides(term):
    term.aviso("Buscando efemerides, espera unos segundos...")
    try:
        texto = ia.consulta(prompts.efemerides())
    except ia.IAError:
        term.print("ERROR: no he podido consultar la IA. Intentalo de nuevo.")
        term.pausa()
        return

    term.titulo(f"Un dia como hoy, {time.strftime('%d/%m')}")
    term.page(texto)
    term.pausa()


# ==========================================================================
# Registro de aplicaciones
# ==========================================================================

APPS = [
    ("1", "Reloj y fecha (poner en hora el 286)", app_reloj),
    ("2", "Calculadora", app_calculadora),
    ("3", "Conversor de unidades", app_conversor),
    ("4", "Bloc de notas (guardado en la Pi)", app_notas),
    ("5", "Estado de la Raspberry", app_sistema),
    ("6", "Adivina el numero", app_adivina),
    ("7", "Efemerides de hoy", app_efemerides),
]
