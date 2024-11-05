using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame.GameObjects.StaticObject
{
    public class HQ : NoMovingObj
    {
        private int hp;
        public int HP { get { return hp; } }
        public HQ(int x, int y) : base(x, y, Properties.Resources.BaseOK) { }

    }
}
