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
            else if (hitbox.X - hitbox.Width > 400) IsDestory = true;
            else if (hitbox.Y + hitbox.Height < 0) IsDestory = true;
            else if (hitbox.Y - hitbox.Height > 400) IsDestory = true;

            NoMovingObj[] walls = Battlefiled.CollidedWalls(hitbox);
            if (walls != null)
            {
                if (GameController.GameState == GameState.Play) if (Tag == 0) SoundManager.PlayBlast();
                IsDestory = true;
                for (int i = 0; i < walls.Length; i++)
                {
                    if (walls[i] is null) break;
                    if (walls[i] is IronBlock) continue;
                    Battlefiled.BreakWall(Dir, (BrickWall)walls[i], power);
                }
                Battlefiled.CreateExplosion(xExplosion, yExplosion);
                return;
            }

            if (Battlefiled.IsCollidedBase(hitbox))
            {
                IsDestory = true;
                if (Battlefiled.HQHasShield) { return; }
                Battlefiled.BaseDestory();
            }

            if (Tag == 0)
            {
                BaseEnemyTank tank = Battlefiled.IsCollidedTank(hitbox);
                if (tank != null)
                {
                    if (GameController.GameState == GameState.Play) SoundManager.PlayBlast();
                    IsDestory = true;
                    Battlefiled.HurtTank(tank, power);
                    Battlefiled.CreateExplosion(xExplosion, yExplosion);
                    return;
                }
            }
            else if (Tag == 1)
            {
                if (Battlefiled.IsCollidedPly(hitbox))
                {
                    IsDestory = true;
                    Battlefiled.CreateExplosion(xExplosion, yExplosion);
                    if (Battlefiled.getPlayer().ShieldTime <= 0)
                    {
                        if (GameController.GameState == GameState.Play) Battlefiled.getPlayer().TakeDamage(power);
                    }
                    else { if (GameController.GameState == GameState.Play) SoundManager.PlayHitShield(); }
                }
            }
            Bullet otherBullet = Battlefiled.IsCollideBullet(hitbox, Tag);

            if (otherBullet != null)
            {
                IsDestory = true;
                Battlefiled.CreateExplosion(xExplosion, yExplosion);
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
