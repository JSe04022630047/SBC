namespace TheGame
{
    partial class TitleForm
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
            this.labelTestEnter = new System.Windows.Forms.Label();
            this.labelLeaderBoard = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTestEnter
            // 
            this.labelTestEnter.AutoSize = true;
            this.labelTestEnter.Location = new System.Drawing.Point(117, 200);
            this.labelTestEnter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTestEnter.Name = "labelTestEnter";
            this.labelTestEnter.Size = new System.Drawing.Size(161, 26);
            this.labelTestEnter.TabIndex = 0;
            this.labelTestEnter.Text = "Enter the game";
            this.labelTestEnter.Click += new System.EventHandler(this.labelTestEnter_Click);
            // 
            // labelLeaderBoard
            // 
            this.labelLeaderBoard.AutoSize = true;
            this.labelLeaderBoard.Location = new System.Drawing.Point(102, 240);
            this.labelLeaderBoard.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelLeaderBoard.Name = "labelLeaderBoard";
            this.labelLeaderBoard.Size = new System.Drawing.Size(191, 26);
            this.labelLeaderBoard.TabIndex = 0;
            this.labelLeaderBoard.Text = "View LeaderBoard";
            this.labelLeaderBoard.Click += new System.EventHandler(this.labelLeaderBoard_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TheGame.Properties.Resources.brick;
            this.pictureBox1.Location = new System.Drawing.Point(113, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(171, 148);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // TitleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 300);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelLeaderBoard);
            this.Controls.Add(this.labelTestEnter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TitleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTestEnter;
        private System.Windows.Forms.Label labelLeaderBoard;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}