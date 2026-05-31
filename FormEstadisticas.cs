using LeeyCopia.Modelo;
using LeeyCopia.Servicios;

namespace LeeyCopia
{
    public partial class FormEstadisticas : Form
    {
        private List<Sesion> sesiones = new List<Sesion>();
        private List<Alumno> alumnos = new List<Alumno>();

        public FormEstadisticas()
        {
            InitializeComponent();
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            alumnos = GestorAlumnos.ObtenerTodos();

            cmbAlumnos.Items.Clear();
            foreach (var a in alumnos)
                cmbAlumnos.Items.Add(a.Nombre);

            // Pre-seleccionar el alumno activo si existe
            if (!string.IsNullOrEmpty(GestorEstadisticas.AlumnoActual))
            {
                int idx = cmbAlumnos.FindStringExact(GestorEstadisticas.AlumnoActual);
                if (idx >= 0)
                    cmbAlumnos.SelectedIndex = idx;
            }
            else if (cmbAlumnos.Items.Count > 0)
            {
                cmbAlumnos.SelectedIndex = 0;
            }
        }

        private void cmbAlumnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlumnos.SelectedIndex < 0) return;
            CargarEstadisticas(alumnos[cmbAlumnos.SelectedIndex].Nombre);
        }

        private void CargarEstadisticas(string nombreAlumno)
        {
            listViewSesiones.Items.Clear();
            listViewFrases.Items.Clear();
            lblResumen.Text = string.Empty;

            try
            {
                sesiones = GestorEstadisticas.CargarSesiones(nombreAlumno);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var s in sesiones.OrderByDescending(s => s.FechaInicio))
            {
                string duracion = s.Duracion.HasValue
                    ? $"{(int)s.Duracion.Value.TotalMinutes}m {s.Duracion.Value.Seconds:D2}s"
                    : "Interrumpida";

                int acertadas = s.Frases.Count(f => f.Acertada);
                int total = s.Frases.Count;
                string pct = total > 0 ? $"{Math.Round((double)acertadas / total * 100, 0)}%" : "-";

                var item = new ListViewItem(s.FechaInicio.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(duracion);
                item.SubItems.Add(total.ToString());
                item.SubItems.Add($"{acertadas} ({pct})");
                item.SubItems.Add(s.Frases.Sum(f => f.Fallos).ToString());
                item.Tag = s;
                if (!s.FechaFin.HasValue)
                    item.ForeColor = Color.Gray;
                listViewSesiones.Items.Add(item);
            }
        }

        private void listViewSesiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewFrases.Items.Clear();
            lblResumen.Text = string.Empty;

            if (listViewSesiones.SelectedItems.Count == 0) return;

            var sesion = (Sesion)listViewSesiones.SelectedItems[0].Tag;

            foreach (var r in sesion.Frases)
            {
                var item = new ListViewItem(r.Fecha.ToString("HH:mm:ss"));
                item.SubItems.Add(r.Frase);
                item.SubItems.Add(r.LongitudFrase.ToString());
                item.SubItems.Add(r.SegundosEmpleados.ToString("0.0"));
                item.SubItems.Add(r.SegundosPorCaracter.ToString("0.00"));
                item.SubItems.Add(r.Fallos.ToString());
                item.SubItems.Add(r.Acertada ? "✓ Acertada" : "✗ Saltada");
                item.ForeColor = r.Acertada ? Color.DarkGreen : Color.Firebrick;
                listViewFrases.Items.Add(item);
            }

            ActualizarResumen(sesion);
        }

        private void ActualizarResumen(Sesion sesion)
        {
            if (sesion.Frases.Count == 0)
            {
                lblResumen.Text = "Sin frases registradas en esta sesión.";
                return;
            }

            int total = sesion.Frases.Count;
            int acertadas = sesion.Frases.Count(f => f.Acertada);
            int fallos = sesion.Frases.Sum(f => f.Fallos);
            double pct = Math.Round((double)acertadas / total * 100, 1);
            double velMedia = sesion.Frases
                .Where(f => f.Acertada && f.SegundosPorCaracter > 0)
                .Select(f => f.SegundosPorCaracter)
                .DefaultIfEmpty(0)
                .Average();

            string duracion = sesion.Duracion.HasValue
                ? $"{(int)sesion.Duracion.Value.TotalMinutes}m {sesion.Duracion.Value.Seconds:D2}s"
                : "sesión interrumpida";

            lblResumen.Text = $"Duración: {duracion}   |   Frases: {total}   |   " +
                              $"Acertadas: {acertadas} ({pct}%)   |   " +
                              $"Fallos: {fallos}   |   Vel. media: {velMedia:0.00} seg/car";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (cmbAlumnos.SelectedIndex < 0) return;
            string nombre = alumnos[cmbAlumnos.SelectedIndex].Nombre;

            if (MessageBox.Show(
                    $"¿Borrar todas las estadísticas de {nombre}?\nEsta acción no se puede deshacer.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                GestorEstadisticas.Limpiar(nombre);
                CargarEstadisticas(nombre);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
