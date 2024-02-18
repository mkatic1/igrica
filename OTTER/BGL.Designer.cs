namespace OTTER
{
    partial class BGL
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
            this.components = new System.ComponentModel.Container();
            this.syncRate = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lbl_Score = new System.Windows.Forms.Label();
            this.lbl_score_broj = new System.Windows.Forms.Label();
            this.lbl_stoperica = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // syncRate
            // 
            this.syncRate.AutoSize = true;
            this.syncRate.BackColor = System.Drawing.Color.Transparent;
            this.syncRate.Location = new System.Drawing.Point(12, 394);
            this.syncRate.Name = "syncRate";
            this.syncRate.Size = new System.Drawing.Size(71, 52);
            this.syncRate.TabIndex = 0;
            this.syncRate.Text = "60";
            this.syncRate.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 17;
            this.timer1.Tick += new System.EventHandler(this.Update);
            // 
            // timer2
            // 
            this.timer2.Interval = 250;
            this.timer2.Tick += new System.EventHandler(this.updateFrameRate);
            // 
            // lbl_Score
            // 
            this.lbl_Score.AutoSize = true;
            this.lbl_Score.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lbl_Score.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Score.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_Score.Font = new System.Drawing.Font("Showcard Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Score.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lbl_Score.Location = new System.Drawing.Point(654, 7);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(171, 55);
            this.lbl_Score.TabIndex = 1;
            this.lbl_Score.Text = "SCORE:";
            this.lbl_Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_score_broj
            // 
            this.lbl_score_broj.AutoSize = true;
            this.lbl_score_broj.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_score_broj.Font = new System.Drawing.Font("Showcard Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_score_broj.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbl_score_broj.Location = new System.Drawing.Point(827, 11);
            this.lbl_score_broj.Name = "lbl_score_broj";
            this.lbl_score_broj.Size = new System.Drawing.Size(0, 53);
            this.lbl_score_broj.TabIndex = 2;
            // 
            // lbl_stoperica
            // 
            this.lbl_stoperica.AutoSize = true;
            this.lbl_stoperica.Font = new System.Drawing.Font("Showcard Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stoperica.Location = new System.Drawing.Point(266, 13);
            this.lbl_stoperica.Name = "lbl_stoperica";
            this.lbl_stoperica.Size = new System.Drawing.Size(0, 53);
            this.lbl_stoperica.TabIndex = 3;
            // 
            // BGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(26F, 52F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(978, 498);
            this.Controls.Add(this.lbl_stoperica);
            this.Controls.Add(this.lbl_score_broj);
            this.Controls.Add(this.lbl_Score);
            this.Controls.Add(this.syncRate);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximizeBox = false;
            this.Name = "BGL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2DGL";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BGL_FormClosed);
            this.Load += new System.EventHandler(this.startTimer);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mouseClicked);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label syncRate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lbl_Score;
        private System.Windows.Forms.Label lbl_score_broj;
        private System.Windows.Forms.Label lbl_stoperica;
    }
}

