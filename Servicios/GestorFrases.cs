using System.Text.Json;
using LeeyCopia.Modelo;

namespace LeeyCopia.Servicios
{
    /// <summary>
    /// Servicio para gestionar la persistencia de frases en disco.
    /// </summary>
    public static class GestorFrases
    {
        private static readonly string rutaArchivo = Path.Combine(
            Application.StartupPath,
            "Frases",
            "configFrases.json"
        );

        /// <summary>
        /// Guarda la configuración de frases en disco.
        /// </summary>
        /// <param name="config">Configuración a guardar</param>
        public static void Guardar(ConfigFrases config)
        {
            try
            {
                // Asegurar que el directorio existe
                var directorio = Path.GetDirectoryName(rutaArchivo);
                if (!string.IsNullOrEmpty(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                // Serializar y guardar
                var opciones = new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                
                var json = JsonSerializer.Serialize(config, opciones);
                File.WriteAllText(rutaArchivo, json, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar configFrases: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Carga la configuración de frases desde disco.
        /// Si no existe el archivo, retorna una configuración vacía.
        /// </summary>
        /// <returns>Configuración cargada o vacía</returns>
        public static ConfigFrases Cargar()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return new ConfigFrases();
                }

                var json = File.ReadAllText(rutaArchivo, System.Text.Encoding.UTF8);
                return JsonSerializer.Deserialize<ConfigFrases>(json) ?? new ConfigFrases();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cargar configFrases: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene la ruta donde se guardan las frases (útil para depuración).
        /// </summary>
        /// <returns>Ruta completa del archivo de configuración</returns>
        public static string ObtenerRutaArchivo()
        {
            return rutaArchivo;
        }

        /// <summary>
        /// Obtiene solo las frases activas de la configuración.
        /// </summary>
        /// <returns>Lista de frases activas</returns>
        public static List<Frase> ObtenerFrasesActivas()
        {
            var config = Cargar();
            return config.Frases.Where(f => f.Activa).ToList();
        }
    }
}
