using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class MovingObj : GameObj
    {
        object _lock = new object();
        private Image img;
        public Image Img {
            get { return img; }
            set
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        public Bitmap ImgUp;
        public Bitmap ImgDown;
        public Bitmap ImgLeft;
        public Bitmap ImgRight;
        public int Speed { get; set; }

        private Direction dir;
        public Direction Dir
        {
            get { return dir; }
            set 
            {
                dir = value;
                Bitmap bmp = null;

                switch (dir)
                {
                    case Direction.Left:
                        bmp = ImgLeft;
                        break;
                    case Direction.Right:
                        bmp = ImgRight;
                        break;
                    case Direction.Up:
                        bmp = ImgUp;
                        break;
                    case Direction.Down:
                        bmp = ImgDown;
                        break;
                }

                lock (_lock)
                {
                    if (bmp != null)
                    {
                        try
                        {
                            Width = bmp.Width;
                            Height = bmp.Height;
                        }
                        catch (Exception) { } // just ignore them, it isn't game breaking
                    }
                }
            }
        }

        protected override Image GetImg()
        {
            Bitmap bmp = null;
            switch (Dir)
            {
                case Direction.Left:
                    bmp = ImgLeft;
                    break;
                case Direction.Right:
                    bmp = ImgRight;
                    break;
                case Direction.Up:
                    bmp = ImgUp;
                    break;
                case Direction.Down:
                    bmp = ImgDown;
                    break;
            }
            bmp.MakeTransparent(Color.FromArgb(24, 255, 0)); // Make not quite extreme green transparent
            bmp.MakeTransparent(Color.FromArgb(0, 255, 0)); // Make extreme green transparent
            return bmp;
        }

        // Basically make the actual postion be in the middle of the hitbox.

        public override void DrawS()
        {
            Graphics g = GameController.g;
            g.DrawImage(GetImg(), X - Width/2, Y - Height/2);
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle(X - Width/2, Y - Height/2, Width, Height);
        }

    }
}
