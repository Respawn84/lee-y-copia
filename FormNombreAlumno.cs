namespace LeeyCopia
{
    public partial class FormNombreAlumno : Form
    {
        public string NombreIntroducido { get; private set; } = string.Empty;

        public FormNombreAlumno()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Escribe un nombre.", "Sin nombre",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            NombreIntroducido = nombre;
            DialogResult = DialogResult.OK;
        }
    }
}
