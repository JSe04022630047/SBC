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
        public PlyTank(int initX, int initY, int HP = 1) : base(initX, initY, 4, HP)
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
                    if (Y - Speed < 0 + Height / 2)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Down:
                    if (Y + Speed + Height > 800 + Height / 2)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Left:
                    if (X - Speed < 0 + Width / 2)
                    {
                        IsMoving = false; return;
                    }
                    break;
                case Direction.Right:
                    if (X + Speed + Width > 800 + Width / 2)
                    {
                        IsMoving = false; return;
                    }
                    break;
            }

            Rectangle thisHitbox = GetRectangle();

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

            /*if (GameObjectManager.IsCollidedBase(thisHitbox))
            {
                IsMoving = false; return;
            }*/
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
                        X = (((X - (Width / 2)) / 16) * 16) + (Width / 2);
                        lastDirection = Dir;
                    }
                    break;

                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        X = ((X + (Width / 2)) / 16) * 16 - (Width / 2);
                        lastDirection = Dir;
                    }
                    break;

                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        Y = (((Y - (Height / 2)) / 16) * 16) + (Height / 2);
                        lastDirection = Dir;
                    }
                    break;

                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    if (lastDirection != Dir)
                    {
                        Y = ((Y + (Height / 2)) / 16) * 16 - (Height / 2);
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

