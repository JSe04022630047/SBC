﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public class BaseEnemyTank : Tank
    {
        public int ChangeDirTime { get; set; } // to get seconds, you divide this variable with Globals.SLEEPTIME
        public float ChanngeDirTimeSec
        {
            get { return ChangeDirTime / Globals.SLEEPTIME; }
            set { ChangeDirTime = (int)(value * Globals.SLEEPTIME); }
        }
        private bool canMove = true;
        private bool dropPowerUp = false;
        public bool CanDropPowerup {  get { return dropPowerUp; } }
        private int changeDirCount;
        public int AttackTime { get; set; }
        public float AttackTimeSec { 
            get { return AttackTime / Globals.SLEEPTIME; }
            set { AttackTime = (int)(value * Globals.SLEEPTIME); }
        }
        private int attackCount;

        private Random r = new Random();

        private Bitmap ImgUpFlash;
        private Bitmap ImgDownFlash;
        private Bitmap ImgLeftFlash;
        private Bitmap ImgRightFlash;
        private bool flashed;

        public BaseEnemyTank(int initX, int initY, int speed, int initHP=1, float changeDirSecond=5.0f, float attackTimeSec=3.0f, Direction dir = Direction.Down, int power = 1) : base(initX, initY, speed, initHP, dir, power)
        {
            ChanngeDirTimeSec = changeDirSecond;
            AttackTimeSec = attackTimeSec;
            ImgUp = Properties.Resources.basictankUp;
            ImgDown = Properties.Resources.basictankDown;
            ImgLeft = Properties.Resources.basictankLeft;
            ImgRight = Properties.Resources.basictankRight;
            makeTransparent();
            dropPowerUp = r.Next(0, 100) < 15;
            //dropPowerUp = true;
            flashed = false;
        }

        protected void makeTransparent()
        {
            foreach (Bitmap bmp in new Bitmap[] { ImgUp, ImgDown, ImgLeft, ImgRight })
            {
                bmp.MakeTransparent(Color.FromArgb(0, 255, 0)); // im too lazy to actually make the images themself to have same transparent key but whatever.
                bmp.MakeTransparent(Color.FromArgb(24, 255, 0)); // I also didn't know that PNG actually have their transparent set for you already but its too late now.
            }
        }
        public override void Update()
        {
            if (dropPowerUp) { if (GameController.gameTick % 5 == 0) { flashed = !flashed; } }
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDir();
            base.Update();
        }

        public void AutoChangeDir()
        {
            //MessageBox.Show(ChangeDirTime.ToString());
            changeDirCount++;
            if (changeDirCount < ChangeDirTime) return;
            changeDirCount = 0;
            ChangeDir();
        }

        public void ChangeDir()
        {
            canMove = true;
            while (true)
            {
            Direction toChange = (Direction)r.Next(0, 4);
                if (toChange == Dir) continue;
                else { Dir = toChange; break; }
            }
            MoveCheck();
        }

        protected override void MoveCheck()
        {
            switch (Dir)
            {
                case Direction.Up:
                    if (Y - Speed < 0 + Height / 2)
                    {
                        canMove = false;
                        return;
                    }
                    break;
                case Direction.Down:
                    if (Y + Speed + Height > 400 + Height / 2)
                    {
                        canMove = false;
                        return;
                    }
                    break;
                case Direction.Left:
                    if (X - Speed < 0 + Width / 2)
                    {
                        canMove = false;
                        return;
                    }
                    break;
                case Direction.Right:
                    if (X + Speed + Width > 400 + Width / 2)
                    {
                        canMove = false;
                        return;
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

            if (Battlefiled.IsCollidedAnyWall(thisHitbox) != null)
            {
                canMove = false;
                return;
            }

            if (Battlefiled.IsCollidedBase(thisHitbox))
            {
                canMove = false; return;
            }

            if (Battlefiled.IsCollidedPly(thisHitbox))
            {
                canMove = false; return;
            }
        }

        protected override void Move()
        {
            if (!canMove) return;
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

        protected virtual void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackTime) return;
            Attack();
            attackCount = 0;
        }

        protected override void Attack()
        {
            Battlefiled.CreateBullet(X, Y, 1, Dir, attackPower);
        }

        protected override Image GetImg()
        {
            if (ImgUpFlash is null)
            {
                ImgUpFlash = AddRedTint(new Bitmap(ImgUp));
                ImgDownFlash = AddRedTint(new Bitmap(ImgDown));
                ImgLeftFlash = AddRedTint(new Bitmap(ImgLeft));
                ImgRightFlash = AddRedTint(new Bitmap(ImgRight));
            }

            Bitmap bmp = new Bitmap(32,32);
            switch (Dir)
            {
                case Direction.Up:
                    if (flashed) bmp = ImgUpFlash;
                    else bmp = ImgUp;
                    break;
                case Direction.Down:
                    if (flashed) bmp = ImgDownFlash; 
                    else bmp = ImgDown;
                    break;
                case Direction.Left:
                    if (flashed) bmp = ImgLeftFlash;
                    else bmp = ImgLeft;
                    break;
                case Direction.Right:
                    if (flashed) bmp = ImgRightFlash;
                    else bmp = ImgRight;
                    break;
            }

            return bmp;
        }

        private Bitmap AddRedTint(Bitmap img)
        {
            byte A,R,G,B;
            Color pixelColor;
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    pixelColor = img.GetPixel(x, y);
                    A = pixelColor.A;
                    R = pixelColor.R;
                    G = (byte)(120 - pixelColor.G);
                    B = (byte)(120 - pixelColor.B);
                    img.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            return img;
        }

    }

    public class BasicTank : BaseEnemyTank
    {
        public BasicTank(int initX, int initY) : base(initX, initY, 1, 1)
        {
            #region image
            ImgUp = Properties.Resources.basictankUp;
            ImgDown = Properties.Resources.basictankDown;
            ImgLeft = Properties.Resources.basictankLeft;
            ImgRight = Properties.Resources.basictankRight;
            makeTransparent();
            #endregion
        }
    }

    public class FastTank : BaseEnemyTank
    {
        public FastTank(int initX, int initY) : base(initX, initY, 3, 1, 4, 2)
        {
            #region image
            ImgUp = Properties.Resources.fastTankUp;
            ImgDown = Properties.Resources.fastTankDown;
            ImgLeft = Properties.Resources.fastTankLeft;
            ImgRight = Properties.Resources.fastTankRight;
            makeTransparent();
            #endregion
        }

        protected override void Attack()
        {
            Battlefiled.CreateBullet(X, Y, 1, Dir, attackPower, 10);
        }
    }

    public class PowerTank : BaseEnemyTank
    {
        public PowerTank(int initX, int initY) : base(initX, initY, 2, 1)
        {
            #region image
            ImgUp = Properties.Resources.powertankUp;
            ImgDown = Properties.Resources.powertankDown;
            ImgLeft = Properties.Resources.powertankLeft;
            ImgRight = Properties.Resources.powertankRight;
            makeTransparent();
            #endregion
        }
    }

    public class ArmorTank : BaseEnemyTank
    {
        public ArmorTank(int initX, int initY) : base(initX, initY, 2, 3)
        {
            #region image
            ImgUp = Properties.Resources.armortankUp;
            ImgDown = Properties.Resources.armortankDown;
            ImgLeft = Properties.Resources.armortankLeft;
            ImgRight = Properties.Resources.armortankRight;
            makeTransparent();
            #endregion
        }
    }
}
