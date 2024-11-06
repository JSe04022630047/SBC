using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace TheGame
{
    public static class GameObjectManager
    {
        private static int totalPoints = 0;
        public static int Points {  get { return totalPoints; } }

        private static Random r = new Random();
        private static int playerLife = 2;
        public static int PlayerLife { get { return playerLife; } }
        public static int lastMaxShieldTime;
        private static bool playerRespawning;
        public static bool PlayerRespawning { get { return playerRespawning; } }
        private static int level = 0;
        public static bool HQHasShield {  get { return plyBase.ShieldTime>0; } }

        private static int enemySpawnTime = 15 * Globals.SLEEPTIME;
        private static int enemySpawnCount = 0;
        public static int EnemyCount {  get { return pendingTanks.Count; } }

        private static int preIntermissionCounter;
        private const int preIntermissionTime = 5 * Globals.SLEEPTIME;

        public static int Level { get { return level+1; } }
        private static List<NoMovingObj> walls = new List<NoMovingObj>();
        private static List<NoMovingObj> bushes = new List<NoMovingObj>();
        private static List<NoMovingObj> water = new List<NoMovingObj>();
        private static List<Powerup> powerups = new List<Powerup>();
        private static List<Shield> activeShields = new List<Shield>();
        private static List<HQShieldVisual> activeHQShields = new List<HQShieldVisual>();

        private static HQ plyBase;
        private static PlyTank player;


        private static List<Bullet> bullets = new List<Bullet>();
        private static List<Explosion> explosions = new List<Explosion>();

        private static List<BaseEnemyTank> enemyTanks = new List<BaseEnemyTank>();

        private static List<Point> enemySpawn = new List<Point>();
        private static List<int> pendingTanks = new List<int>();
        public static int[] enemyList = new int[4];
        public static int EnemyLeft { get { return pendingTanks.Count + enemyTanks.Count; } }
        

        public static void loadMap(int map)
        {
            clearLevel();
            switch (map)
            {
                case 0:
                    genMap(Properties.Resources.map0.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                    break;
                case 1:
                    genMap(Properties.Resources.map1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                    break;
                case 2:
                    genMap(Properties.Resources.map1.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
                    break;
            }
        }

        public static void clearLevel()
        {
            enemyList = new int[4];
            enemySpawnCount = 0;
            lastMaxShieldTime = 0;
            walls.Clear();
            bushes.Clear();
            water.Clear(); // in case we have to add water
            powerups.Clear();
            activeShields.Clear();
            activeHQShields.Clear();
            plyBase = null;
            player = null;
            bullets.Clear();
            explosions.Clear();
            enemyTanks.Clear();
            enemySpawn.Clear();
            pendingTanks.Clear();
        }

        public static void ResetScore()
        {
            totalPoints = 0;
        }

        public static void IncreaseSorce(int amount = 0)
        {
            totalPoints += amount;
        }

        public static void loadLevel()
        {
            if (GameFramework.GameState == GameState.Win) { return; }
            loadMap(level);
        }

        public static void increaseLevel()
        {
            level++;
            if (level > 2)
            {
                GameFramework.ChangeToWon();
            }
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
                    else if (curI == 'q')
                    {
                        CreateHQ(j * 16, i * 16);
                    }
                    else if (curI == 'm')
                    {
                        addBush(new Bush(j * 16, i * 16));
                    }
                    else if (curI == 's')
                    {
                        enemySpawn.Add(new Point(16 * j, 16 * i));
                    }
                }
            }

            string[] enemySpawnPoolStr = data[50].Split('|');
            int[] enemySpawnPool = new int[4];

            for (int i = 0; i < 4; i++) 
            {
                if (enemySpawnPoolStr[i] == null) break;
                enemySpawnPool[i] = Convert.ToInt32(enemySpawnPoolStr[i]);
                enemyList[i] = enemySpawnPool[i];
            }

            while (true)
            { 
                if (enemySpawnPool[0] == 0 && enemySpawnPool[1] == 0 && enemySpawnPool[2] == 0 && enemySpawnPool[3] == 0) break;
                // ^^^^ check if enemySpawnPool is empty, if yes just exit out of the loop

                int pick = r.Next(0, 3); // 0 = basic, 1 = fast, 2 = power, 3 = armor
                if (enemySpawnPool[pick] == 0) continue;
                enemySpawnPool[pick] -= 1;
                pendingTanks.Add(pick);
            }
            
        }

        public static void addWall(NoMovingObj wall)
        {
            walls.Add(wall);
        }
        
        public static void addBush(Bush bush)
        {
            bushes.Add(bush);
        }

        public static void removeWall(object wall)
        {
            walls.Remove((NoMovingObj)wall);
        }

        public static void UpdateG()
        {
            if (EnemyLeft == 0)
            {
                if (level < 2)
                {
                    preIntermissionCounter++;
                    if (preIntermissionCounter > preIntermissionTime)
                    {
                        preIntermissionCounter = 0;
                        GameFramework.ChangeToIntermission();
                        return;
                    }
                }
                else
                {
                    GameFramework.ChangeToWon();
                }
            }
            if (playerLife <= 0)
            {
                GameFramework.ChangeToGM();
            }

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

            List<Bullet> bulletsToCheck = new List<Bullet>(bullets);
            foreach (Bullet bullet in bulletsToCheck)
            {
                bullet.Update();
            }

            CheckAndDestoryBullets();

            foreach (BaseEnemyTank tank in enemyTanks)
            {
                tank.Update();
            }

            foreach (NoMovingObj wall in walls)
            {
                wall.Update();
            }

            plyBase.Update();
            player.Update();

            foreach (Shield shield in activeShields)
            {
                shield.Update();
            }
            foreach (HQShieldVisual shield in activeHQShields)
            {
                shield.Update();
            }
            CheckAndDestoryShields();

            foreach (Powerup pwup in powerups)
            {
                pwup.Update();
            }
            CheckAndDestoryPowerups();

            foreach (Bush bush in bushes) { bush.Update(); }

            EnemySpawn();
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
                RemoveShield(shield);
            }

            List<HQShieldVisual> needToDestory1 = new List<HQShieldVisual>();
            foreach (HQShieldVisual shield in activeHQShields)
            {
                if (shield.Following.ShieldTime <= 0)
                {
                    needToDestory1.Add(shield);
                }
            }
            foreach (HQShieldVisual shield in needToDestory1)
            {
                RemoveShield(shield);
            }
        }

        private static void CheckAndDestoryPowerups()
        {
            List<Powerup> needToDestory = new List<Powerup>();
            foreach (Powerup powerup in powerups)
            {
                if (powerup.IsDestory)
                {
                    needToDestory.Add(powerup);
                }
            }
            foreach (Powerup powerup in needToDestory)
            {
                powerups.Remove(powerup);
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

        public static void CreateBullet(int x, int y, int tag, Direction dir,int power, int speed=7)
        {
            Bullet bullet = new Bullet(x, y, speed, dir, tag,power);
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

        public static void CreateShield(int x, int y, HQ hqToFollow)
        {
            HQShieldVisual newShield = new HQShieldVisual(x, y, hqToFollow);
            activeHQShields.Add(newShield);
        }

        public static void CreatePowerup(int x, int y, PowerupID powerup)
        {
            Powerup pwup; // I don't understand why professor wants us to sperate them.
            switch (powerup)
            {
                case PowerupID.Grenade:
                    pwup = new Grenade(x, y); break;
                case PowerupID.Helmet:
                    pwup = new TankShield(x, y); break;
                case PowerupID.Shovel:
                    pwup = new HQShield(x, y); break;
                case PowerupID.Star:
                    pwup = new StarUpgrade(x, y); break;
                case PowerupID.Tank:
                    pwup = new _1Up(x, y); break;
                default:
                    pwup = new Powerup(x, y, powerup); break;
            }
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

        

        public static void CreateEnemyTank(int x, int y, int type)
        {
            BaseEnemyTank tank;
            switch (type)
            {
                case 0:
                    tank = new BasicTank(x, y); break;
                case 1:
                    tank = new FastTank(x, y); break;
                case 2:
                    tank = new PowerTank(x, y); break;
                case 3:
                    tank = new ArmorTank(x, y); break;
                default:
                    tank = new BaseEnemyTank(x, y, 1); break;

            }
            enemyTanks.Add(tank);
        }

        private static void EnemySpawn()
        {
            if (pendingTanks.Count <= 0 || enemySpawn.Count <= 0) return;

            enemySpawnCount++;
            if (enemySpawnCount < enemySpawnTime) return;

            int indexPoint = r.Next(0, enemySpawn.Count);
            Point toSpawnAt = enemySpawn[indexPoint];

            switch (pendingTanks[0])
            {
                case 0:
                    CreateEnemyTank(toSpawnAt.X, toSpawnAt.Y, pendingTanks[0]);
                    break;
                case 1:
                    CreateEnemyTank(toSpawnAt.X, toSpawnAt.Y, pendingTanks[0]);
                    break;
                case 2:
                    CreateEnemyTank(toSpawnAt.X, toSpawnAt.Y, pendingTanks[0]);
                    break;
                case 3:
                    CreateEnemyTank(toSpawnAt.X, toSpawnAt.Y, pendingTanks[0]);
                    break;
                default: // this can't happen right?
                    MessageBox.Show(pendingTanks[0].ToString() + " index does not correspond to any existing tank!\nWhat happened?");
                    CreateEnemyTank(toSpawnAt.X, toSpawnAt.Y, -1); break;
            }
            pendingTanks.RemoveAt(0);

            enemySpawnCount = 0;

        }

        public static void RemoveTank(BaseEnemyTank tank, bool grantPoints=true)
        {
            if (tank.CanDropPowerup)
            {
                CreatePowerup((int)GetRanCoord(),(int)GetRanCoord(), (PowerupID)r.Next(0, 4));
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

        public static void RemoveShield(HQShieldVisual shield)
        {
            activeHQShields.Remove(shield);
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
            NoMovingObj[] toReturn = new NoMovingObj[10];
            int index = 0;
            foreach (NoMovingObj wall in walls)
            {
                if (wall.GetRectangle().IntersectsWith(hitbox))
                {
                    toReturn[index] = wall;
                    index++;
                }
            }
            if (toReturn[0] == null) return null;
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

        public static Powerup IsCollidedPowerup(Rectangle hitbox)
        {
            foreach (Powerup pwup in powerups)
            {
                if (pwup.GetRectangle().IntersectsWith(hitbox))
                {
                    pwup.IsDestory = true;
                    return pwup;
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

        #region Powerup Effects

        public static void GrenadePowerup()
        {
            List<BaseEnemyTank> toDestroy = new List<BaseEnemyTank>();
            foreach (BaseEnemyTank tank in enemyTanks)
            {

                toDestroy.Add(tank);
            }
            foreach(BaseEnemyTank tank in toDestroy)
            {
                CreateExplosion(tank.X, tank.Y);
                RemoveTank(tank, false);
            }
        }

        public static void SetHQShield(int second = 10)
        {
            plyBase.SetShield(second*60);
        }

        public static void IncreaseLife(int amount = 1)
        {
            playerLife += amount;
        }

        #endregion

        public static void BaseDestory()
        {
            plyBase.Hurt();
            GameFramework.ChangeToGM();
        }

        public static void KeyDown(KeyEventArgs args)
        {
            player.KeyDown(args);
            if (Globals.DEBUG)
            {
                if (args.KeyCode == Keys.D9)
                {
                    pendingTanks.Clear();
                    enemyTanks.Clear();
                }
            }
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
