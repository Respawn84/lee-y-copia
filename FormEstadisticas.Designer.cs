namespace LeeyCopia
{
    partial class FormEstadisticas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblAlumno = new Label();
            cmbAlumnos = new ComboBox();
            listViewSesiones = new ListView();
            colSFecha = new ColumnHeader();
            colSDuracion = new ColumnHeader();
            colSTotal = new ColumnHeader();
            colSAcertadas = new ColumnHeader();
            colSFallos = new ColumnHeader();
            lblSesiones = new Label();
            lblFrases = new Label();
            listViewFrases = new ListView();
            colFHora = new ColumnHeader();
            colFFrase = new ColumnHeader();
            colFLongitud = new ColumnHeader();
            colFSegundos = new ColumnHeader();
            colFSegPorChar = new ColumnHeader();
            colFFallos = new ColumnHeader();
            colFAcertada = new ColumnHeader();
            lblResumen = new Label();
            btnLimpiar = new Button();
            btnCerrar = new Button();
            SuspendLayout();
            // 
            // lblAlumno
            // 
            lblAlumno.AutoSize = true;
            lblAlumno.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAlumno.Location = new Point(12, 12);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Text = "Alumno:";
            // 
            // cmbAlumnos
            // 
            cmbAlumnos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumnos.Font = new Font("Segoe UI", 10F);
            cmbAlumnos.Location = new Point(70, 8);
            cmbAlumnos.Name = "cmbAlumnos";
            cmbAlumnos.Size = new Size(260, 25);
            cmbAlumnos.TabIndex = 0;
            cmbAlumnos.SelectedIndexChanged += cmbAlumnos_SelectedIndexChanged;
            // 
            // lblSesiones
            // 
            lblSesiones.AutoSize = true;
            lblSesiones.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSesiones.Location = new Point(12, 44);
            lblSesiones.Name = "lblSesiones";
            lblSesiones.Text = "Sesiones";
            // 
            // listViewSesiones
            // 
            listViewSesiones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listViewSesiones.Columns.AddRange(new ColumnHeader[] { colSFecha, colSDuracion, colSTotal, colSAcertadas, colSFallos });
            listViewSesiones.FullRowSelect = true;
            listViewSesiones.GridLines = true;
            listViewSesiones.HideSelection = false;
            listViewSesiones.Location = new Point(12, 62);
            listViewSesiones.MultiSelect = false;
            listViewSesiones.Name = "listViewSesiones";
            listViewSesiones.Size = new Size(960, 140);
            listViewSesiones.TabIndex = 1;
            listViewSesiones.UseCompatibleStateImageBehavior = false;
            listViewSesiones.View = View.Details;
            listViewSesiones.SelectedIndexChanged += listViewSesiones_SelectedIndexChanged;
            // 
            // colSFecha
            // 
            colSFecha.Text = "Fecha y hora";
            colSFecha.Width = 130;
            // 
            // colSDuracion
            // 
            colSDuracion.Text = "Duración";
            colSDuracion.Width = 100;
            // 
            // colSTotal
            // 
            colSTotal.Text = "Frases";
            colSTotal.Width = 60;
            colSTotal.TextAlign = HorizontalAlignment.Right;
            // 
            // colSAcertadas
            // 
            colSAcertadas.Text = "Acertadas";
            colSAcertadas.Width = 100;
            colSAcertadas.TextAlign = HorizontalAlignment.Right;
            // 
            // colSFallos
            // 
            colSFallos.Text = "Fallos";
            colSFallos.Width = 60;
            colSFallos.TextAlign = HorizontalAlignment.Right;
            // 
            // lblFrases
            // 
            lblFrases.AutoSize = true;
            lblFrases.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFrases.Location = new Point(12, 212);
            lblFrases.Name = "lblFrases";
            lblFrases.Text = "Detalle de frases";
            // 
            // listViewFrases
            // 
            listViewFrases.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewFrases.Columns.AddRange(new ColumnHeader[] { colFHora, colFFrase, colFLongitud, colFSegundos, colFSegPorChar, colFFallos, colFAcertada });
            listViewFrases.FullRowSelect = true;
            listViewFrases.GridLines = true;
            listViewFrases.Location = new Point(12, 230);
            listViewFrases.Name = "listViewFrases";
            listViewFrases.Size = new Size(960, 195);
            listViewFrases.TabIndex = 2;
            listViewFrases.UseCompatibleStateImageBehavior = false;
            listViewFrases.View = View.Details;
            // 
            // colFHora
            // 
            colFHora.Text = "Hora";
            colFHora.Width = 75;
            // 
            // colFFrase
            // 
            colFFrase.Text = "Frase";
            colFFrase.Width = 330;
            // 
            // colFLongitud
            // 
            colFLongitud.Text = "Caracteres";
            colFLongitud.Width = 80;
            colFLongitud.TextAlign = HorizontalAlignment.Right;
            // 
            // colFSegundos
            // 
            colFSegundos.Text = "Segundos";
            colFSegundos.Width = 80;
            colFSegundos.TextAlign = HorizontalAlignment.Right;
            // 
            // colFSegPorChar
            // 
            colFSegPorChar.Text = "Seg/Carácter";
            colFSegPorChar.Width = 95;
            colFSegPorChar.TextAlign = HorizontalAlignment.Right;
            // 
            // colFFallos
            // 
            colFFallos.Text = "Fallos";
            colFFallos.Width = 55;
            colFFallos.TextAlign = HorizontalAlignment.Right;
            // 
            // colFAcertada
            // 
            colFAcertada.Text = "Resultado";
            colFAcertada.Width = 80;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = false;
            lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblResumen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblResumen.Location = new Point(12, 433);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(760, 22);
            lblResumen.TabIndex = 3;
            lblResumen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimpiar.Font = new Font("Segoe UI", 9F);
            btnLimpiar.Location = new Point(782, 430);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(90, 28);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar todo";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.Font = new Font("Segoe UI", 9F);
            btnCerrar.Location = new Point(882, 430);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(90, 28);
            btnCerrar.TabIndex = 5;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FormEstadisticas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 470);
            Controls.Add(lblAlumno);
            Controls.Add(cmbAlumnos);
            Controls.Add(lblSesiones);
            Controls.Add(listViewSesiones);
            Controls.Add(lblFrases);
            Controls.Add(listViewFrases);
            Controls.Add(lblResumen);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCerrar);
            MinimumSize = new Size(800, 500);
            Name = "FormEstadisticas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Estadísticas de sesiones";
            Load += FormEstadisticas_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAlumno;
        private ComboBox cmbAlumnos;
        private ListView listViewSesiones;
        private ColumnHeader colSFecha;
        private ColumnHeader colSDuracion;
        private ColumnHeader colSTotal;
        private ColumnHeader colSAcertadas;
        private ColumnHeader colSFallos;
        private ListView listViewFrases;
        private ColumnHeader colFHora;
        private ColumnHeader colFFrase;
        private ColumnHeader colFLongitud;
        private ColumnHeader colFSegundos;
        private ColumnHeader colFSegPorChar;
        private ColumnHeader colFFallos;
        private ColumnHeader colFAcertada;
        private Label lblSesiones;
        private Label lblFrases;
        private Label lblResumen;
        private Button btnLimpiar;
        private Button btnCerrar;
    }
}
