namespace LeeyCopia.Modelo
{
    public class Alumno
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}
