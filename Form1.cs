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
        private int frasesCopiadas = 0;
        private bool impideCerrar = true;

        // Estadísticas
        private Modelo.Sesion sesionActual = new Modelo.Sesion();
        private int indiceSesionActual = -1;
        private DateTime inicioFrase;
        private int fallosFraseActual = 0;
        private bool sesionCerrada = false;
        private System.Windows.Forms.Timer timerSesion = new System.Windows.Forms.Timer();
        private int metaSesion = 0;
        private HashSet<string> frasesUsadasEnSesion = new HashSet<string>();
        private Modelo.Alumno? alumnoActual;

        public Form1()
        {
            InitializeComponent();

            // Ventana maximizada
            this.WindowState = FormWindowState.Maximized;

            // Inicializar TTS
            synth = new SpeechSynthesizer();
            synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            synth.Volume = 100;
            synth.Rate = -3; // Velocidad más lenta para niños

            // Inicializar random
            random = new Random();

            // Timer de sesión
            timerSesion.Interval = 1000;
            timerSesion.Tick += TimerSesion_Tick;

            // Cargar frases
            frasesActivas = new List<Frase>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarFrasesActivas();
            MostrarSiguienteFrase();
            this.WindowState = FormWindowState.Maximized;
            CentrarContenido();
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

        private void IniciarSesion()
        {
            if (frasesActivas.Count == 0)
            {
                MessageBox.Show(
                    "No hay frases activas. Ve a Configuración > Frases antes de iniciar una sesión.",
                    "Sin frases", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new FormInicioSesion(frasesActivas.Count, Servicios.GestorAlumnos.ObtenerTodos());
            if (dlg.ShowDialog(this) != DialogResult.OK)
                return;

            alumnoActual = dlg.AlumnoSeleccionado;
            if (alumnoActual == null) return;
            Servicios.GestorEstadisticas.AlumnoActual = alumnoActual.Nombre;
            metaSesion = dlg.FrasesSeleccionadas;
            frasesUsadasEnSesion.Clear();
            groupBox1.Visible = true;
            try
            {
                sesionActual = new Modelo.Sesion { FechaInicio = DateTime.Now };
                indiceSesionActual = Servicios.GestorEstadisticas.IniciarSesion(sesionActual);
                sesionCerrada = false;
                frasesCopiadas = 0;
                ActualizarContador();

                finSesionToolStripMenuItem.Enabled = true;
                reinicioDeSesiónToolStripMenuItem.Enabled = false;

                timerSesion.Start();
                toolStripStatusLabelTimer.Text = "⏳ 0:00";
            }
            catch
            {
                // No interrumpir si falla el guardado
            }

            MostrarSiguienteFrase();
        }

        private void CerrarSesion()
        {
            if (sesionCerrada || indiceSesionActual < 0)
                return;
            try
            {
                Servicios.GestorEstadisticas.CerrarSesion(indiceSesionActual, DateTime.Now);
                sesionCerrada = true;
            }
            catch { }
            groupBox1.Visible = false;
            timerSesion.Stop();
            toolStripStatusLabelTimer.Text = "⏳ Sin sesión";
            finSesionToolStripMenuItem.Enabled = false;
            reinicioDeSesiónToolStripMenuItem.Enabled = true;
        }

        private void TimerSesion_Tick(object? sender, EventArgs e)
        {
            if (sesionCerrada || indiceSesionActual < 0) return;
            var elapsed = DateTime.Now - sesionActual.FechaInicio;
            toolStripStatusLabelTimer.Text = $"⏳ {(int)elapsed.TotalMinutes}:{elapsed.Seconds:D2}";
        }

        void CentrarContenido()
        {
            groupBox1.Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - groupBox1.Width / 2;
            groupBox1.Top = Screen.PrimaryScreen.WorkingArea.Height / 2 - groupBox1.Height / 2;
        }

        private void MostrarSiguienteFrase()
        {
            var disponibles = frasesActivas
                .Where(f => !frasesUsadasEnSesion.Contains(f.Texto))
                .ToList();

            if (disponibles.Count == 0)
            {
                // Sin más frases disponibles: cerrar sesión automáticamente
                textBoxOriginal.Text = string.Empty;
                textBox2.Clear();
                fraseActual = string.Empty;
                FinalizarSesionAutomatica();
                return;
            }

            int indice = random.Next(disponibles.Count);
            fraseActual = disponibles[indice].Texto;
            frasesUsadasEnSesion.Add(fraseActual);
            textBoxOriginal.Text = fraseActual;

            // Reiniciar contadores de la frase en curso
            inicioFrase = DateTime.Now;
            fallosFraseActual = 0;

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
            // Saltar sin acertar: registrar como fallada
            if (!string.IsNullOrWhiteSpace(fraseActual))
            {
                GuardarRegistroActual(acertada: false);

                if (frasesUsadasEnSesion.Count >= metaSesion && metaSesion > 0)
                {
                    synth.SpeakAsync("¡Has terminado!");
                    Task.Delay(1500).ContinueWith(t =>
                    {
                        this.Invoke((MethodInvoker)FinalizarSesionAutomatica);
                    });
                    return;
                }
            }

            MostrarSiguienteFrase();
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
                // Correcto
                frasesCopiadas++;
                ActualizarContador();
                GuardarRegistroActual(acertada: true);
                MostrarResultado(true);

                if (frasesUsadasEnSesion.Count >= metaSesion && metaSesion > 0)
                {
                    // Meta alcanzada: cerrar sesión automáticamente
                    synth.SpeakAsync("¡Has terminado! ¡Muy bien!");
                    Task.Delay(2000).ContinueWith(t =>
                    {
                        this.Invoke((MethodInvoker)FinalizarSesionAutomatica);
                    });
                }
                else
                {
                    synth.SpeakAsync("¡Muy bien!");
                    Task.Delay(1500).ContinueWith(t =>
                    {
                        this.Invoke((MethodInvoker)MostrarSiguienteFrase);
                    });
                }
            }
            else
            {
                // Incorrecto: contar fallo
                fallosFraseActual++;
                MostrarResultado(false);
                synth.SpeakAsync("Inténtalo de nuevo");
                // Devolver foco a la caja de escritura
                textBox2.Focus();
            }
        }

        private void FinalizarSesionAutomatica()
        {
            CerrarSesion();
            FormEstadisticas frm = new FormEstadisticas();
            frm.ShowDialog();
        }

        private void GuardarRegistroActual(bool acertada)
        {
            if (indiceSesionActual < 0 || sesionCerrada)
                return;
            try
            {
                var registro = new Modelo.RegistroSesion
                {
                    Fecha = inicioFrase,
                    Frase = fraseActual,
                    LongitudFrase = fraseActual.Trim().Length,
                    SegundosEmpleados = Math.Round((DateTime.Now - inicioFrase).TotalSeconds, 1),
                    Fallos = fallosFraseActual,
                    Acertada = acertada
                };
                Servicios.GestorEstadisticas.GuardarRegistro(indiceSesionActual, registro);
            }
            catch
            {
                // No interrumpir el flujo del niño si falla el guardado
            }
        }

        private void ActualizarContador()
        {
            toolStripStatusLabel1.Text = $"Frases copiadas: {frasesCopiadas}";
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
            frm.ShowDialog();
            CargarFrasesActivas();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigAlumnos frm = new FormConfigAlumnos();
            frm.ShowDialog();
        }

        private void estadísticasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEstadisticas frm = new FormEstadisticas();
            frm.ShowDialog();
        }

        private void finSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesion();
            this.TopMost = false;
            FormEstadisticas frm = new FormEstadisticas();
            frm.ShowDialog();
            // Iniciar una sesión nueva para continuar si el niño sigue
            IniciarSesion();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesion();
            impideCerrar = false;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = impideCerrar; //Si clicamos en Programa -> Cerrar se permite el FormClosing.
        }

        private void reinicioDeSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesion();
            IniciarSesion();
        }
    }
}
