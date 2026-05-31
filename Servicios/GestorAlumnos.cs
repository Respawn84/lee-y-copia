using System.Text.Json;
using LeeyCopia.Modelo;

namespace LeeyCopia.Servicios
{
    public static class GestorAlumnos
    {
        private static readonly string rutaArchivo = Path.Combine(
            Application.StartupPath, "Alumnos", "alumnos.json");

        private static readonly JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static ConfigAlumnos Cargar()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                    return new ConfigAlumnos();
                var json = File.ReadAllText(rutaArchivo, System.Text.Encoding.UTF8);
                return JsonSerializer.Deserialize<ConfigAlumnos>(json, opciones) ?? new ConfigAlumnos();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cargar alumnos: {ex.Message}", ex);
            }
        }

        public static void Guardar(ConfigAlumnos config)
        {
            try
            {
                var directorio = Path.GetDirectoryName(rutaArchivo);
                if (!string.IsNullOrEmpty(directorio))
                    Directory.CreateDirectory(directorio);
                var json = JsonSerializer.Serialize(config, opciones);
                File.WriteAllText(rutaArchivo, json, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar alumnos: {ex.Message}", ex);
            }
        }

        public static List<Alumno> ObtenerTodos() => Cargar().Alumnos;
    }
}
