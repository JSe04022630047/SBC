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
        public int thisPowerup { get { return (int)id; } }
        private bool flashed;

        private Bitmap normal;
        private Bitmap flash;

        public Powerup(int initX, int initY, PowerupID id) : base(initX, initY, new Bitmap(32, 32)) { this.id = id; flashed = false; }

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
                    case PowerupID.Star:
                        normal = Properties.Resources.Powerup; break;
                    case PowerupID.Tank:
                        normal = Properties.Resources._1Up; break;
                    case PowerupID.Timer:
                        normal = Properties.Resources.Clock; break;
                }
                normal.MakeTransparent(Color.FromArgb(0, 255, 0));
                byte A,R,G,B;
                Color pixelColor;
                flash = normal;
                for (int y = 0; y < flash.Height; y++)
                {
                    for (int x = 0; x < flash.Width; x++)
                    {
                        pixelColor = flash.GetPixel(x, y);
                        A = pixelColor.A;
                        R = (byte)(255 - pixelColor.R);
                        G = (byte)(255 - pixelColor.G);
                        B = (byte)(255 - pixelColor.B);
                        flash.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                    }
                }
            }

            if (!flashed) return normal;
            return flash;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width+16, Height+16); // making getting powerup forgiving
        }
    }
}
