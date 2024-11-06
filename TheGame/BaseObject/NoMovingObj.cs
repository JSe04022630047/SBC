using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class NoMovingObj : GameObj
    {
        public NoMovingObj(int x, int y, Image img)
        {
            X = x;
            Y = y;
            Img = img;
        }

        private Image img;
        public Image Img
        {
            get { return img; }
            set
            {
                img = value;
                Width = img.Width;
                Height = img.Height; // oh boy, i sure hope the images are perfect square or else something bad is going to happen
            }
        }

        protected override Image GetImg()
        {
            return Img;
        }
    }
}
