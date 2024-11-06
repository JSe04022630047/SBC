using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Powerup : NoMovingObj
    {
        private PowerupID id;
        public int thisPowerupID { get { return (int)id; } }
        private bool flashed;
        public bool IsDestory { get; set; }
        private int lifetimeCount = 0;
        private int lifeTime = 30 * Globals.SLEEPTIME;

        private Bitmap normal = null;
        private Bitmap flash;

        public Powerup(int initX, int initY, PowerupID id) : base(initX, initY, new Bitmap(32, 32)) { this.id = id; flashed = false; IsDestory = false; }

        protected override Image GetImg()
        {
            // This assumes that the powerup ID doesn't change after the object is instanced
            if (normal is null)
            {
                switch (id)
                {
                    case PowerupID.Grenade:
                        normal = Properties.Resources.Grenade; break;
                    case PowerupID.Helmet:
                        normal = Properties.Resources.Helmet; break;
                    case PowerupID.Shovel:
                        normal = Properties.Resources.BaseDefense; break;
                    case PowerupID.Star:
                        normal = Properties.Resources.Powerup; break;
                    case PowerupID.Tank:
                        normal = Properties.Resources._1Up; break;
                    case PowerupID.Timer:
                        normal = Properties.Resources.Clock; break;
                    default: // just in case it happens
                        normal = Properties.Resources.BaseDED; break;
                }
                flash = InvertImage(normal);
            }

            if (!flashed) return normal;
            else return flash;
        }

        private Bitmap InvertImage(Bitmap img)
        {
            byte A, R, G, B;
            Color pixelColor;
            Bitmap invert = new Bitmap(img);
            for (int y = 0; y < invert.Height; y++)
            {
                for (int x = 0; x < invert.Width; x++)
                {
                    pixelColor = invert.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    invert.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            return invert;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle(X-8, Y-8, Width+16, Height+16); // making getting powerup forgiving
        }

        public override void Update()
        {
            lifetimeCount++;
            if (GameFramework.gameTick % 3 == 0) { flashed = !flashed; }
            if (lifetimeCount > lifeTime)
            {
                lifetimeCount = 0;
                IsDestory = true;
            }

            base.Update();
        }
    }
}
