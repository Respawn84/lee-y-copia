namespace LeeyCopia.Modelo
{
    /// <summary>
    /// Configuración completa del programa, conteniendo todas las frases.
    /// </summary>
    public class ConfigFrases
    {
        /// <summary>
        /// Lista de todas las frases disponibles.
        /// </summary>
        public List<Frase> Frases { get; set; } = new List<Frase>();

        public ConfigFrases()
        {
        }
    }
}
