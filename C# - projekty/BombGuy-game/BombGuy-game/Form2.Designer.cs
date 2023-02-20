namespace BombGuy_game
{
    partial class GameMenu
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
            this.bNewGame = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.bTwoPlayers = new System.Windows.Forms.Button();
            this.bFourPlayers = new System.Windows.Forms.Button();
            this.bThreePlayers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bNewGame
            // 
            this.bNewGame.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.bNewGame.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bNewGame.Location = new System.Drawing.Point(75, 180);
            this.bNewGame.Name = "bNewGame";
            this.bNewGame.Size = new System.Drawing.Size(300, 100);
            this.bNewGame.TabIndex = 0;
            this.bNewGame.Text = "NEW GAME";
            this.bNewGame.UseVisualStyleBackColor = false;
            this.bNewGame.Click += new System.EventHandler(this.bNewGame_Click);
            // 
            // bExit
            // 
            this.bExit.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.bExit.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bExit.Location = new System.Drawing.Point(75, 300);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(300, 100);
            this.bExit.TabIndex = 1;
            this.bExit.Text = "EXIT";
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // bTwoPlayers
            // 
            this.bTwoPlayers.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.bTwoPlayers.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bTwoPlayers.Location = new System.Drawing.Point(75, 160);
            this.bTwoPlayers.Name = "bTwoPlayers";
            this.bTwoPlayers.Size = new System.Drawing.Size(300, 85);
            this.bTwoPlayers.TabIndex = 2;
            this.bTwoPlayers.Text = "TWO PLAYERS";
            this.bTwoPlayers.UseVisualStyleBackColor = false;
            this.bTwoPlayers.Click += new System.EventHandler(this.bTwoPlayers_Click);
            // 
            // bFourPlayers
            // 
            this.bFourPlayers.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.bFourPlayers.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bFourPlayers.Location = new System.Drawing.Point(75, 360);
            this.bFourPlayers.Name = "bFourPlayers";
            this.bFourPlayers.Size = new System.Drawing.Size(300, 85);
            this.bFourPlayers.TabIndex = 3;
            this.bFourPlayers.Text = "FOUR PLAYERS";
            this.bFourPlayers.UseVisualStyleBackColor = false;
            this.bFourPlayers.Click += new System.EventHandler(this.bFourPlayers_Click);
            // 
            // bThreePlayers
            // 
            this.bThreePlayers.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.bThreePlayers.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bThreePlayers.Location = new System.Drawing.Point(75, 260);
            this.bThreePlayers.Name = "bThreePlayers";
            this.bThreePlayers.Size = new System.Drawing.Size(300, 85);
            this.bThreePlayers.TabIndex = 4;
            this.bThreePlayers.Text = "THREE PLAYERS";
            this.bThreePlayers.UseVisualStyleBackColor = false;
            this.bThreePlayers.Click += new System.EventHandler(this.bThreePlayers_Click);
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BombGuy_game.Properties.Resources.bricks1_jpg;
            this.ClientSize = new System.Drawing.Size(732, 453);
            this.Controls.Add(this.bThreePlayers);
            this.Controls.Add(this.bFourPlayers);
            this.Controls.Add(this.bTwoPlayers);
            this.Controls.Add(this.bExit);
            this.Controls.Add(this.bNewGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameMenu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.GameMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button bNewGame;
        private Button bExit;
        private Button bTwoPlayers;
        private Button bFourPlayers;
        private Button bThreePlayers;
    }
}