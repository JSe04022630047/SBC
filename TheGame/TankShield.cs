using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    class TankShield : Powerup
    {
        public TankShield(int initX, int initY) : base(initX, initY, PowerupID.Helmet) { }
    }
}
