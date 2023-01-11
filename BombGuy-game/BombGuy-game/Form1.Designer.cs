namespace BombGuy_game
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.lTime = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.Main_timer = new System.Windows.Forms.Timer(this.components);
            this.pMap = new System.Windows.Forms.PictureBox();
            this.lPlayer1 = new System.Windows.Forms.Label();
            this.lPlayer3 = new System.Windows.Forms.Label();
            this.lPlayer2 = new System.Windows.Forms.Label();
            this.lPlayer4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).BeginInit();
            this.SuspendLayout();
            // 
            // lTime
            // 
            this.lTime.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lTime.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lTime.Location = new System.Drawing.Point(-1, 1);
            this.lTime.Name = "lTime";
            this.lTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lTime.Size = new System.Drawing.Size(150, 50);
            this.lTime.TabIndex = 1;
            this.lTime.Text = "0:00";
            this.lTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Main_timer
            // 
            this.Main_timer.Enabled = true;
            this.Main_timer.Interval = 30;
            this.Main_timer.Tick += new System.EventHandler(this.Main_timer_Tick);
            // 
            // pMap
            // 
            this.pMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pMap.BackColor = System.Drawing.Color.DimGray;
            this.pMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pMap.Location = new System.Drawing.Point(149, 1);
            this.pMap.Name = "pMap";
            this.pMap.Size = new System.Drawing.Size(750, 550);
            this.pMap.TabIndex = 0;
            this.pMap.TabStop = false;
            // 
            // lPlayer1
            // 
            this.lPlayer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lPlayer1.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lPlayer1.Location = new System.Drawing.Point(-1, 51);
            this.lPlayer1.Name = "lPlayer1";
            this.lPlayer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lPlayer1.Size = new System.Drawing.Size(150, 50);
            this.lPlayer1.TabIndex = 4;
            this.lPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lPlayer3
            // 
            this.lPlayer3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lPlayer3.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lPlayer3.Location = new System.Drawing.Point(-1, 151);
            this.lPlayer3.Name = "lPlayer3";
            this.lPlayer3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lPlayer3.Size = new System.Drawing.Size(150, 50);
            this.lPlayer3.TabIndex = 5;
            this.lPlayer3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lPlayer2
            // 
            this.lPlayer2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lPlayer2.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lPlayer2.Location = new System.Drawing.Point(-1, 101);
            this.lPlayer2.Name = "lPlayer2";
            this.lPlayer2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lPlayer2.Size = new System.Drawing.Size(150, 50);
            this.lPlayer2.TabIndex = 6;
            this.lPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lPlayer4
            // 
            this.lPlayer4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lPlayer4.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lPlayer4.Location = new System.Drawing.Point(-1, 201);
            this.lPlayer4.Name = "lPlayer4";
            this.lPlayer4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lPlayer4.Size = new System.Drawing.Size(150, 50);
            this.lPlayer4.TabIndex = 7;
            this.lPlayer4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(901, 552);
            this.Controls.Add(this.lPlayer4);
            this.Controls.Add(this.lPlayer2);
            this.Controls.Add(this.lPlayer3);
            this.Controls.Add(this.lPlayer1);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.pMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "GAME";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Game_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer Main_timer;
        private PictureBox pMap;
        private Label lPlayer1;
        private Label lPlayer3;
        private Label lPlayer2;
        private Label lPlayer4;
    }
}