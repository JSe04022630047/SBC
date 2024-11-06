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
        private static GameState gameState = GameState.Play;
        public static GameState GameState { get { return gameState; } }
        public static Graphics g;
        private static int intermissionCounter;
        private static int intermissionTime = 10 * Globals.SLEEPTIME;
        public static int gameTick = 0;

        private static bool skipInter = false;
        public static void Start()
        {
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
            }

        }

        private static void IntermissionUpdate()
        {
            intermissionCounter++;
            Bitmap bmp = Properties.Resources.gameover;
            bmp.MakeTransparent(Color.Black);
            int x = 800 / 2 - Properties.Resources.gameover.Width / 2;
            int y = 800 / 2 - Properties.Resources.gameover.Height / 2;
            g.DrawImage(bmp, x, y);
            if (intermissionCounter < intermissionTime || !skipInter) return;
            skipInter = false;
            GameObjectManager.increaseLevel();
            GameObjectManager.loadLevel();
            CreatePlayerTank();
            intermissionCounter = 0;
            gameTick = 0;
            gameState = GameState.Play;
        }

        private static void GameOverUpdate()
        {
            Bitmap bmp = Properties.Resources.gameover;
            bmp.MakeTransparent(Color.Black);
            int x = 800 / 2 - Properties.Resources.gameover.Width / 2;
            int y = 800 / 2 - Properties.Resources.gameover.Height / 2;
            g.DrawImage(bmp, x, y);
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
                skipInter = true;
            }
        }
    }
}
