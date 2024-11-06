using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public class Shield : NoMovingObj
    {
        private bool flashed = false;
        private Tank toFollow;
        public Tank Following { get { return toFollow; } }
        private Bitmap frame0 = Properties.Resources.shield0;
        private Bitmap frame1 = Properties.Resources.shield1;
        public Shield(int initX, int initY, Tank tank) : base(initX, initY, Properties.Resources.shield0)
        {
            X = initX-16;
            Y = initY-16;
            toFollow = tank;
            frame0.MakeTransparent(Color.FromArgb(0, 255, 0));
            frame1.MakeTransparent(Color.FromArgb(0, 255, 0));
        }

        public override void Update() 
        {
            if (GameFramework.gameTick % 5 == 0) { flashed = !flashed; }
            X = toFollow.X-16;
            Y = toFollow.Y-16;
            base.Update();
        }

        protected override Image GetImg()
        {
            if (flashed) return frame1;
            return frame0;
        }
    }

    public class HQShieldVisual : NoMovingObj
    {
        private bool flashed = false;
        private HQ toFollow;
        public HQ Following { get { return toFollow; } }
        private Bitmap frame0 = Properties.Resources.shield0;
        private Bitmap frame1 = Properties.Resources.shield1;
        public HQShieldVisual(int initX, int initY, HQ hq) : base(initX, initY, Properties.Resources.shield0)
        {
            X = initX;
            Y = initY;
            toFollow = hq;
            frame0.MakeTransparent(Color.FromArgb(0, 255, 0));
            frame1.MakeTransparent(Color.FromArgb(0, 255, 0));
        }

        public override void Update()
        {
            if (GameFramework.gameTick % 5 == 0) { flashed = !flashed; }
            X = toFollow.X;
            Y = toFollow.Y;
            base.Update();
        }

        protected override Image GetImg()
        {
            if (flashed) return frame1;
            return frame0;
        }
    }
}
