namespace LeeyCopia
{
    partial class FormConfigFrases
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewItem listViewItem1 = new ListViewItem("MI MAMA ME MIMA");
            listView1 = new ListView();
            textBox1 = new TextBox();
            btnAdd = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.CheckBoxes = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listView1.Items.AddRange(new ListViewItem[] { listViewItem1 });
            listView1.Location = new Point(12, 49);
            listView1.Name = "listView1";
            listView1.Size = new Size(731, 204);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 20);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(551, 23);
            textBox1.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(569, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(84, 23);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Añadir";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(659, 19);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(84, 23);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Borrar";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(659, 259);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(84, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FormConfigFrases
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(760, 291);
            Controls.Add(btnSave);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(textBox1);
            Controls.Add(listView1);
            Name = "FormConfigFrases";
            Text = "FormConfigFrases";
            Load += FormConfigFrases_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private TextBox textBox1;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnSave;
    }
}