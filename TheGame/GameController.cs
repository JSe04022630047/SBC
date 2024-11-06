using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public class GameFramework
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

        private Label labelInstruction;

        public static void Start()
        {
            SoundManager.InitSound();
            gameOverSoundPlayed = false;
            gameState = GameState.Play;
            gameTick = 0;
            gameOverCounter = 0;
            GameObjectManager.ResetScore();
            GameObjectManager.lastMaxShieldTime = 0;
            SoundManager.PlayStart();
            GameObjectManager.loadLevel();
            CreatePlayerTank();
        }

        public static void Update()
        {
            switch (gameState)
            {
                case GameState.Play:
                    gameTick++;
                    GameObjectManager.UpdateG();
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
            Bitmap bmp = Properties.Resources.Intermission;
            bmp.MakeTransparent(Color.Black);
            int x = 800 / 2 - Properties.Resources.gameover.Width / 2;
            int y = 800 / 2 - Properties.Resources.gameover.Height / 2;
            g.DrawString(GameObjectManager.enemyList[0].ToString(), arialFont, Brushes.White, new Point(400, 220));
            g.DrawString(GameObjectManager.enemyList[1].ToString(), arialFont, Brushes.White, new Point(400, 350));
            g.DrawString(GameObjectManager.enemyList[2].ToString(), arialFont, Brushes.White, new Point(400, 500));
            g.DrawString(GameObjectManager.enemyList[1].ToString(), arialFont, Brushes.White, new Point(400, 650));
            g.DrawImage(bmp, x, y);
            if (intermissionCounter < intermissionTime) return;
            GameObjectManager.increaseLevel();
            GameObjectManager.loadLevel();
            if (gameState == GameState.Win) return;
            SoundManager.PlayStart();
            CreatePlayerTank();
            intermissionCounter = 0;
            gameTick = 0;
            gameState = GameState.Play;
        }

        private static void GameOverUpdate()
        {
            if (!gameOverSoundPlayed) { gameOverSoundPlayed = true; SoundManager.PlayGameOver(); }
            int x = 800 / 2 - Properties.Resources.gameover.Width / 2;
            int y = 800 / 2 - Properties.Resources.gameover.Height / 2;
            gameOverCounter++;
            if (gameOverCounter < gameOverTime)
            {
                gameTick++;
                GameObjectManager.UpdateG();
                Bitmap bmp = Properties.Resources.gameover;
                bmp.MakeTransparent(Color.Black);

                g.DrawImage(bmp, x, y);
                return;
            } else
            {
                Bitmap bmp = Properties.Resources.gameover;
                bmp.MakeTransparent(Color.Black);

                g.DrawImage(bmp, x, y);
            }
        }

        private static void GameWonUpdate()
        {
            if (!gameOverSoundPlayed) { gameOverSoundPlayed = true; SoundManager.PlayStart(); }
            int x = 800 / 2 - Properties.Resources.EndGame.Width / 2;
            int y = 800 / 2 - Properties.Resources.EndGame.Height / 2;
            gameOverCounter++;
            if (gameOverCounter < gameOverTime)
            {
                gameTick++;
                GameObjectManager.UpdateG();
                Bitmap bmp = Properties.Resources.EndGame;
                bmp.MakeTransparent(Color.Black);

                g.DrawImage(bmp, x, y);
                return;
            }
            else
            {
                Bitmap bmp = Properties.Resources.EndGame;
                bmp.MakeTransparent(Color.Black);
                g.DrawImage(bmp, x, y);
            }

        }

        private static void CreatePlayerTank()
        {
            GameObjectManager.CreatePlyTank(11 * 32, 24 * 32);
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
                if (gameState == GameState.Inter) { intermissionCounter = intermissionTime; };
                if (gameState == GameState.Over) { gameOverCounter = 0; gameOverSoundPlayed = false; }
                if (gameState == GameState.Win) { gameOverSoundPlayed = false; }
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
