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
            this.SuspendLayout();
            // 
            // labelTestEnter
            // 
            this.labelTestEnter.AutoSize = true;
            this.labelTestEnter.Location = new System.Drawing.Point(103, 208);
            this.labelTestEnter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTestEnter.Name = "labelTestEnter";
            this.labelTestEnter.Size = new System.Drawing.Size(161, 26);
            this.labelTestEnter.TabIndex = 0;
            this.labelTestEnter.Text = "Enter the game";
            this.labelTestEnter.Click += new System.EventHandler(this.labelTestEnter_Click);
            // 
            // Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 300);
            this.Controls.Add(this.labelTestEnter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Title";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTestEnter;
    }
}