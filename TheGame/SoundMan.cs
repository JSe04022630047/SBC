using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheGame.Properties;

namespace TheGame
{
    class SoundManager
    {
        private static SoundPlayer uiSound = new SoundPlayer();
        private static SoundPlayer playerSound = new SoundPlayer();
        private static SoundPlayer blastSound = new SoundPlayer();
        /*TODO add sounds and test*/
        public static void PlayStart()
        {
            /*uiSound.Stream = Resources.*/
            uiSound.Play();
        }

        public static void PlayAdd()
        {
            /*uiSound.Stream = Resources.*/
            uiSound.Play();
        }

        public static void PlayFire()
        {
            /*playerSound.Stream = Resources.*/
            playerSound.Play();
        }

        public static void PlayHit()
        {
            /*playerSound.Stream = Resources.*/
            playerSound.Play();
        }

        public static void PlayBlast()
        {
            /*blastSound.Stream = Resources.*/
            blastSound.Play();
        }
    }
}
