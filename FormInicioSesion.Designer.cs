namespace LeeyCopia
{
    partial class FormInicioSesion
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
            btnNuevoAlumno = new Button();
            lblPregunta = new Label();
            numFrases = new NumericUpDown();
            lblMaximo = new Label();
            btnOk = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)numFrases).BeginInit();
            SuspendLayout();
            // 
            // lblAlumno
            // 
            lblAlumno.AutoSize = true;
            lblAlumno.Font = new Font("Segoe UI", 10F);
            lblAlumno.Location = new Point(20, 18);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Text = "Alumno:";
            // 
            // cmbAlumnos
            // 
            cmbAlumnos.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumnos.Font = new Font("Segoe UI", 10F);
            cmbAlumnos.Location = new Point(20, 40);
            cmbAlumnos.Name = "cmbAlumnos";
            cmbAlumnos.Size = new Size(240, 25);
            cmbAlumnos.TabIndex = 0;
            // 
            // btnNuevoAlumno
            // 
            btnNuevoAlumno.Font = new Font("Segoe UI", 9F);
            btnNuevoAlumno.Location = new Point(270, 39);
            btnNuevoAlumno.Name = "btnNuevoAlumno";
            btnNuevoAlumno.Size = new Size(70, 28);
            btnNuevoAlumno.TabIndex = 1;
            btnNuevoAlumno.Text = "Nuevo";
            btnNuevoAlumno.UseVisualStyleBackColor = true;
            btnNuevoAlumno.Click += btnNuevoAlumno_Click;
            // 
            // lblPregunta
            // 
            lblPregunta.AutoSize = true;
            lblPregunta.Font = new Font("Segoe UI", 10F);
            lblPregunta.Location = new Point(20, 85);
            lblPregunta.Name = "lblPregunta";
            lblPregunta.Text = "¿Cuántas frases tiene que copiar?";
            // 
            // numFrases
            // 
            numFrases.Font = new Font("Segoe UI", 13F);
            numFrases.Location = new Point(20, 110);
            numFrases.Minimum = 1;
            numFrases.Name = "numFrases";
            numFrases.Size = new Size(80, 30);
            numFrases.TabIndex = 2;
            numFrases.TextAlign = HorizontalAlignment.Center;
            // 
            // lblMaximo
            // 
            lblMaximo.AutoSize = true;
            lblMaximo.Font = new Font("Segoe UI", 9F);
            lblMaximo.ForeColor = Color.Gray;
            lblMaximo.Location = new Point(115, 118);
            lblMaximo.Name = "lblMaximo";
            lblMaximo.Text = "Máximo disponible: ?";
            // 
            // btnOk
            // 
            btnOk.Font = new Font("Segoe UI", 10F);
            btnOk.Location = new Point(150, 155);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(90, 30);
            btnOk.TabIndex = 3;
            btnOk.Text = "¡Vamos!";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(250, 155);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(90, 30);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormInicioSesion
            // 
            AcceptButton = btnOk;
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 200);
            Controls.Add(lblAlumno);
            Controls.Add(cmbAlumnos);
            Controls.Add(btnNuevoAlumno);
            Controls.Add(lblPregunta);
            Controls.Add(numFrases);
            Controls.Add(lblMaximo);
            Controls.Add(btnOk);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInicioSesion";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Iniciar sesión";
            Load += FormInicioSesion_Load;
            ((System.ComponentModel.ISupportInitialize)numFrases).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAlumno;
        private ComboBox cmbAlumnos;
        private Button btnNuevoAlumno;
        private Label lblPregunta;
        private NumericUpDown numFrases;
        private Label lblMaximo;
        private Button btnOk;
        private Button btnCancelar;
    }
}
