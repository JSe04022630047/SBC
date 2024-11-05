using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class IronBlock : NoMovingObj
    {
        public IronBlock(int x, int y) : base(x, y, Properties.Resources.ironblock) { }
    }
}
