# 📚 Lee y Copia

**Herramienta educativa de apoyo a la lectoescritura con síntesis de voz**

Una aplicación Windows diseñada específicamente para niños con TDAH y dentro del espectro autista que están aprendiendo a unir sílabas en palabras completas.

---

## 🎯 Propósito

**Lee y Copia** surge de una necesidad real: ayudar a niños que, aunque han asumido las sílabas, tienen dificultades para integrarlas en palabras completas. La aplicación aprovecha dos elementos clave:

1. **Motivación visual**: Los niños aprenden mejor copiando palabras que intentando leerlas directamente
2. **Refuerzo auditivo**: La síntesis de voz (TTS) les permite asociar texto escrito con sonido

---

## 📖 ¿Por qué funciona este enfoque?

Este programa aplica principios científicos de neurociencia educativa, teoría de la carga cognitiva y aprendizaje multimodal. **Si quieres entender los fundamentos pedagógicos** detrás del diseño:

### 👉 **[Lee el Fundamento Pedagógico completo](FUNDAMENTO_PEDAGOGICO.md)**

Incluye:
- Teorías científicas (Vygotsky, Sweller, Paivio, Ericsson)
- Por qué "copiar" funciona mejor que "leer" en esta fase
- Aplicabilidad específica en TDAH y TEA
- Estudios de neuroimagen sobre aprendizaje multimodal
- Guías prácticas por fases de aprendizaje
- Referencias bibliográficas completas

**Resumen en 3 líneas:**  
El cerebro aprende mejor cuando múltiples canales (visual + motor + auditivo) trabajan juntos. Copiar reduce la carga cognitiva y permite integrar sílabas en palabras completas como "unidades gestálticas". El feedback inmediato refuerza el aprendizaje sin frustración.

---

## ✨ Características

### Para el niño:
- ✅ **Interfaz simple y clara**: Sin distracciones, enfocada en la tarea
- 🔊 **Síntesis de voz**: Escucha la frase modelo y su propia escritura
- 🎨 **Retroalimentación visual inmediata**: Verde ✅ cuando acierta, rojo ❌ cuando se equivoca
- 🎤 **Refuerzo auditivo positivo**: "¡Muy bien!" al acertar, "Inténtalo de nuevo" al fallar
- 🔄 **Flujo continuo**: Avance automático a la siguiente frase tras cada acierto
- 🎮 **Frases temáticas**: Posibilidad de usar vocabulario motivador (Minecraft, etc.)

### Para padres/educadores:
- 📝 **Gestión de frases**: Añade, edita y activa/desactiva frases fácilmente
- 💾 **Portable**: No requiere instalación, se ejecuta desde cualquier carpeta
- 🎚️ **Progresión controlada**: Activa solo las frases apropiadas para el nivel actual
- 📊 **Frases graduadas**: De 3-5 palabras para lectura inicial

## 🖥️ Capturas de pantalla

### Ventana principal
La interfaz de práctica donde el niño copia las frases:

```
┌─────────────────────────────────────────────┐
│  Copia la Siguiente Frase:                  │
│  ┌─────────────────────────────────────┐   │
│  │  MI MAMA ME MIMA                    │   │
│  └─────────────────────────────────────┘   │
│  [🔊 Lee]  [⏭️ Siguiente]                   │
│                                             │
│  Escribe la frase aquí:                     │
│  ┌─────────────────────────────────────┐   │
│  │  MI MAMA ME MIMA_                   │   │
│  └─────────────────────────────────────┘   │
│  [🔊 Lee]  [✓ Comprobar]                    │
└─────────────────────────────────────────────┘
```

### Ventana de configuración
Gestión sencilla del banco de frases:

```
┌─────────────────────────────────────────────┐
│  [Nueva frase aquí...]  [Añadir] [Borrar]  │
│                                             │
│  ☑ MI MAMA ME MIMA                         │
│  ☑ EL PATO NADA                            │
│  ☑ LA LUNA BRILLA                          │
│  ☑ CAVO EN LA MINA                         │
│  ☐ EL DRAGON DEL END (desactivada)         │
│                                             │
│                        [Guardar]            │
└─────────────────────────────────────────────┘
```

## 🚀 Requisitos

