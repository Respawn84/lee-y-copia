using LeeyCopia.Modelo;
using LeeyCopia.Servicios;

namespace LeeyCopia
{
    public partial class FormConfigAlumnos : Form
    {
        private ConfigAlumnos config = new ConfigAlumnos();

        public FormConfigAlumnos()
        {
            InitializeComponent();
        }

        private void FormConfigAlumnos_Load(object sender, EventArgs e)
        {
            CargarLista();
        }

        private void CargarLista()
        {
            listView1.Items.Clear();
            config = GestorAlumnos.Cargar();

            foreach (var a in config.Alumnos)
            {
                var item = new ListViewItem(a.Nombre);
                item.SubItems.Add(a.FechaAlta.ToString("dd/MM/yyyy"));
                item.Tag = a;
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                textBox1.Clear();
                btnRename.Enabled = false;
                return;
            }

            var alumno = (Alumno)listView1.SelectedItems[0].Tag;
            textBox1.Text = alumno.Nombre;
            btnRename.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Escribe un nombre antes de añadir.", "Sin nombre",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (config.Alumnos.Any(a => a.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un alumno con ese nombre.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevo = new Alumno { Nombre = nombre };
            config.Alumnos.Add(nuevo);
            GestorAlumnos.Guardar(config);

            textBox1.Clear();
            CargarLista();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            string nuevoNombre = textBox1.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(nuevoNombre))
            {
                MessageBox.Show("Escribe el nuevo nombre.", "Sin nombre",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (config.Alumnos.Any(a => a.Nombre.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un alumno con ese nombre.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var alumno = (Alumno)listView1.SelectedItems[0].Tag;

            if (MessageBox.Show(
                    $"¿Renombrar \"{alumno.Nombre}\" a \"{nuevoNombre}\"?\n\nNota: el fichero de estadísticas anterior quedará con el nombre antiguo.",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                alumno.Nombre = nuevoNombre;
                GestorAlumnos.Guardar(config);
                CargarLista();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
