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
        private bool gameover=false;
        private bool won=false;
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

            if (!Globals.DEBUG)
            {
#pragma warning disable CS0162 // Unreachable code detected
                labelX.Hide();
                labelY.Hide();
#pragma warning restore CS0162 // Unreachable code detected
            }
        }

        private void GameMainThread()
        {
            GameFramework.Start();

            int ticksToWait = 8 * Globals.SLEEPTIME;
            int ticksCounter = 0;

            while (true)
            {
                if (gameover)
                {
                    ticksCounter++;
                    if (ticksCounter > ticksToWait) { break; }
                }
                mrse.WaitOne();
                switch (GameFramework.GameState)
                {
                    case GameState.Play:
                        GameFramework.g.Clear(Color.Black);
                        GameFramework.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);

#pragma warning disable CS0162 // Unreachable code detected, shutup its here for a reason
                        if (Globals.DEBUG)
                        {
                            WriteLabelSafe(labelX, GameObjectManager.getPlayer().X.ToString());
                            WriteLabelSafe(labelY, GameObjectManager.getPlayer().Y.ToString());
                        }
#pragma warning restore CS0162 // Unreachable code detected

                        WriteLabelSafe(labelLevelCount, GameObjectManager.Level.ToString());
                        WriteLabelSafe(labelPlyHP, GameObjectManager.getPlayer().HP.ToString());
                        WriteLabelSafe(labelLife, GameObjectManager.PlayerLife.ToString());
                        WriteLabelSafe(labelScore, GameObjectManager.Points.ToString());
                        WriteLabelSafe(labelEnemyLeft, GameObjectManager.EnemyLeft.ToString());
                        WriteProgressBarSafe(progressRespawnTime, GameObjectManager.getPlayer().respawnTimeCounter, GameObjectManager.getPlayer().respawnTime);
                        WriteProgressBarSafe(progressBarShield, GameObjectManager.getPlayer().ShieldTime, GameObjectManager.lastMaxShieldTime);
                        break;
                    case GameState.Inter:
                        GameFramework.g.Clear(Color.Black);
                        GameFramework.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        break;
                    case GameState.Over:
                        GameFramework.g.Clear(Color.Black);
                        GameFramework.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        gameover = true;
                        break;
                    case GameState.Win:
                        GameFramework.g.Clear(Color.Black);
                        GameFramework.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        gameover = true;
                        won = true;
                        break;
                }
                Thread.Sleep(Globals.SLEEPTIME);
            }
            Highscores.AddScore(name, GameObjectManager.Points);
            
        }
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
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
            if (Globals.DEBUG) { 
                if (e.KeyCode == Keys.Oemplus) GameFramework.ChangeToIntermission();
                if (e.KeyCode == Keys.OemMinus) GameFramework.ChangeToGM();
            }
            GameFramework.KeyDown(e);
            if (gameover)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.t.Abort();
                    TitleForm t = new TitleForm();
                    Hide();
                    t.ShowDialog();
                    Close();
                }
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
