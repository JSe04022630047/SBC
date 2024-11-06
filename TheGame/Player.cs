using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public class PlyTank : Tank
    {
        public int respawnTime;
        public int respawnTimeCounter;
        public bool IsMoving { get; set; }
        public Direction lastDirection = 0;
        private int initX = 0, initY = 0;
        public PlyTank(int initX, int initY, int HP = 1) : base(initX, initY, 5, HP)
        {
            #region image
            Bitmap[] bitmaps = { Properties.Resources.playerUp, Properties.Resources.playerDown, Properties.Resources.playerLeft, Properties.Resources.playerRight };
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        ImgUp = bitmaps[i];
                        break;
                    case 1:
                        ImgDown = bitmaps[i];
                        break;
                    case 2:
                        ImgLeft = bitmaps[i];
                        break;
                    case 3:
                        ImgRight = bitmaps[i];
                        break;
                }
            }
            #endregion
            this.initX = initX;
            this.initY = initY;
            respawnTime = 10 * Globals.SLEEPTIME;
            SetShield(15*Globals.SLEEPTIME);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void SetShield(int tick)
        {
            if (tick > 0) GameObjectManager.lastMaxShieldTime = tick;
            base.SetShield(tick);
        }

        protected override void MoveCheck()
        {
            switch (Dir)
            {
                case Direction.Up:
                    if (Y - Speed < 0)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Down:
                    if (Y + Speed + Height/4 > 800)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Left:
                    if (X - Speed < 0)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Right:
                    if (X + Speed + Width/4 > 800)
                    {
                        IsMoving = false; return;
                    }
                    break;
            }

            Rectangle thisHitbox = GetRectangle();

            thisHitbox.Width = (int)(Width*0.8);
            thisHitbox.Height = (int)(Height*0.8);

            switch (Dir)
            {
                case Direction.Up:
                    thisHitbox.Y -= Speed;
                    break;
                case Direction.Down:
                    thisHitbox.Y += Speed;
                    break;
                case Direction.Left:
                    thisHitbox.X -= Speed;
                    break;
                case Direction.Right:
                    thisHitbox.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedAnyWall(thisHitbox) != null)
            {
                IsMoving = false; return;
            }

            if (GameObjectManager.IsCollidedBase(thisHitbox))
            {
                IsMoving = false; return;
            }

            if (GameObjectManager.IsCollidedTank(thisHitbox) != null) { IsMoving = false; return; }

            Powerup touchedPowerup = GameObjectManager.IsCollidedPowerup(thisHitbox);

            if (touchedPowerup != null)
            {
                switch (touchedPowerup.thisPowerupID)
                {
                    case 0:
                        if (GameFramework.GameState == GameState.Play) SoundManager.PlayPowerUpSound();
                        GameObjectManager.GrenadePowerup();
                        GameObjectManager.IncreaseSorce(100);
                        break;
                    case 1:
                        if (GameFramework.GameState == GameState.Play) SoundManager.PlayPlyShieldSound();
                        SetShield(15 * Globals.SLEEPTIME);
                        break;
                    case 2:
                        if (GameFramework.GameState == GameState.Play) SoundManager.PlayPlyShieldSound();
                        GameObjectManager.SetHQShield();
                        break;
                    case 3:
                        attackPower++;
                        if (GameFramework.GameState == GameState.Play) SoundManager.PlayPowerUpSound();
                        GameObjectManager.IncreaseSorce(100);
                        break;
                    case 4:
                        if (GameFramework.GameState == GameState.Play) SoundManager.Play1UpSound();
                        GameObjectManager.IncreaseLife();
                        GameObjectManager.IncreaseSorce(100);
                        break;
                    case 5:
                        MessageBox.Show("NOT IMPLEMENTED");
                        break;
                }
                touchedPowerup.IsDestory = true;

            }
        }

        protected override void Move()
        {
            if (!IsMoving) { return; }
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }

        protected override void Attack()
        {
            if (GameFramework.GameState == GameState.Play) SoundManager.PlayFire();
            GameObjectManager.CreateBullet(X, Y, 0, Dir, attackPower);
        }

        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        X = (int)(X / 16.0) * 16;
                        lastDirection = Dir;
                    }
                    break;

                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        X = (int)(X / 16.0) * 16;
                        lastDirection = Dir;
                    }
                    break;

                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        Y = (int)(Y / 16.0) * 16;
                        lastDirection = Dir;
                    }
                    break;

                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        Y = (int)(Y / 16.0) * 16;
                        lastDirection = Dir;
                    }
                    break;

                case Keys.Space:
                    Attack();
                    break;
            }
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }

        public void Death()
        {
            respawnTimeCounter++;
            if (respawnTime > respawnTimeCounter)
            {
                X = 950;
                Y = 950;
                return;
            }
            HP = 1;
            X = initX;
            Y = initY;
            respawnTimeCounter = 0;
            SetShield(10*Globals.SLEEPTIME);
            GameObjectManager.PlayerFinishedRespawning();
        }
    }
}

