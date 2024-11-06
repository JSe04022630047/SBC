using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class Grenade : Powerup
    {
        public Grenade(int initX, int initY) : base(initX, initY, PowerupID.Grenade) { }
    }
}
