using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheGame.GameObjects.StaticObject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TheGame
{
    public static class GameObjectManager
    {
        private static int totalPoints = 0;

        private static Random r = new Random();
        private static int playerLife = 2;
        public static int PlayerLife { get { return playerLife; } }
        public static int lastMaxShieldTime;
        private static bool playerRespawning;
        private static int level = 0;

        public static int Level { get { return level; } }
        private static List<NoMovingObj> walls = new List<NoMovingObj>();
        private static List<NoMovingObj> bushes = new List<NoMovingObj>();
        private static List<NoMovingObj> water = new List<NoMovingObj>();
        private static List<Powerup> powerups = new List<Powerup>();
        private static List<Shield> activeShields = new List<Shield>();
        private static HQ plyBase;
        private static PlyTank player;


        private static List<Bullet> bullets = new List<Bullet>();
        private static List<Explosion> explosions = new List<Explosion>();

        private static List<BaseEnemyTank> enemyTanks = new List<BaseEnemyTank>();

        private static List<BaseEnemyTank> pendingTanks = new List<BaseEnemyTank>();
        

        public static void loadMap(int map)
        {
            switch (map)
            {
                case 0:
                    genMap(Properties.Resources.map0.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                    break;
            }
        }

        public static void loadLevel()
        {
            loadMap(Level);
        }

        public static void increaseLevel()
        {
            level++;
        }

        public static PlyTank getPlayer() { return player; }

        static void genMap(string[] data)
        {
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    char curI = data[i][j];
                    if (curI == 'd')
                    {
                        addWall(new BrickWall(j * 16, i * 16));
                    }
                    else if (curI == 'i') 
                    {
                        addWall(new IronBlock(j * 16, i * 16));
                    }
                }
            }
            Console.WriteLine(walls.ToString());
        }

        public static void addWall(object wall)
        {
            walls.Add((NoMovingObj)wall);
        }

        public static void removeWall(object wall)
        {
            walls.Remove((NoMovingObj)wall);
        }

        public static void UpdateG()
        {
            if (playerLife <= 0)
            {
                GameFramework.ChangeToGM();
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
            }
            CheckAndDestoryBullets();

            foreach (Explosion explode in explosions)
            {
                explode.Update();
            }
            CheckAndDestoryExplosions();

            if (player.HP < 1 || playerRespawning)
            {
                if (!playerRespawning) { playerLife -= 1; }
                playerRespawning = true;
                player.Death();
            }

            foreach (BaseEnemyTank tank in enemyTanks)
            {
                tank.Update();
            }

            foreach (NoMovingObj wall in walls)
            {
                wall.Update();
            }

            player.Update();

            foreach (Shield shield in activeShields)
            {
                shield.Update();
            }
            CheckAndDestoryShields();
        }

        public static void PlayerFinishedRespawning()
        {
            playerRespawning = false;
        }

        private static void CheckAndDestoryBullets()
        {
            List<Bullet> needToDestory = new List<Bullet>();

            foreach (Bullet bullet in bullets)
            {
                if (bullet.IsDestory)
                {
                    needToDestory.Add(bullet);
                }
            }
            foreach (Bullet bullet in needToDestory)
            {
                bullets.Remove(bullet);
            }
        }

        private static void CheckAndDestoryExplosions()
        {
            List<Explosion> needToDestory = new List<Explosion>();
            foreach (Explosion explode in explosions)
            {
                if (explode.IsDestory)
                {
                    needToDestory.Add(explode);
                }
            }
            foreach (Explosion explode in needToDestory)
            {
                explosions.Remove(explode);
            }
        }
        
        private static void CheckAndDestoryShields()
        {
            List<Shield> needToDestory = new List<Shield>();
            foreach (Shield shield in activeShields)
            {
                if (shield.Following.ShieldTime <= 0)
                {
                    needToDestory.Add(shield);
                }
            }
            foreach (Shield shield in needToDestory)
            {
                activeShields.Remove(shield);
            }
        }

        public static void BreakWall(Direction dir, BrickWall wall, int power = 1)
        {
            if (power < 3) {
                switch (dir)
                {
                    case Direction.Up:
                        switch (wall.getState())
                        {
                            case 0:
                                wall.setState(BrickState.D); break;
                            case 1:
                                wall.setState(BrickState.TR); break;
                            case 2:
                                wall.setState(BrickState.TL); break;
                            default:
                                removeWall(wall); break;
                        }
                        break;

                    case Direction.Down:
                        switch (wall.getState())
                        {
                            case 0:
                                wall.setState(BrickState.U); break;
                            case 1:
                                wall.setState(BrickState.BR); break;
                            case 2:
                                wall.setState(BrickState.BL); break;
                            default:
                                removeWall(wall); break;
                        }
                        break;

                    case Direction.Left:
                        switch (wall.getState())
                        {
                            case 0:
                                wall.setState(BrickState.R); break;
                            case 4:
                                wall.setState(BrickState.TL); break;
                            case 3:
                                wall.setState(BrickState.BL); break;
                            default:
                                removeWall(wall); break;
                        }
                        break;

                    case Direction.Right:
                        switch (wall.getState())
                        {
                            case 0:
                                wall.setState(BrickState.L); break;
                            case 4:
                                wall.setState(BrickState.TR); break;
                            case 3:
                                wall.setState(BrickState.BR); break;
                            default: removeWall(wall); break;
                        }
                        break;
                }
            }
            else { removeWall(wall); }
        }

        public static void CreateBullet(int x, int y, int tag, Direction dir,int power)
        {
            Bullet bullet = new Bullet(x, y, 7, dir, tag,power);
            bullets.Add(bullet);
        }

        public static void CreateExplosion(int x, int y)
        {
            Explosion exp = new Explosion(x, y);
            explosions.Add(exp);
        }

        public static void CreateShield(int x, int y, Tank tankToFollow)
        {
            Shield newShield = new Shield(x, y, tankToFollow);
            activeShields.Add(newShield);
        }

        public static void CreatePowerup(int x, int y, PowerupID powerup)
        {
            Powerup pwup = new Powerup(x, y, powerup);
            powerups.Add(pwup);
        }

        public static void CreatePlyTank(int x, int y)
        {
            player = new PlyTank(x, y);
        }

        public static void CreateHQ(int x, int y)
        {
            plyBase = new HQ(x, y);
        }

        public static void CreateTestEnemyTank(int x, int y)
        {
            BaseEnemyTank tank = new BaseEnemyTank(x, y, 2);
            enemyTanks.Add(tank);
        }

        public static void CreateNormalEnemyTank(int x, int y)
        {
            BasicTank tank = new BasicTank(x, y);
            enemyTanks.Add(tank);
        }

        public static void CreateFastEnemyTank(int x, int y)
        {
            FastTank tank = new FastTank(x, y);
            enemyTanks.Add(tank);
        }

        public static void CreatePowerEnemyTank(int x, int y)
        {
            PowerTank tank = new PowerTank(x, y);
            enemyTanks.Add(tank);
        }

        public static void CreateArmorEnemyTank(int x, int y)
        {
            ArmorTank tank = new ArmorTank(x, y);
            enemyTanks.Add(tank);
        }

        public static void RemoveTank(BaseEnemyTank tank, bool grantPoints=true)
        {
            if (tank.CanDropPowerup)
            {

            }
            if (grantPoints)
            {
                if (tank is BasicTank) totalPoints += 100;
                else if (tank is FastTank) totalPoints += 200;
                else if (tank is PowerTank) totalPoints += 300;
                else if (tank is ArmorTank) totalPoints += 400;
                else totalPoints += 100;
            }
            enemyTanks.Remove(tank);
        }

        public static void RemoveShield(Shield shield)
        {
            activeShields.Remove(shield);
        }

        public static void HurtTank(BaseEnemyTank tank, int power)
        {
            tank.HP -= power;
            if (tank.HP <= 0) RemoveTank(tank);
        }

        public static NoMovingObj IsCollidedAnyWall(Rectangle hitbox)
        {
            foreach(NoMovingObj wall in walls)
            {
                if (wall.GetRectangle().IntersectsWith(hitbox))
                {
                    return wall;
                }
            }
            return null;
        }

        public static NoMovingObj[] CollidedWalls(Rectangle hitbox)
        {
            NoMovingObj[] toReturn = new NoMovingObj[8];
            int curr = 0;
            foreach (NoMovingObj wall in walls)
            {
                if (wall.GetRectangle().IntersectsWith(hitbox))
                {
                    toReturn[curr] = wall;
                    curr++;
                }
            }
            if (toReturn[0] == null) { return null; }
            return toReturn;
        }

        public static Bullet IsCollideBullet(Rectangle hitbox, int tag)
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.Tag != tag && bullet.GetRectangle().IntersectsWith(hitbox))
                {
                    bullet.IsDestory = true;
                    return bullet;
                }
            }
            return null;
        }

        public static bool IsCollidedBase(Rectangle hitbox)
        {
            if (plyBase.GetRectangle().IntersectsWith(hitbox))
            {
                return true;
            }
            return false;
        }

        public static bool IsCollidedPly(Rectangle hitbox)
        {
            if (player.GetRectangle().IntersectsWith(hitbox))
            {
                return true;
            }
            return false;
        }

        public static BaseEnemyTank IsCollidedTank(Rectangle hitbox)
        {
            foreach(BaseEnemyTank tank in enemyTanks)
            {
                if (tank.GetRectangle().IntersectsWith(hitbox))
                {
                    return tank;
                }
            }
            return null;
        }

        public static void KeyDown(KeyEventArgs args)
        {
            player.KeyDown(args);

        }

        public static void KeyUp(KeyEventArgs args)
        {
            player.KeyUp(args);

        }

        private static float GetRanCoord()
        {
            return r.Next(64, 736);
        }
    }
}
