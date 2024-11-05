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
        public static Graphics g;
        private static int intermissionCounter;
        private static int intermissionTime = 10 * Globals.SLEEPTIME;
        public static int gameTick = 0;

        private static bool skipInter = false;
        public static void Start()
        {
            GameObjectManager.lastMaxShieldTime = 0;
            SoundManager.PlayStart();
            GameObjectManager.loadLevel();
            GameObjectManager.CreatePlyTank(10 * 32, 24 * 32);
            GameObjectManager.CreateArmorEnemyTank(2 * 32, 3 * 32);
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
            }

        }

        private static void IntermissionUpdate()
        {
            intermissionCounter++;
            if (intermissionCounter < intermissionTime || !skipInter) return;
            skipInter = false;
            GameObjectManager.increaseLevel();
            GameObjectManager.CreatePlyTank(10 * 32, 24 * 32);
            gameTick = 0;
            gameState = GameState.Play;
        }

        public static void ChangeToIntermission()
        {
            gameState = GameState.Inter;
        }

        public static void ChangeToGM()
        {
            gameState = GameState.Over;
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
