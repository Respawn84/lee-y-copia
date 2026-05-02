using System.Speech.Synthesis;
using LeeyCopia.Modelo;
using LeeyCopia.Servicios;

namespace LeeyCopia
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synth;
        private List<Frase> frasesActivas;
        private Random random;
        private string fraseActual = string.Empty;

        public Form1()
        {
            InitializeComponent();

            // Inicializar TTS
            synth = new SpeechSynthesizer();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            synth.Volume = 100;
            synth.Rate = -3; // Velocidad más lenta para niños

            // Inicializar random
            random = new Random();

            // Cargar frases
            frasesActivas = new List<Frase>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarFrasesActivas();
            MostrarSiguienteFrase();
        }

        private void CargarFrasesActivas()
        {
            try
            {
                frasesActivas = GestorFrases.ObtenerFrasesActivas();

                if (frasesActivas.Count == 0)
                {
                    MessageBox.Show(
                        "No hay frases activas configuradas.\n\nVe a Configuración > Frases para añadir frases.",
                        "Sin frases",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar las frases:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void MostrarSiguienteFrase()
        {
            if (frasesActivas.Count == 0)
            {
                textBoxOriginal.Text = "No hay frases disponibles";
                fraseActual = string.Empty;
                return;
            }

            // Seleccionar frase aleatoria
            int indice = random.Next(frasesActivas.Count);
            fraseActual = frasesActivas[indice].Texto;
            textBoxOriginal.Text = fraseActual;

            // Limpiar el área de copia
            textBox2.Clear();
            textBox2.Focus();
        }

        private void btnReadOriginal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxOriginal.Text))
            {
                synth.SpeakAsync(textBoxOriginal.Text);
            }
            // Devolver foco a la caja de escritura
            textBox2.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                synth.SpeakAsync(textBox2.Text);
            }
            // Devolver foco a la caja de escritura
            textBox2.Focus();
        }

        private void btnNextFrase_Click(object sender, EventArgs e)
        {
            MostrarSiguienteFrase();
            // El foco ya se establece en MostrarSiguienteFrase()
        }

        private void btnCheckAndNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fraseActual))
            {
                return;
            }

            // Normalizar textos para comparación (quitar espacios extra, mayúsculas)
            string original = NormalizarTexto(fraseActual);
            string copiado = NormalizarTexto(textBox2.Text);

            if (original == copiado)
            {
                // Correcto - Retroalimentación positiva
                MostrarResultado(true);
                synth.SpeakAsync("¡Muy bien!");
                
                // Pasar directamente a la siguiente frase tras un breve delay
                Task.Delay(1500).ContinueWith(t =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MostrarSiguienteFrase();
                    });
                });
            }
            else
            {
                // Incorrecto - Retroalimentación
                MostrarResultado(false);
                synth.SpeakAsync("Inténtalo de nuevo");
                // Devolver foco a la caja de escritura
                textBox2.Focus();
            }
        }

        private string NormalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            // Convertir a mayúsculas y quitar espacios extra
            return texto.Trim().ToUpper().Replace("  ", " ");
        }

        private void MostrarResultado(bool correcto)
        {
            if (correcto)
            {
                textBox2.BackColor = Color.LightGreen;
            }
            else
            {
                textBox2.BackColor = Color.LightCoral;
            }

            // Restaurar color original después de 1 segundo
            Task.Delay(1000).ContinueWith(t =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox2.BackColor = SystemColors.Window;
                });
            });
        }

        private void frasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigFrases frm = new FormConfigFrases();
            frm.ShowDialog(); // Usar ShowDialog para que sea modal

            // Recargar frases cuando se cierre el formulario de configuración
            CargarFrasesActivas();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                // Liberar recursos TTS
                if (synth != null)
                {
                    synth.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
