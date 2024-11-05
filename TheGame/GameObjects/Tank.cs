using System.Drawing;
using System.Windows.Forms;

namespace TheGame
{
    public class Tank : MovingObj
    {
        private int shieldTime = 0;
        public int ShieldTime { get { return shieldTime; } }
        private bool hasShield = false;
        private int hp = 0;
        public int HP { get { return hp; } set { hp = value; } }
        public int attackPower;

        public Tank(int initX, int initY, int speed, int initHP = 1, Direction dir = Direction.Up, int attackPower = 1)
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

            X = initX;
            Y = initY;
            Speed = speed;
            Dir = dir;
            HP = initHP;
            this.attackPower = attackPower;
        }

        public void TakeDamage(int dmg = 1)
        {
            if (HP - dmg < 0) { HP = 0; }
            else { HP -= dmg; }
        }

        public override void Update()
        {
            if (shieldTime > 0)
            {
                if (!hasShield)
                {
                    hasShield = true;
                    GameObjectManager.CreateShield(X, Y, this);
                }
                shieldTime--;
            } else hasShield = false;
            MoveCheck();
            Move();
            base.Update();
        }

        public virtual void SetShield(int tick)
        {
            if (tick<0)
            {
                shieldTime = 0;
            }
            else
            {
                shieldTime = tick;
            }
        }

        protected virtual void MoveCheck() { MessageBox.Show("This tank has no features to check its movement!"); }

        protected virtual void Move() { MessageBox.Show("This tank has no features to move!"); }

        protected virtual void Attack() { MessageBox.Show("This tank has no features to attack!"); }

    }
}
