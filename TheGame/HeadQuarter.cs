using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGame
{
    public class HQ : NoMovingObj
    {
        private int shieldTime = 0;
        public int ShieldTime { get { return shieldTime; } }
        private bool hasShield = false;
        private int hp;
        public int HP { get { return hp; } }

        private Bitmap ok = Properties.Resources.BaseOK;
        private Bitmap notok = Properties.Resources.BaseDED;

        public HQ(int x, int y) : base(x, y, Properties.Resources.BaseOK)
        {
            hp = 1;
            ok.MakeTransparent(Color.FromArgb(0, 255, 0));
            notok.MakeTransparent(Color.FromArgb(0, 255, 0));
        }

        protected override Image GetImg()
        {
            if (hp > 0) return ok;
            return notok;
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
            }
            else hasShield = false;
            base.Update();
        }

        public virtual void SetShield(int tick)
        {
            if (tick < 0)
            {
                shieldTime = 0;
            }
            else
            {
                shieldTime = tick;
            }
        }

    }
}
