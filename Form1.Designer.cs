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
            salirToolStripMenuItem = new ToolStripMenuItem();
            configuraciónToolStripMenuItem = new ToolStripMenuItem();
            frasesToolStripMenuItem = new ToolStripMenuItem();
            textBoxOriginal = new TextBox();
            label1 = new Label();
            btnReadOriginal = new Button();
            textBox2 = new TextBox();
            button2 = new Button();
            label2 = new Label();
            btnNextFrase = new Button();
            btnCheckAndNext = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { programaToolStripMenuItem, configuraciónToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(698, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // programaToolStripMenuItem
            // 
            programaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { salirToolStripMenuItem });
            programaToolStripMenuItem.Name = "programaToolStripMenuItem";
            programaToolStripMenuItem.Size = new Size(71, 20);
            programaToolStripMenuItem.Text = "Programa";
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(96, 22);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // configuraciónToolStripMenuItem
            // 
            configuraciónToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { frasesToolStripMenuItem });
            configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            configuraciónToolStripMenuItem.Size = new Size(95, 20);
            configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // frasesToolStripMenuItem
            // 
            frasesToolStripMenuItem.Name = "frasesToolStripMenuItem";
            frasesToolStripMenuItem.Size = new Size(180, 22);
            frasesToolStripMenuItem.Text = "Frases";
            frasesToolStripMenuItem.Click += frasesToolStripMenuItem_Click;
            // 
            // textBoxOriginal
            // 
            textBoxOriginal.Enabled = false;
            textBoxOriginal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            textBoxOriginal.Location = new Point(12, 57);
            textBoxOriginal.Name = "textBoxOriginal";
            textBoxOriginal.Size = new Size(642, 32);
            textBoxOriginal.TabIndex = 1;
            textBoxOriginal.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(160, 19);
            label1.TabIndex = 2;
            label1.Text = "Copia la Siguiente Frase:";
            // 
            // btnReadOriginal
            // 
            btnReadOriginal.Font = new Font("Segoe UI", 10F);
            btnReadOriginal.Location = new Point(12, 95);
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
            textBox2.Location = new Point(12, 159);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(642, 32);
            textBox2.TabIndex = 4;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10F);
            button2.Location = new Point(12, 197);
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
            label2.Location = new Point(12, 137);
            label2.Name = "label2";
            label2.Size = new Size(133, 19);
            label2.TabIndex = 6;
            label2.Text = "Escribe la frase aquí:";
            // 
            // btnNextFrase
            // 
            btnNextFrase.Font = new Font("Segoe UI", 10F);
            btnNextFrase.Location = new Point(93, 95);
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
            btnCheckAndNext.Location = new Point(93, 197);
            btnCheckAndNext.Name = "btnCheckAndNext";
            btnCheckAndNext.Size = new Size(120, 30);
            btnCheckAndNext.TabIndex = 8;
            btnCheckAndNext.Text = "✓ Comprobar";
            btnCheckAndNext.UseVisualStyleBackColor = true;
            btnCheckAndNext.Click += btnCheckAndNext_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 287);
            Controls.Add(btnCheckAndNext);
            Controls.Add(btnNextFrase);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(textBox2);
            Controls.Add(btnReadOriginal);
            Controls.Add(label1);
            Controls.Add(textBoxOriginal);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lee y Copia - Práctica de Lectoescritura";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem programaToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
        private ToolStripMenuItem configuraciónToolStripMenuItem;
        private ToolStripMenuItem frasesToolStripMenuItem;
        private TextBox textBoxOriginal;
        private Label label1;
        private Button btnReadOriginal;
        private TextBox textBox2;
        private Button button2;
        private Label label2;
        private Button btnNextFrase;
        private Button btnCheckAndNext;
    }
}
