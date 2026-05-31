namespace LeeyCopia
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            programaToolStripMenuItem = new ToolStripMenuItem();
            reinicioDeSesiónToolStripMenuItem = new ToolStripMenuItem();
            finSesionToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            configuraciónToolStripMenuItem = new ToolStripMenuItem();
            frasesToolStripMenuItem = new ToolStripMenuItem();
            alumnosToolStripMenuItem = new ToolStripMenuItem();
            estadísticasToolStripMenuItem = new ToolStripMenuItem();
            textBoxOriginal = new TextBox();
            label1 = new Label();
            btnReadOriginal = new Button();
            textBox2 = new TextBox();
            button2 = new Button();
            label2 = new Label();
            btnNextFrase = new Button();
            btnCheckAndNext = new Button();
            groupBox1 = new GroupBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabelTimer = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { programaToolStripMenuItem, configuraciónToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(864, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // programaToolStripMenuItem
            // 
            programaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { reinicioDeSesiónToolStripMenuItem, finSesionToolStripMenuItem, salirToolStripMenuItem });
            programaToolStripMenuItem.Name = "programaToolStripMenuItem";
            programaToolStripMenuItem.Size = new Size(71, 20);
            programaToolStripMenuItem.Text = "Programa";
            // 
            // reinicioDeSesiónToolStripMenuItem
            // 
            reinicioDeSesiónToolStripMenuItem.Name = "reinicioDeSesiónToolStripMenuItem";
            reinicioDeSesiónToolStripMenuItem.Size = new Size(180, 22);
            reinicioDeSesiónToolStripMenuItem.Text = "Inicio de Sesión";
            reinicioDeSesiónToolStripMenuItem.Click += reinicioDeSesiónToolStripMenuItem_Click;
            // 
            // finSesionToolStripMenuItem
            // 
            finSesionToolStripMenuItem.Enabled = false;
            finSesionToolStripMenuItem.Name = "finSesionToolStripMenuItem";
            finSesionToolStripMenuItem.Size = new Size(180, 22);
            finSesionToolStripMenuItem.Text = "Fin de sesión";
            finSesionToolStripMenuItem.Click += finSesionToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(180, 22);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // configuraciónToolStripMenuItem
            // 
            configuraciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { frasesToolStripMenuItem, alumnosToolStripMenuItem, estadísticasToolStripMenuItem });
            configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            configuraciónToolStripMenuItem.Size = new Size(95, 20);
            configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // frasesToolStripMenuItem
            // 
            frasesToolStripMenuItem.Name = "frasesToolStripMenuItem";
            frasesToolStripMenuItem.Size = new Size(134, 22);
            frasesToolStripMenuItem.Text = "Frases";
            frasesToolStripMenuItem.Click += frasesToolStripMenuItem_Click;
            // 
            // alumnosToolStripMenuItem
            // 
            alumnosToolStripMenuItem.Name = "alumnosToolStripMenuItem";
            alumnosToolStripMenuItem.Size = new Size(134, 22);
            alumnosToolStripMenuItem.Text = "Alumnos";
            alumnosToolStripMenuItem.Click += alumnosToolStripMenuItem_Click;
            // 
            // estadísticasToolStripMenuItem
            // 
            estadísticasToolStripMenuItem.Name = "estadísticasToolStripMenuItem";
            estadísticasToolStripMenuItem.Size = new Size(134, 22);
            estadísticasToolStripMenuItem.Text = "Estadísticas";
            estadísticasToolStripMenuItem.Click += estadísticasToolStripMenuItem_Click;
            // 
            // textBoxOriginal
            // 
            textBoxOriginal.Enabled = false;
            textBoxOriginal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            textBoxOriginal.Location = new Point(6, 42);
            textBoxOriginal.Name = "textBoxOriginal";
            textBoxOriginal.Size = new Size(642, 32);
            textBoxOriginal.TabIndex = 1;
            textBoxOriginal.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(6, 20);
            label1.Name = "label1";
            label1.Size = new Size(173, 19);
            label1.TabIndex = 2;
            label1.Text = "Copia la Siguiente Frase:";
            // 
            // btnReadOriginal
            // 
            btnReadOriginal.Font = new Font("Segoe UI", 10F);
            btnReadOriginal.Location = new Point(6, 80);
            btnReadOriginal.Name = "btnReadOriginal";
            btnReadOriginal.Size = new Size(75, 30);
            btnReadOriginal.TabIndex = 3;
            btnReadOriginal.Text = "🔊 Lee";
            btnReadOriginal.UseVisualStyleBackColor = true;
            btnReadOriginal.Click += btnReadOriginal_Click;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 14F);
            textBox2.Location = new Point(6, 144);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(642, 32);
            textBox2.TabIndex = 4;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10F);
            button2.Location = new Point(6, 182);
            button2.Name = "button2";
            button2.Size = new Size(75, 30);
            button2.TabIndex = 5;
            button2.Text = "🔊 Lee";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(6, 122);
            label2.Name = "label2";
            label2.Size = new Size(146, 19);
            label2.TabIndex = 6;
            label2.Text = "Escribe la frase aquí:";
            // 
            // btnNextFrase
            // 
            btnNextFrase.Font = new Font("Segoe UI", 10F);
            btnNextFrase.Location = new Point(87, 80);
            btnNextFrase.Name = "btnNextFrase";
            btnNextFrase.Size = new Size(100, 30);
            btnNextFrase.TabIndex = 7;
            btnNextFrase.Text = "⏭️ Siguiente";
            btnNextFrase.UseVisualStyleBackColor = true;
            btnNextFrase.Click += btnNextFrase_Click;
            // 
            // btnCheckAndNext
            // 
            btnCheckAndNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCheckAndNext.Location = new Point(87, 182);
            btnCheckAndNext.Name = "btnCheckAndNext";
            btnCheckAndNext.Size = new Size(120, 30);
            btnCheckAndNext.TabIndex = 8;
            btnCheckAndNext.Text = "✓ Comprobar";
            btnCheckAndNext.UseVisualStyleBackColor = true;
            btnCheckAndNext.Click += btnCheckAndNext_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxOriginal);
            groupBox1.Controls.Add(btnCheckAndNext);
            groupBox1.Controls.Add(btnNextFrase);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnReadOriginal);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Location = new Point(12, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(688, 242);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Visible = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 16F);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabelTimer });
            statusStrip1.Location = new Point(0, 320);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(864, 35);
            statusStrip1.TabIndex = 11;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(191, 30);
            toolStripStatusLabel1.Text = "Frases Copiadas: 0";
            // 
            // toolStripStatusLabelTimer
            // 
            toolStripStatusLabelTimer.Alignment = ToolStripItemAlignment.Right;
            toolStripStatusLabelTimer.Font = new Font("Segoe UI", 14F);
            toolStripStatusLabelTimer.Name = "toolStripStatusLabelTimer";
            toolStripStatusLabelTimer.Size = new Size(116, 30);
            toolStripStatusLabelTimer.Text = "⏳ Sin sesión";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 355);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lee y Copia - Práctica de Lectoescritura";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem programaToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem finSesionToolStripMenuItem;
        private ToolStripMenuItem configuraciónToolStripMenuItem;
        private ToolStripMenuItem frasesToolStripMenuItem;
        private ToolStripMenuItem alumnosToolStripMenuItem;
        private ToolStripMenuItem estadísticasToolStripMenuItem;
        private TextBox textBoxOriginal;
        private Label label1;
        private Button btnReadOriginal;
        private TextBox textBox2;
        private Button button2;
        private Label label2;
        private Button btnNextFrase;
        private Button btnCheckAndNext;
        private GroupBox groupBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem reinicioDeSesiónToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelTimer;
    }
}
