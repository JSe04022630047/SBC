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
            tempBitmap = new Bitmap(400, 400);
            GameController.g = Graphics.FromImage(tempBitmap);

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
            GameController.Start();

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
                switch (GameController.GameState)
                {
                    case GameState.Play:
                        GameController.g.Clear(Color.Black);
                        GameController.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);

#pragma warning disable CS0162 // Unreachable code detected, shutup its here for a reason
                        if (Globals.DEBUG)
                        {
                            WriteLabelSafe(labelX, Battlefiled.getPlayer().X.ToString());
                            WriteLabelSafe(labelY, Battlefiled.getPlayer().Y.ToString());
                        }
#pragma warning restore CS0162 // Unreachable code detected

                        WriteLabelSafe(labelLevelCount, Battlefiled.Level.ToString());
                        WriteLabelSafe(labelPlyHP, Battlefiled.getPlayer().HP.ToString());
                        WriteLabelSafe(labelLife, Battlefiled.PlayerLife.ToString());
                        WriteLabelSafe(labelScore, Battlefiled.Points.ToString());
                        WriteLabelSafe(labelEnemyLeft, Battlefiled.EnemyLeft.ToString());
                        WriteProgressBarSafe(progressRespawnTime, Battlefiled.getPlayer().respawnTimeCounter, Battlefiled.getPlayer().respawnTime);
                        WriteProgressBarSafe(progressBarShield, Battlefiled.getPlayer().ShieldTime, Battlefiled.lastMaxShieldTime);
                        Highscores.AddScore(name, Battlefiled.Points);

                        break;
                    case GameState.Inter:
                        GameController.g.Clear(Color.Black);
                        GameController.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        break;
                    case GameState.Over:
                        GameController.g.Clear(Color.Black);
                        GameController.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        gameover = true;
                        break;
                    case GameState.Win:
                        GameController.g.Clear(Color.Black);
                        GameController.Update();
                        windowsG.DrawImage(tempBitmap, 0, 0);
                        WriteProgressBarSafe(progressRespawnTime, 0, 0);
                        WriteProgressBarSafe(progressBarShield, 0, 0);
                        gameover = true;
                        won = true;
                        break;
                }
                Thread.Sleep(Globals.SLEEPTIME);
            }
            
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
            Battlefiled.KeyDown(e);
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
                if (e.KeyCode == Keys.Oemplus) GameController.ChangeToIntermission();
                if (e.KeyCode == Keys.OemMinus) GameController.ChangeToGM();
            }
            GameController.KeyDown(e);
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
            Battlefiled.KeyUp(e);
        }
    }
}
