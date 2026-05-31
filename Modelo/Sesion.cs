namespace LeeyCopia.Modelo
{
    /// <summary>
    /// Representa una sesión de trabajo completa con su lista de frases practicadas.
    /// </summary>
    public class Sesion
    {
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        /// <summary>Null si la sesión fue interrumpida sin cerrar correctamente.</summary>
        public DateTime? FechaFin { get; set; }

        public List<RegistroSesion> Frases { get; set; } = new List<RegistroSesion>();

        /// <summary>Duración total de la sesión. Null si no tiene FechaFin.</summary>
        public TimeSpan? Duracion => FechaFin.HasValue ? FechaFin.Value - FechaInicio : null;
    }
}
