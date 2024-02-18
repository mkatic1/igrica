
namespace OTTER
{
    partial class IzbornikForma
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
            this.btn_new_game = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_ime = new System.Windows.Forms.TextBox();
            this.btn_play_Again = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_new_game
            // 
            this.btn_new_game.BackColor = System.Drawing.Color.LightSalmon;
            this.btn_new_game.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_new_game.Font = new System.Drawing.Font("Showcard Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new_game.ForeColor = System.Drawing.Color.Black;
            this.btn_new_game.Location = new System.Drawing.Point(97, 116);
            this.btn_new_game.Name = "btn_new_game";
            this.btn_new_game.Size = new System.Drawing.Size(196, 67);
            this.btn_new_game.TabIndex = 0;
            this.btn_new_game.Text = "NEW GAME";
            this.btn_new_game.UseVisualStyleBackColor = false;
            this.btn_new_game.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "NAME :";
            // 
            // txtbox_ime
            // 
            this.txtbox_ime.BackColor = System.Drawing.Color.LimeGreen;
            this.txtbox_ime.Font = new System.Drawing.Font("Showcard Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbox_ime.Location = new System.Drawing.Point(134, 50);
            this.txtbox_ime.Name = "txtbox_ime";
            this.txtbox_ime.Size = new System.Drawing.Size(170, 32);
            this.txtbox_ime.TabIndex = 2;
            // 
            // btn_play_Again
            // 
            this.btn_play_Again.BackColor = System.Drawing.Color.LightSalmon;
            this.btn_play_Again.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_play_Again.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_play_Again.Location = new System.Drawing.Point(97, 213);
            this.btn_play_Again.Name = "btn_play_Again";
            this.btn_play_Again.Size = new System.Drawing.Size(196, 80);
            this.btn_play_Again.TabIndex = 3;
            this.btn_play_Again.Text = "PLAY AGAIN";
            this.btn_play_Again.UseVisualStyleBackColor = false;
            this.btn_play_Again.Click += new System.EventHandler(this.btn_play_Again_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSalmon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(97, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 65);
            this.button1.TabIndex = 6;
            this.button1.Text = "EXIT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // IzbornikForma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(392, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_play_Again);
            this.Controls.Add(this.txtbox_ime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_new_game);
            this.Name = "IzbornikForma";
            this.Text = "IzbornikForma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbox_ime;
        private System.Windows.Forms.Button btn_new_game;
        private System.Windows.Forms.Button btn_play_Again;
        private System.Windows.Forms.Button button1;
    }
}