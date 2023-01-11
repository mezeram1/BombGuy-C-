namespace WinFormsApp1
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bStart = new System.Windows.Forms.Button();
            this.bNovaHra = new System.Windows.Forms.Button();
            this.lSkore = new System.Windows.Forms.Label();
            this.lTahy = new System.Windows.Forms.Label();
            this.lUvod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.SystemColors.GrayText;
            this.bStart.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bStart.Location = new System.Drawing.Point(266, 256);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(258, 98);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "START";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bNovaHra
            // 
            this.bNovaHra.BackColor = System.Drawing.SystemColors.GrayText;
            this.bNovaHra.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bNovaHra.Location = new System.Drawing.Point(618, 22);
            this.bNovaHra.Name = "bNovaHra";
            this.bNovaHra.Size = new System.Drawing.Size(147, 41);
            this.bNovaHra.TabIndex = 1;
            this.bNovaHra.Text = "NOVÁ HRA";
            this.bNovaHra.UseVisualStyleBackColor = false;
            this.bNovaHra.Click += new System.EventHandler(this.bNovaHra_Click);
            // 
            // lSkore
            // 
            this.lSkore.AutoSize = true;
            this.lSkore.Font = new System.Drawing.Font("Showcard Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lSkore.Location = new System.Drawing.Point(250, 150);
            this.lSkore.Name = "lSkore";
            this.lSkore.Size = new System.Drawing.Size(119, 37);
            this.lSkore.TabIndex = 2;
            this.lSkore.Text = "SKÓRE: ";
            // 
            // lTahy
            // 
            this.lTahy.AutoSize = true;
            this.lTahy.Font = new System.Drawing.Font("Showcard Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lTahy.Location = new System.Drawing.Point(27, 22);
            this.lTahy.Name = "lTahy";
            this.lTahy.Size = new System.Drawing.Size(93, 74);
            this.lTahy.TabIndex = 3;
            this.lTahy.Text = "0";
            // 
            // lUvod
            // 
            this.lUvod.AutoSize = true;
            this.lUvod.Font = new System.Drawing.Font("Showcard Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lUvod.Location = new System.Drawing.Point(219, 108);
            this.lUvod.Name = "lUvod";
            this.lUvod.Size = new System.Drawing.Size(342, 98);
            this.lUvod.TabIndex = 4;
            this.lUvod.Text = "PEXESO";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lUvod);
            this.Controls.Add(this.lTahy);
            this.Controls.Add(this.lSkore);
            this.Controls.Add(this.bNovaHra);
            this.Controls.Add(this.bStart);
            this.Name = "Form1";
            this.Text = "Pexeso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button bStart;
        private Button bNovaHra;
        private Label lSkore;
        private Label lTahy;
        private Label lUvod;
    }
}