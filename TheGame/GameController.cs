using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public class GameController
    {
        private static Font arialFont = new Font("Arial", 42);

        private static GameState gameState = GameState.Play;
        public static GameState GameState { get { return gameState; } }
        public static Graphics g;
        private  static int intermissionCounter;
        private const int intermissionTime = 10 * Globals.SLEEPTIME;

        private static int gameOverCounter;
        private const int gameOverTime = 5 * Globals.SLEEPTIME;

        public static int gameTick = 0;

        private static bool gameOverSoundPlayed = false;

        public static void Start()
        {
            SoundManager.InitSound();
            gameOverSoundPlayed = false;
            gameState = GameState.Play;
            gameTick = 0;
            gameOverCounter = 0;
            Battlefiled.ResetScore();
            Battlefiled.lastMaxShieldTime = 0;
            SoundManager.PlayStart();
            Battlefiled.loadLevel();
        }

        public static void Update()
        {
            switch (gameState)
            {
                case GameState.Play:
                    gameTick++;
                    Battlefiled.UpdateG();
                    break;
                case GameState.Inter:
                    IntermissionUpdate();
                    break;
                case GameState.Over:
                    GameOverUpdate();
                    break;
                case GameState.Win:
                    GameWonUpdate();
                    break;

            }

        }

        private static void IntermissionUpdate()
        {
            intermissionCounter++;
            Bitmap bmpog = Properties.Resources.Intermission;
            Bitmap bmp = new Bitmap(bmpog, new Size(bmpog.Width / 2, bmpog.Height / 2));
            int x = 400 / 2 - bmp.Width / 2;
            int y = 400 / 2 - bmp.Height / 2;
            g.DrawString(Battlefiled.enemyList[0].ToString(), arialFont, Brushes.White, new Point(400/2, 85));
            g.DrawString(Battlefiled.enemyList[1].ToString(), arialFont, Brushes.White, new Point(400/2, 160));
            g.DrawString(Battlefiled.enemyList[2].ToString(), arialFont, Brushes.White, new Point(400/2, 230));
            g.DrawString(Battlefiled.enemyList[1].ToString(), arialFont, Brushes.White, new Point(400/2, 310));
            g.DrawImage(bmp, x, y);
            if (intermissionCounter < intermissionTime) return;
            Battlefiled.increaseLevel();
            Battlefiled.loadLevel();
            if (gameState == GameState.Win) return;
            SoundManager.PlayStart();
            intermissionCounter = 0;
            gameTick = 0;
            gameState = GameState.Play;
        }

        private static void GameOverUpdate()
        {
            if (!gameOverSoundPlayed) { gameOverSoundPlayed = true; SoundManager.PlayGameOver(); }
            Bitmap bmpog = Properties.Resources.Intermission;
            Bitmap bmp = new Bitmap(bmpog, new Size(bmpog.Width / 2, bmpog.Height / 2));
            int x = 400 / 2 - bmp.Width / 2;
            int y = 400 / 2 - bmp.Height / 2;
            gameOverCounter++;
            if (gameOverCounter < gameOverTime)
            {
                gameTick++;
                Battlefiled.UpdateG();
                g.DrawImage(bmp, x, y);
                return;
            } else
            {
                g.DrawImage(bmp, x, y);
            }
        }

        private static void GameWonUpdate()
        {
            if (!gameOverSoundPlayed) { gameOverSoundPlayed = true; SoundManager.PlayStart(); }
            Bitmap bmpog = Properties.Resources.EndGame;
            Bitmap bmp = new Bitmap(bmpog, new Size(bmpog.Width / 2, bmpog.Height / 2));
            int x = 400 / 2 - bmp.Width / 2;
            int y = 400 / 2 - bmp.Height / 2;
            gameOverCounter++;
            if (gameOverCounter < gameOverTime)
            {
                gameTick++;
                Battlefiled.UpdateG();
                g.DrawImage(bmp, x, y);
                return;
            }
            else
            {
                g.DrawImage(bmp, x, y);
            }
        }

        public static void ChangeToIntermission()
        {
            gameState = GameState.Inter;
        }

        public static void ChangeToGM()
        {
            gameState = GameState.Over;
        }

        public static void ChangeToWon()
        {
            gameState = GameState.Win;
        }

        public static void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gameState == GameState.Inter) { intermissionCounter = intermissionTime-1; };
                if (gameState == GameState.Over) { gameOverCounter = 0; gameOverSoundPlayed = false;}
                if (gameState == GameState.Win) { gameOverSoundPlayed = false;}
            }
            if (Globals.DEBUG)
            {
                if (e.KeyCode == Keys.D0)
                {
                    ChangeToWon();
                }
            }
        }
    }
}
