using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public abstract class GameObj
    {
        public int X;
        public int Y;

        public int Width;
        public int Height;

        // im thinking some of the object may not have a hitbox so

        protected abstract Image GetImg();

        public virtual void DrawS()
        {
            Graphics g = GameFramework.g;
            g.DrawImage(GetImg(), X, Y);
        }

        public virtual void Update()
        {
            DrawS();
        }

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
        
    }
}
