using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public partial class FormLeaderBoard : Form
    {
        TitleForm previous;
        Font font1;
        Font font2;
        public FormLeaderBoard(TitleForm previous)
        {
            InitializeComponent();
            this.previous = previous;
            font1 = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            font2 = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
        }

        private void FormLeaderBoard_Load(object sender, EventArgs e)
        {
            TextBox[] textBoxes = { textFirst, textSecond, textThird, textForth, textFifth };
            if (Highscores.ScoreList.Count() == 0)
            {
                #region Add controls hack
                List<Control> toHide = new List<Control>();
                toHide.AddRange(textBoxes);
                toHide.Add(labelSt);
                
                foreach (Control control in toHide)
                {
                    control.Visible = false;
                }

                Label labelNoCurrentScore1 = new System.Windows.Forms.Label()
                {
                    AutoSize = true,
                    Font = font1,
                    Name = "labelNoCurrentScore1",
                    Location = new System.Drawing.Point(45, 19),
                    Size = new System.Drawing.Size(375, 37),
                    Text = "There aren\'t any scores"
                };
                Label labelNoCurrentScore2 = new System.Windows.Forms.Label()
                {
                    AutoSize = true,
                    Font = font1,
                    Name = "labelNoCurrentScore2",
                    Location = new System.Drawing.Point(110, 56),
                    Size = new System.Drawing.Size(245, 37),
                    Text = "at the moment."
                };
                Label labelNoCurrentScore3 = new System.Windows.Forms.Label()
                {
                    AutoSize = true,
                    Font = font2,
                    Name = "labelNoCurrentScore3",
                    Location = new System.Drawing.Point(97, 233),
                    Size = new System.Drawing.Size(270, 20),
                    Text = "Play a few game and come back.."

                };
                Panel panelNoCurrentScore = new Panel()
                {
                    Location = new System.Drawing.Point(12, 54),
                    Name = "panelNoCurrentScore",
                    Size = new System.Drawing.Size(450, 330),
                };
                this.Controls.Add(panelNoCurrentScore);
                panelNoCurrentScore.Controls.Add(labelNoCurrentScore1);
                panelNoCurrentScore.Controls.Add(labelNoCurrentScore2);
                panelNoCurrentScore.Controls.Add(labelNoCurrentScore3);

                #endregion
            }
            else
            {
                int index = 0;
                foreach (var row in Highscores.ScoreList)
                {
                    textBoxes[index].Text = String.Format("{0}\t{1}", new object[] { row.Key, row.Value });
                    index++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            previous.Show();
            this.Dispose();
        }

        private void FormLeaderBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
            this.Dispose();
        }
    }
}
