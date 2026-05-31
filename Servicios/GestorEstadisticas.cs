using System.Text.Json;
using LeeyCopia.Modelo;

namespace LeeyCopia.Servicios
{
    public static class GestorEstadisticas
    {
        /// <summary>Nombre del alumno activo. Se establece al iniciar sesión.</summary>
        public static string AlumnoActual { get; set; } = string.Empty;

        private static readonly JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        private static string RutaArchivo(string nombreAlumno)
        {
            var nombre = SanitizarNombre(nombreAlumno);
            return Path.Combine(Application.StartupPath, "Estadisticas", $"alumno_{nombre}.json");
        }

        private static string SanitizarNombre(string nombre)
        {
            var invalidos = Path.GetInvalidFileNameChars();
            return string.Concat(nombre.Trim().Select(c => invalidos.Contains(c) ? '_' : c)).ToUpper();
        }

        // ── API pública ───────────────────────────────────────────────────────

        public static List<Sesion> CargarSesiones(string nombreAlumno)
        {
            try
            {
                var ruta = RutaArchivo(nombreAlumno);
                if (!File.Exists(ruta))
                    return new List<Sesion>();
                var json = File.ReadAllText(ruta, System.Text.Encoding.UTF8);
                return JsonSerializer.Deserialize<List<Sesion>>(json, opciones) ?? new List<Sesion>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cargar estadísticas: {ex.Message}", ex);
            }
        }

        private static void GuardarSesiones(string nombreAlumno, List<Sesion> sesiones)
        {
            var ruta = RutaArchivo(nombreAlumno);
            var directorio = Path.GetDirectoryName(ruta);
            if (!string.IsNullOrEmpty(directorio))
                Directory.CreateDirectory(directorio);
            var json = JsonSerializer.Serialize(sesiones, opciones);
            File.WriteAllText(ruta, json, System.Text.Encoding.UTF8);
        }

        public static int IniciarSesion(Sesion sesion)
        {
            try
            {
                var sesiones = CargarSesiones(AlumnoActual);
                sesiones.Add(sesion);
                GuardarSesiones(AlumnoActual, sesiones);
                return sesiones.Count - 1;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al iniciar sesión: {ex.Message}", ex);
            }
        }

        public static void GuardarRegistro(int indiceSesion, RegistroSesion registro)
        {
            try
            {
                var sesiones = CargarSesiones(AlumnoActual);
                if (indiceSesion < 0 || indiceSesion >= sesiones.Count) return;
                sesiones[indiceSesion].Frases.Add(registro);
                GuardarSesiones(AlumnoActual, sesiones);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar registro: {ex.Message}", ex);
            }
        }

        public static void CerrarSesion(int indiceSesion, DateTime fechaFin)
        {
            try
            {
                var sesiones = CargarSesiones(AlumnoActual);
                if (indiceSesion < 0 || indiceSesion >= sesiones.Count) return;
                sesiones[indiceSesion].FechaFin = fechaFin;
                GuardarSesiones(AlumnoActual, sesiones);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cerrar sesión: {ex.Message}", ex);
            }
        }

        public static void Limpiar(string nombreAlumno)
        {
            var ruta = RutaArchivo(nombreAlumno);
            if (File.Exists(ruta))
                File.Delete(ruta);
        }
    }
}
