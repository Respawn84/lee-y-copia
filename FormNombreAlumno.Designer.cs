namespace LeeyCopia
{
    partial class FormNombreAlumno
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
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnOk = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.Location = new Point(12, 14);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre del alumno:";
            // 
            // txtNombre
            // 
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(12, 34);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(270, 25);
            txtNombre.TabIndex = 0;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(105, 68);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(80, 26);
            btnOk.TabIndex = 1;
            btnOk.Text = "Aceptar";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(195, 68);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(80, 26);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormNombreAlumno
            // 
            AcceptButton = btnOk;
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 106);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(btnOk);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormNombreAlumno";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nuevo alumno";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombre;
        private TextBox txtNombre;
        private Button btnOk;
        private Button btnCancelar;
    }
}