- **Sistema Operativo**: Windows 10/11
- **.NET Runtime**: .NET 6.0 o superior
- **Síntesis de voz**: Windows incluye voces en español por defecto

## 📦 Instalación

### Opción 1: Desde releases (recomendado)
1. Descarga la última versión desde [Releases](../../releases)
2. Descomprime el archivo ZIP en cualquier carpeta
3. Ejecuta `LeeyCopia.exe`

### Opción 2: Compilar desde código
```bash
git clone https://github.com/Respawn84/lee-y-copia.git
cd lee-y-copia
dotnet build -c Release
```

El ejecutable estará en `bin/Release/net8.0-windows/`

## 📖 Uso

### Primera vez
1. Ejecuta el programa
2. Ve a **Configuración > Frases**
3. Añade frases apropiadas para el nivel del niño (3-5 palabras recomendadas)
4. Marca las casillas de las frases que quieres activar
5. Haz clic en **Guardar**

### Durante la práctica
1. El programa muestra una frase aleatoria de las activas
2. El niño la copia en el cuadro inferior
3. Puede usar los botones **🔊 Lee** para escuchar la frase
4. Al terminar, pulsa **✓ Comprobar**
5. Si acierta → feedback positivo y avance automático
6. Si falla → puede intentarlo de nuevo

### Consejos pedagógicos
- **Empieza con pocas frases** (4-6) para no abrumar
- **Usa vocabulario motivador**: Personajes, juegos o intereses del niño
- **Progresión gradual**: 
  - Semana 1-2: Frases de 3 palabras
  - Semana 3-4: Frases de 4 palabras
  - Semana 5+: Frases de 5 palabras
- **Sesiones cortas**: 10-15 minutos máximo para mantener la atención
- **Refuerzo positivo**: Celebra los aciertos más allá del programa

## 🛠️ Configuración

### Archivo de frases
Las frases se guardan en: `[carpeta del programa]/Frases/configFrases.json`

Ejemplo de estructura:
```json
{
  "Frases": [
    {
      "Texto": "MI MAMA ME MIMA",
      "Activa": true
    },
    {
      "Texto": "EL GATO DUERME",
      "Activa": true
    },
    {
      "Texto": "CAVO EN LA MINA",
      "Activa": false
    }
  ]
}
```

### Velocidad de lectura
La velocidad de la voz está configurada como `-3` (más lenta) para facilitar la comprensión. Puedes ajustarla editando `Form1.cs`:

```csharp
synth.Rate = -3;  // Valores: -10 (muy lento) a 10 (muy rápido)
```

## 🤝 Contribuciones

Este es un proyecto educativo de código abierto. Las contribuciones son bienvenidas:

- 📝 Sugerencias de mejora
- 🐛 Reportes de errores
- 🎨 Mejoras de interfaz
- 📚 Bancos de frases temáticas
- 🌍 Traducciones a otros idiomas

## 📄 Licencia

Este proyecto está bajo licencia [MIT](LICENSE). Libre para usar, modificar y distribuir.

## 👨‍💻 Autor

Desarrollado por Daniel ([@Respawn84](https://github.com/Respawn84)) como herramienta de apoyo educativo.

---

## 💡 Inspiración y contexto

Este programa nació de la observación directa de un niño con TDAH dentro del espectro autista que había asumido las sílabas pero no lograba integrarlas en palabras completas. La observación mostró que **copiar palabras** ayudaba más que intentar leerlas directamente.

Combinando experiencia previa con tecnologías de asistencia en .NET/WinForms y aprovechando elementos motivadores (Minecraft, referencias visuales), se desarrolló esta herramienta en pocas horas.

**Si ayuda a un niño, el proyecto ya cumplió su propósito. Si ayuda a más, mejor.** 🌟

---

### 🔗 Enlaces útiles

- **[📖 Fundamento Pedagógico](FUNDAMENTO_PEDAGOGICO.md)** - Explicación científica completa
- [Issues](../../issues) - Reporta problemas o sugiere mejoras
- [Discussions](../../discussions) - Comparte experiencias y resultados
- [Releases](../../releases) - Descarga la última versión

### 🙏 Agradecimientos

A todos los padres, educadores y terapeutas que trabajan día a día con niños neurodivergentes. Este proyecto es para ustedes y para ellos.
