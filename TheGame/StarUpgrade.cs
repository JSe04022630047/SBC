using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class StarUpgrade : Powerup
    {
        public StarUpgrade(int initX, int initY) : base(initX, initY, PowerupID.Star) { }
    }
}
