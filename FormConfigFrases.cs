using LeeyCopia.Modelo;
using LeeyCopia.Servicios;

namespace LeeyCopia
{
    public partial class FormConfigFrases : Form
    {
        ConfigFrases configFrases;
        public FormConfigFrases()
        {
            InitializeComponent();
            configFrases = new ConfigFrases();
        }

        private void FormConfigFrases_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            configFrases = GestorFrases.Cargar();
            foreach (Frase f in configFrases.Frases)
            {
                ListViewItem lwi = new ListViewItem();
                lwi.Checked = f.Activa;
                lwi.Text = f.Texto;
                listView1.Items.Add(lwi);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            configFrases.Frases = new List<Frase>();

            foreach (ListViewItem lwi in listView1.Items)
            {
                Frase frase = new Frase();
                frase.Activa = lwi.Checked;
                frase.Texto = lwi.Text;
                configFrases.Frases.Add(frase);
            }
            GestorFrases.Guardar(configFrases);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem lwi = new ListViewItem();
            lwi.Checked = true;
            lwi.Text = textBox1.Text.Trim().ToUpper();
            listView1.Items.Add(lwi);
            configFrases.Frases.Add(new Frase(lwi.Text));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Logica para eliminar la frase seleccionada (se ve en pantalla con fondo azul)
        }
    }
}
