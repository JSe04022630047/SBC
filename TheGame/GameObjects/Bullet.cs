using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Bullet : MovingObj
    {
        public int Tag { get; set; } // 0 = player, 1 = enemy
        public bool IsDestory { get; set; }
        public int power = 1;

        public Bullet(int x, int y, int speed, Direction dir, int tag, int power)
        {
            IsDestory = false;
            X = x; Y = y;
            Speed = speed;
            ImgUp = Properties.Resources.bulletup;
            ImgDown = Properties.Resources.bulletdown;
            ImgLeft = Properties.Resources.bulletleft;
            ImgRight = Properties.Resources.bulletright;

            Dir = dir;
            Tag = tag;
            this.power = power;
        }

        public override void DrawS()
        {
            base.DrawS();
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
        }

        private void MoveCheck()
        {
            Rectangle hitbox = GetRectangle();

            if (Dir == Direction.Up || Dir == Direction.Down)
            {
                hitbox.Height += 8;
            }
            else
            {
                hitbox.Width += 8;
            }

            int xExplosion = X - Width / 2;
            int yExplosion = Y - Height / 2;

            if (hitbox.X + hitbox.Width < 0) IsDestory = true;
            else if (hitbox.X - hitbox.Width > 800) IsDestory = true;
            else if (hitbox.Y + hitbox.Height < 0) IsDestory = true;
            else if (hitbox.Y - hitbox.Height > 800) IsDestory = true;

            NoMovingObj[] walls = GameObjectManager.CollidedWalls(hitbox);
            if (walls != null)
            {
                IsDestory = true;
                for (int i = 0; i < walls.Length; i++)
                {
                    if (walls[i] is null) break;
                    if (walls[i] is IronBlock) continue;
                    GameObjectManager.BreakWall(Dir, (BrickWall)walls[i], power);
                }
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }

            /*if (GameObjectManager.IsCollidedBase(hitbox))
            {
                // todo
            }*/

            if (Tag == 0)
            {
                BaseEnemyTank tank = GameObjectManager.IsCollidedTank(hitbox);
                if (tank != null)
                {
                    IsDestory = true;
                    GameObjectManager.HurtTank(tank, power);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    return;
                }
            }
            else if (Tag == 1)
            {
                if (GameObjectManager.IsCollidedPly(hitbox))
                {
                    IsDestory = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    if (GameObjectManager.getPlayer().ShieldTime <= 0)
                    {
                        GameObjectManager.getPlayer().TakeDamage(power);
                    }
                }
            }
            Bullet otherBullet = GameObjectManager.IsCollideBullet(hitbox, Tag);

            if (otherBullet != null)
            {
                IsDestory = true;
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
            }
        }

        private void Move()
        {
            switch (Dir)
            {
                case Direction.Left:
                    X -= Speed; break;
                case Direction.Right:
                    X += Speed; break;
                case Direction.Up:
                    Y -= Speed; break;
                case Direction.Down:
                    Y += Speed; break;
            }
        }

    }
}
