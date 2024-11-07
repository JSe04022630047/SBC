namespace TheGame
{
    partial class GameForm
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
            this.panelGameArea = new System.Windows.Forms.Panel();
            this.panelStat = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.progressEnemyLeft = new System.Windows.Forms.ProgressBar();
            this.labelPlyName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLife = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelEnemyLeft = new System.Windows.Forms.Label();
            this.labelPlyHP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLevelCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPauseInstruct = new System.Windows.Forms.Label();
            this.progressRespawnTime = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBarShield = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.panelStat.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGameArea
            // 
            this.panelGameArea.BackColor = System.Drawing.Color.DimGray;
            this.panelGameArea.Location = new System.Drawing.Point(12, 12);
            this.panelGameArea.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelGameArea.Name = "panelGameArea";
            this.panelGameArea.Size = new System.Drawing.Size(400, 400);
            this.panelGameArea.TabIndex = 0;
            // 
            // panelStat
            // 
            this.panelStat.BackColor = System.Drawing.Color.Bisque;
            this.panelStat.Controls.Add(this.label7);
            this.panelStat.Controls.Add(this.labelScore);
            this.panelStat.Controls.Add(this.labelPauseInstruct);
            this.panelStat.Controls.Add(this.labelY);
            this.panelStat.Controls.Add(this.labelX);
            this.panelStat.Controls.Add(this.progressEnemyLeft);
            this.panelStat.Controls.Add(this.labelPlyName);
            this.panelStat.Controls.Add(this.label5);
            this.panelStat.Controls.Add(this.labelLife);
            this.panelStat.Controls.Add(this.label3);
            this.panelStat.Controls.Add(this.labelEnemyLeft);
            this.panelStat.Controls.Add(this.labelPlyHP);
            this.panelStat.Controls.Add(this.label2);
            this.panelStat.Controls.Add(this.labelLevelCount);
            this.panelStat.Controls.Add(this.label1);
            this.panelStat.Location = new System.Drawing.Point(420, 12);
            this.panelStat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelStat.Name = "panelStat";
            this.panelStat.Size = new System.Drawing.Size(132, 287);
            this.panelStat.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Score";
            // 
            // labelScore
            // 
            this.labelScore.BackColor = System.Drawing.Color.SeaShell;
            this.labelScore.Location = new System.Drawing.Point(8, 226);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(113, 21);
            this.labelScore.TabIndex = 6;
            this.labelScore.Text = "0123456789";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(38, 181);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(51, 20);
            this.labelY.TabIndex = 5;
            this.labelY.Text = "label4";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(38, 161);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(51, 20);
            this.labelX.TabIndex = 5;
            this.labelX.Text = "label4";
            // 
            // progressEnemyLeft
            // 
            this.progressEnemyLeft.Location = new System.Drawing.Point(7, 124);
            this.progressEnemyLeft.Name = "progressEnemyLeft";
            this.progressEnemyLeft.Size = new System.Drawing.Size(120, 23);
            this.progressEnemyLeft.TabIndex = 4;
            // 
            // labelPlyName
            // 
            this.labelPlyName.Location = new System.Drawing.Point(1, 258);
            this.labelPlyName.Name = "labelPlyName";
            this.labelPlyName.Size = new System.Drawing.Size(124, 21);
            this.labelPlyName.TabIndex = 3;
            this.labelPlyName.Text = "12characters";
            this.labelPlyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "1UPs";
            // 
            // labelLife
            // 
            this.labelLife.BackColor = System.Drawing.SystemColors.Control;
            this.labelLife.Location = new System.Drawing.Point(58, 70);
            this.labelLife.Name = "labelLife";
            this.labelLife.Size = new System.Drawing.Size(70, 20);
            this.labelLife.TabIndex = 1;
            this.labelLife.Text = "3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enemy";
            // 
            // labelEnemyLeft
            // 
            this.labelEnemyLeft.BackColor = System.Drawing.SystemColors.Control;
            this.labelEnemyLeft.Location = new System.Drawing.Point(57, 101);
            this.labelEnemyLeft.Name = "labelEnemyLeft";
            this.labelEnemyLeft.Size = new System.Drawing.Size(70, 20);
            this.labelEnemyLeft.TabIndex = 1;
            this.labelEnemyLeft.Text = "10";
            // 
            // labelPlyHP
            // 
            this.labelPlyHP.BackColor = System.Drawing.SystemColors.Control;
            this.labelPlyHP.Location = new System.Drawing.Point(58, 41);
            this.labelPlyHP.Name = "labelPlyHP";
            this.labelPlyHP.Size = new System.Drawing.Size(70, 20);
            this.labelPlyHP.TabIndex = 1;
            this.labelPlyHP.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "HP";
            // 
            // labelLevelCount
            // 
            this.labelLevelCount.BackColor = System.Drawing.SystemColors.Control;
            this.labelLevelCount.Location = new System.Drawing.Point(58, 12);
            this.labelLevelCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLevelCount.Name = "labelLevelCount";
            this.labelLevelCount.Size = new System.Drawing.Size(70, 20);
            this.labelLevelCount.TabIndex = 0;
            this.labelLevelCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Level";
            // 
            // labelPauseInstruct
            // 
            this.labelPauseInstruct.AutoSize = true;
            this.labelPauseInstruct.Location = new System.Drawing.Point(3, 150);
            this.labelPauseInstruct.Name = "labelPauseInstruct";
            this.labelPauseInstruct.Size = new System.Drawing.Size(129, 20);
            this.labelPauseInstruct.TabIndex = 2;
            this.labelPauseInstruct.Text = "Press P to pause";
            // 
            // progressRespawnTime
            // 
            this.progressRespawnTime.Location = new System.Drawing.Point(424, 383);
            this.progressRespawnTime.Name = "progressRespawnTime";
            this.progressRespawnTime.Size = new System.Drawing.Size(120, 23);
            this.progressRespawnTime.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Respawn Timer";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarShield
            // 
            this.progressBarShield.Location = new System.Drawing.Point(425, 330);
            this.progressBarShield.Name = "progressBarShield";
            this.progressBarShield.Size = new System.Drawing.Size(120, 23);
            this.progressBarShield.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Shield Timer";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 427);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBarShield);
            this.Controls.Add(this.panelStat);
            this.Controls.Add(this.progressRespawnTime);
            this.Controls.Add(this.panelGameArea);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GameForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.panelStat.ResumeLayout(false);
            this.panelStat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGameArea;
        private System.Windows.Forms.Panel panelStat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPlyHP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLevelCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLife;
        private System.Windows.Forms.Label labelEnemyLeft;
        private System.Windows.Forms.ProgressBar progressEnemyLeft;
        private System.Windows.Forms.Label labelPlyName;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelPauseInstruct;
        private System.Windows.Forms.ProgressBar progressRespawnTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBarShield;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label label7;
    }
}