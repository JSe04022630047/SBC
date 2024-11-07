using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGame
{
    public enum GameState
    {
        Play, // playing
        Inter,// mission
        Over, // game over
        Win   // end credit screen question mark
    }

    public enum Direction
    {
        Up,   //0
        Down, //1
        Left, //2
        Right //3
    }
    public enum BrickState
    { // the description is what part is destoryed
        None,// 0000
        L,   // 1010
        R,   // 0101
        U,   // 1100
        D,   // 0011
        TL,  // 0111
        TR,  // 1011
        BL,  // 1101
        BR   // 1110
    }

    /*
     * Basically brick state is like this:
     * D = destoryed
     * C = intact
     * 
     * if the brick has not been damaged, it will get the none state or
     * 
     *  CC
     *  CC or 0000
     *  
     *  for damaged L it will look like this
     *  DC
     *  DC or 1010
    */

    public enum PowerupID
    {
        Grenade, // kill all enemy tank
        Helmet,  // give shield for 15 seconds
        Shovel,  // HQShield
        Star,    // upgrade player destruction
        Tank,    // +1up
        Timer    // enemy tanks lobotomized for 15 seconds
    }

    public static class Globals
    {
        public const int FRAMERATE = 120;
        public const int SLEEPTIME = 1000 / FRAMERATE;
        public const bool DEBUG = true;
        public const int CANVAS_X = 400;
        public const int CANVAS_Y = 400;
    }

}
