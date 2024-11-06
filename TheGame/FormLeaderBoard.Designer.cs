namespace TheGame
{
    partial class FormLeaderBoard
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelSt = new System.Windows.Forms.Label();
            this.textFirst = new System.Windows.Forms.TextBox();
            this.textSecond = new System.Windows.Forms.TextBox();
            this.textThird = new System.Windows.Forms.TextBox();
            this.textForth = new System.Windows.Forms.TextBox();
            this.textFifth = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(155, 9);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(172, 33);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Highscores";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(161, 403);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSt
            // 
            this.labelSt.AutoSize = true;
            this.labelSt.Location = new System.Drawing.Point(45, 85);
            this.labelSt.Name = "labelSt";
            this.labelSt.Size = new System.Drawing.Size(22, 180);
            this.labelSt.TabIndex = 3;
            this.labelSt.Text = "1.\r\n\r\n2.\r\n\r\n3.\r\n\r\n4.\r\n\r\n5.";
            // 
            // textFirst
            // 
            this.textFirst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFirst.Location = new System.Drawing.Point(73, 81);
            this.textFirst.Name = "textFirst";
            this.textFirst.ReadOnly = true;
            this.textFirst.Size = new System.Drawing.Size(353, 26);
            this.textFirst.TabIndex = 4;
            // 
            // textSecond
            // 
            this.textSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSecond.Location = new System.Drawing.Point(73, 121);
            this.textSecond.Name = "textSecond";
            this.textSecond.ReadOnly = true;
            this.textSecond.Size = new System.Drawing.Size(353, 26);
            this.textSecond.TabIndex = 4;
            // 
            // textThird
            // 
            this.textThird.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textThird.Location = new System.Drawing.Point(73, 164);
            this.textThird.Name = "textThird";
            this.textThird.ReadOnly = true;
            this.textThird.Size = new System.Drawing.Size(353, 26);
            this.textThird.TabIndex = 4;
            // 
            // textForth
            // 
            this.textForth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textForth.Location = new System.Drawing.Point(73, 201);
            this.textForth.Name = "textForth";
            this.textForth.ReadOnly = true;
            this.textForth.Size = new System.Drawing.Size(353, 26);
            this.textForth.TabIndex = 4;
            // 
            // textFifth
            // 
            this.textFifth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textFifth.Location = new System.Drawing.Point(73, 242);
            this.textFifth.Name = "textFifth";
            this.textFifth.ReadOnly = true;
            this.textFifth.Size = new System.Drawing.Size(353, 26);
            this.textFifth.TabIndex = 4;
            // 
            // FormLeaderBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.textFifth);
            this.Controls.Add(this.textForth);
            this.Controls.Add(this.textThird);
            this.Controls.Add(this.textSecond);
            this.Controls.Add(this.textFirst);
            this.Controls.Add(this.labelSt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormLeaderBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leaderboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLeaderBoard_FormClosing);
            this.Load += new System.EventHandler(this.FormLeaderBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelSt;
        private System.Windows.Forms.TextBox textFirst;
        private System.Windows.Forms.TextBox textSecond;
        private System.Windows.Forms.TextBox textThird;
        private System.Windows.Forms.TextBox textForth;
        private System.Windows.Forms.TextBox textFifth;
    }
}