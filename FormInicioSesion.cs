using LeeyCopia.Modelo;
using LeeyCopia.Servicios;

namespace LeeyCopia
{
    public partial class FormInicioSesion : Form
    {
        public int FrasesSeleccionadas { get; private set; } = 1;
        public Alumno? AlumnoSeleccionado { get; private set; }

        private readonly int maxFrases;
        private List<Alumno> alumnos;

        /// <summary>Constructor para el diseñador de Windows Forms.</summary>
        public FormInicioSesion() : this(10, new List<Alumno>()) { }

        public FormInicioSesion(int maxFrases, List<Alumno> alumnos)
        {
            this.maxFrases = maxFrases;
            this.alumnos = alumnos;
            InitializeComponent();
        }

        private void FormInicioSesion_Load(object sender, EventArgs e)
        {
            numFrases.Maximum = maxFrases;
            numFrases.Value = maxFrases;
            lblMaximo.Text = $"Máximo disponible: {maxFrases}";

            cmbAlumnos.Items.Clear();
            foreach (var a in alumnos)
                cmbAlumnos.Items.Add(a.Nombre);

            if (cmbAlumnos.Items.Count > 0)
                cmbAlumnos.SelectedIndex = 0;
        }

        private void btnNuevoAlumno_Click(object sender, EventArgs e)
        {
            using var dlg = new FormNombreAlumno();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string nombre = dlg.NombreIntroducido;

            if (alumnos.Any(a => a.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un alumno con ese nombre.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevo = new Alumno { Nombre = nombre };
            alumnos.Add(nuevo);

            var config = GestorAlumnos.Cargar();
            config.Alumnos.Add(nuevo);
            GestorAlumnos.Guardar(config);

            cmbAlumnos.Items.Add(nombre);
            cmbAlumnos.SelectedIndex = cmbAlumnos.Items.Count - 1;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbAlumnos.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona o crea un alumno antes de continuar.",
                    "Sin alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            AlumnoSeleccionado = alumnos[cmbAlumnos.SelectedIndex];
            FrasesSeleccionadas = (int)numFrases.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
