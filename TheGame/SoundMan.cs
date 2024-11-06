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
        private static SoundPlayer startSound = new SoundPlayer();
        private static SoundPlayer gameOverSound = new SoundPlayer();
        private static SoundPlayer shootSound = new SoundPlayer();
        private static SoundPlayer hitBrickSound = new SoundPlayer();
        private static SoundPlayer hitIronSound = new SoundPlayer();
        private static SoundPlayer shieldHitSound = new SoundPlayer();
        private static SoundPlayer blastSound = new SoundPlayer();

        private static SoundPlayer powerUpSound = new SoundPlayer();
        /*TODO add sounds and test*/
        public static void InitSound()
        {
            startSound.Stream = Properties.Resources.levelstarting;
            gameOverSound.Stream = Properties.Resources.gameover1;
            shootSound.Stream = Properties.Resources.shoot;
            hitBrickSound.Stream = Properties.Resources.brickhit;
            hitIronSound.Stream = Properties.Resources.steelhit;
            shieldHitSound.Stream = Properties.Resources.shieldhit;
            blastSound.Stream = Resources.fexplosion;
        }
        public static void PlayStart()
        {
            startSound.Play();
        }

        public static void PlayGameOver()
        {
            startSound.Stop();
            gameOverSound.Play();
        }

        public static void PlayFire()
        {
            shootSound.Play();
        }

        public static void PlayHitBrick()
        {
            hitBrickSound.Play();
        }

        public static void PlayHitIron()
        {
            hitIronSound.Play();
        }

        public static void PlayHitShield()
        {
            shieldHitSound.Play();
        }

        public static void PlayBlast()
        {
            blastSound.Play();
        }

        public static void Play1UpSound()
        {
            powerUpSound.Stream = Properties.Resources.life;
            powerUpSound.Play();
        }

        public static void PlayPlyShieldSound()
        {
            powerUpSound.Stream = Properties.Resources.tbonushit;
            powerUpSound.Play();
        }

        public static void PlayPowerUpSound()
        {
            powerUpSound.Stream = Properties.Resources.bonus;
            powerUpSound.Play();
        }
    }
}
