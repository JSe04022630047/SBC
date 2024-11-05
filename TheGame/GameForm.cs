using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheGame.Properties;

namespace TheGame
{
    public partial class GameForm : Form
    {
        private Thread t;
        private ManualResetEvent mrse = new ManualResetEvent(true);
        private static Graphics windowsG;
        private static Bitmap tempBitmap;
        private bool paused=false;
        public string name;
        public GameForm(string name)
        {
            InitializeComponent();
            windowsG = panelGameArea.CreateGraphics();
            tempBitmap = new Bitmap(800, 800);
            GameFramework.g = Graphics.FromImage(tempBitmap);

            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
            this.name = name;
            labelPlyName.Text = name;
        }

        private void GameMainThread()
        {
            GameFramework.Start();

            while (true)
            {
                mrse.WaitOne();
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update();
                windowsG.DrawImage(tempBitmap, 0, 0);
                WriteLabelSafe(labelX, GameObjectManager.getPlayer().X.ToString());
                WriteLabelSafe(labelY, GameObjectManager.getPlayer().Y.ToString());
                WriteLabelSafe(labelPlyHP, GameObjectManager.getPlayer().HP.ToString());
                WriteLabelSafe(labelLife, GameObjectManager.PlayerLife.ToString());
                WriteProgressBarSafe(progressRespawnTime, GameObjectManager.getPlayer().respawnTimeCounter, GameObjectManager.getPlayer().respawnTime);
                WriteProgressBarSafe(progressBarShield, GameObjectManager.getPlayer().ShieldTime, GameObjectManager.lastMaxShieldTime);
                Thread.Sleep(Globals.SLEEPTIME);
            }
        }
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void labelEnemyLeft_TextChanged(object sender, EventArgs e)
        {
            //progressEnemyLeft.SetBounds(0,Game)
        }

        #region Hud

        public delegate void SafeSetLabelDelegate(Label label, string text);

        public delegate void SafeSetProgressBarDelegate(ProgressBar progressBar, int value, int max);

        private void WriteLabelSafe(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                var d = new SafeSetLabelDelegate(WriteLabelSafe);
                label.Invoke(d, new object[] { label, text });
            }
            else
            {
                label.Text = text;
            }
        }

        private void WriteProgressBarSafe(ProgressBar progressBar, int value, int max)
        {
            if (progressBar.InvokeRequired)
            {
                var d = new SafeSetProgressBarDelegate(WriteProgressBarSafe);
                progressBar.Invoke(d, new object[] { progressBar, value, max });
            }
            else
            {
                if (value == 0) { progressBar.Visible = false; return; }
                else { progressBar.Visible = true; }
                progressBar.Maximum = max;
                progressBar.Value = value;
            }
        }

        #endregion

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
            if (e.KeyCode == Keys.P)
            {
                if (!paused)
                {
                    paused = true;
                    mrse.Reset();
                }
                else
                {
                    paused = false;
                    mrse.Set();
                }
            }
            if (e.KeyCode == Keys.Oemplus) GameFramework.ChangeToIntermission();
            GameFramework.KeyDown(e);

        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
