namespace LeeyCopia.Modelo
{
    /// <summary>
    /// Representa una frase que el niño puede practicar.
    /// </summary>
    public class Frase
    {
        /// <summary>
        /// Texto de la frase.
        /// </summary>
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Indica si la frase está activa para mostrarse en el ejercicio.
        /// </summary>
        public bool Activa { get; set; } = true;

        public Frase()
        {
        }

        public Frase(string texto, bool activa = true)
        {
            Texto = texto;
            Activa = activa;
        }
    }
}
