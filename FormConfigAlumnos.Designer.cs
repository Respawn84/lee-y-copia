namespace LeeyCopia
{
    partial class FormConfigAlumnos
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
            listView1 = new ListView();
            colNombre = new ColumnHeader();
            colFechaAlta = new ColumnHeader();
            textBox1 = new TextBox();
            btnAdd = new Button();
            btnRename = new Button();
            btnClose = new Button();
            lblNombre = new Label();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { colNombre, colFechaAlta });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.HideSelection = false;
            listView1.Location = new Point(12, 49);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(560, 200);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // colNombre
            // 
            colNombre.Text = "Nombre";
            colNombre.Width = 380;
            // 
            // colFechaAlta
            // 
            colFechaAlta.Text = "Alta";
            colFechaAlta.Width = 120;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 18);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre:";
            lblNombre.Font = new Font("Segoe UI", 9F);
            // 
            // textBox1
            // 
            textBox1.Location = new Point(70, 15);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(350, 23);
            textBox1.TabIndex = 1;
            textBox1.CharacterCasing = CharacterCasing.Upper;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(430, 14);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(65, 25);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Añadir";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRename
            // 
            btnRename.Location = new Point(504, 14);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(68, 25);
            btnRename.TabIndex = 3;
            btnRename.Text = "Renombrar";
            btnRename.Enabled = false;
            btnRename.UseVisualStyleBackColor = true;
            btnRename.Click += btnRename_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(497, 264);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 25);
            btnClose.TabIndex = 4;
            btnClose.Text = "Cerrar";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FormConfigAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 300);
            Controls.Add(lblNombre);
            Controls.Add(textBox1);
            Controls.Add(btnAdd);
            Controls.Add(btnRename);
            Controls.Add(listView1);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormConfigAlumnos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de alumnos";
            Load += FormConfigAlumnos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ColumnHeader colNombre;
        private ColumnHeader colFechaAlta;
        private TextBox textBox1;
        private Button btnAdd;
        private Button btnRename;
        private Button btnClose;
        private Label lblNombre;
    }
}
