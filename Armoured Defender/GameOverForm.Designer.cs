
namespace Armoured_Defender
{
    partial class GameOverForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOverForm));
            this.lblGameOver = new System.Windows.Forms.Label();
            this.lblGetName = new System.Windows.Forms.Label();
            this.lblLeadConfirm = new System.Windows.Forms.Label();
            this.txbname = new System.Windows.Forms.TextBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnGoToMain = new System.Windows.Forms.Button();
            this.pBExplosion = new System.Windows.Forms.PictureBox();
            this.pbAlien = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBExplosion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlien)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.BackColor = System.Drawing.Color.Transparent;
            this.lblGameOver.Font = new System.Drawing.Font("OCR A Extended", 100.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGameOver.Location = new System.Drawing.Point(12, 29);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(991, 174);
            this.lblGameOver.TabIndex = 0;
            this.lblGameOver.Text = "GAME OVER";
            // 
            // lblGetName
            // 
            this.lblGetName.AutoSize = true;
            this.lblGetName.BackColor = System.Drawing.Color.Transparent;
            this.lblGetName.Font = new System.Drawing.Font("OCR A Extended", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGetName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblGetName.Location = new System.Drawing.Point(26, 693);
            this.lblGetName.Name = "lblGetName";
            this.lblGetName.Size = new System.Drawing.Size(908, 32);
            this.lblGetName.TabIndex = 1;
            this.lblGetName.Text = "Please enter your 3 character long username -->";
            // 
            // lblLeadConfirm
            // 
            this.lblLeadConfirm.AutoSize = true;
            this.lblLeadConfirm.BackColor = System.Drawing.Color.Transparent;
            this.lblLeadConfirm.Font = new System.Drawing.Font("OCR A Extended", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeadConfirm.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblLeadConfirm.Location = new System.Drawing.Point(35, 547);
            this.lblLeadConfirm.Name = "lblLeadConfirm";
            this.lblLeadConfirm.Size = new System.Drawing.Size(1023, 35);
            this.lblLeadConfirm.TabIndex = 2;
            this.lblLeadConfirm.Text = "Congratulations, you made it to the leaderboard!";
            // 
            // txbname
            // 
            this.txbname.BackColor = System.Drawing.Color.DimGray;
            this.txbname.Font = new System.Drawing.Font("OCR A Extended", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbname.Location = new System.Drawing.Point(958, 693);
            this.txbname.MaxLength = 3;
            this.txbname.Name = "txbname";
            this.txbname.Size = new System.Drawing.Size(148, 35);
            this.txbname.TabIndex = 3;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("OCR A Extended", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblScore.Location = new System.Drawing.Point(27, 329);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(819, 83);
            this.lblScore.TabIndex = 4;
            this.lblScore.Text = "Your score was: \r\n";
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.Transparent;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEnter.Font = new System.Drawing.Font("OCR A Extended", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.ForeColor = System.Drawing.SystemColors.Info;
            this.btnEnter.Location = new System.Drawing.Point(1156, 684);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(164, 54);
            this.btnEnter.TabIndex = 5;
            this.btnEnter.Text = "&Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("OCR A Extended", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.DarkRed;
            this.btnExit.Location = new System.Drawing.Point(1583, 23);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 58);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "E&xit ";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnGoToMain
            // 
            this.btnGoToMain.BackColor = System.Drawing.Color.Transparent;
            this.btnGoToMain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoToMain.Font = new System.Drawing.Font("OCR A Extended", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToMain.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGoToMain.Location = new System.Drawing.Point(1268, 29);
            this.btnGoToMain.Name = "btnGoToMain";
            this.btnGoToMain.Size = new System.Drawing.Size(276, 47);
            this.btnGoToMain.TabIndex = 7;
            this.btnGoToMain.Text = "Go to Main Menu ";
            this.btnGoToMain.UseVisualStyleBackColor = false;
            this.btnGoToMain.Click += new System.EventHandler(this.btnGoToMain_Click);
            // 
            // pBExplosion
            // 
            this.pBExplosion.BackColor = System.Drawing.Color.Transparent;
            this.pBExplosion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBExplosion.BackgroundImage")));
            this.pBExplosion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pBExplosion.Location = new System.Drawing.Point(1412, 157);
            this.pBExplosion.Name = "pBExplosion";
            this.pBExplosion.Size = new System.Drawing.Size(207, 120);
            this.pBExplosion.TabIndex = 8;
            this.pBExplosion.TabStop = false;
            // 
            // pbAlien
            // 
            this.pbAlien.BackColor = System.Drawing.Color.Transparent;
            this.pbAlien.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbAlien.BackgroundImage")));
            this.pbAlien.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAlien.Location = new System.Drawing.Point(1587, 283);
            this.pbAlien.Name = "pbAlien";
            this.pbAlien.Size = new System.Drawing.Size(32, 50);
            this.pbAlien.TabIndex = 9;
            this.pbAlien.TabStop = false;
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1614, 903);
            this.Controls.Add(this.pbAlien);
            this.Controls.Add(this.pBExplosion);
            this.Controls.Add(this.btnGoToMain);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.txbname);
            this.Controls.Add(this.lblLeadConfirm);
            this.Controls.Add(this.lblGetName);
            this.Controls.Add(this.lblGameOver);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameOverForm";
            this.Text = "GameOverForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.GameOverForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pBExplosion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGameOver;
        private System.Windows.Forms.Label lblGetName;
        private System.Windows.Forms.Label lblLeadConfirm;
        private System.Windows.Forms.TextBox txbname;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnGoToMain;
        private System.Windows.Forms.PictureBox pBExplosion;
        private System.Windows.Forms.PictureBox pbAlien;
    }
}