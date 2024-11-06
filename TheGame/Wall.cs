using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class BrickWall : NoMovingObj
    {
        private BrickState state;
        public BrickWall(int x, int y) : base(x, y, new Bitmap(16, 16))
        {
            state = 0; // basically brickState.None
        }

        public int getState()
        {
            return (int)state;
        }

        public void setState(BrickState setTo)
        {
            state = setTo;
        }

        protected override Image GetImg()
        {
            Bitmap bitmap;
            switch (getState())
            {
                case 0:
                    bitmap = Properties.Resources.brick;
                    break;
                case 1:
                    bitmap = Properties.Resources.brick1010;
                    break;
                case 2:
                    bitmap = Properties.Resources.brick0101;
                    break;
                case 3:
                    bitmap = Properties.Resources.brick1100;
                    break;
                case 4:
                    bitmap = Properties.Resources.brick0011;
                    break;
                case 5:
                    bitmap = Properties.Resources.brick0111;
                    break;
                case 6:
                    bitmap = Properties.Resources.brick1011;
                    break;
                case 7:
                    bitmap = Properties.Resources.brick1101;
                    break;
                case 8:
                    bitmap = Properties.Resources.brick1110;
                    break;
                default:
                    bitmap = Properties.Resources.brick;
                    break;
            }
            bitmap.MakeTransparent(Color.FromArgb(0, 255, 0));
            return bitmap;
        }
    }

    public class IronBlock : NoMovingObj
    {
        public IronBlock(int x, int y) : base(x, y, Properties.Resources.ironblock) { }
    }

    public class Bush : NoMovingObj
    {
        public Bush(int x, int y) : base(x, y, Properties.Resources.bush) { }
    }
}
