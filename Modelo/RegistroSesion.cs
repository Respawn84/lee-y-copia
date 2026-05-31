namespace LeeyCopia.Modelo
{
    /// <summary>
    /// Representa el registro estadístico de una frase intentada durante una sesión.
    /// </summary>
    public class RegistroSesion
    {
        /// <summary>Fecha y hora en que se mostró la frase.</summary>
        public DateTime Fecha { get; set; } = DateTime.Now;

        /// <summary>Texto de la frase que se pidió copiar.</summary>
        public string Frase { get; set; } = string.Empty;

        /// <summary>Número de caracteres de la frase (sin espacios extremos).</summary>
        public int LongitudFrase { get; set; }

        /// <summary>Segundos empleados desde que apareció la frase hasta el acierto o salto.</summary>
        public double SegundosEmpleados { get; set; }

        /// <summary>Segundos por carácter (tiempo relativo a la longitud).</summary>
        public double SegundosPorCaracter => LongitudFrase > 0 ? Math.Round(SegundosEmpleados / LongitudFrase, 2) : 0;

        /// <summary>Número de intentos fallidos antes de acertar o saltar.</summary>
        public int Fallos { get; set; }

        /// <summary>Indica si el niño acertó la frase o la saltó sin completarla.</summary>
        public bool Acertada { get; set; }
    }
}
