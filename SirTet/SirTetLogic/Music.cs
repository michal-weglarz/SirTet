using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace SirTetLogic
{
    public static class Music
    {
        public static void MainTheme(int themeNum = 1)
        {
            SoundPlayer sound = new SoundPlayer();
            switch(themeNum)
            {
                case 1:
                    sound = new SoundPlayer(SirTetLogic.Properties.Resources.music1);
                    break;
                case 2:
                    sound = new SoundPlayer(SirTetLogic.Properties.Resources.music2);
                    break;
            }
            sound.PlayLooping();
        }

        public static void MoveSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.slow_hit);
            sound.Play();
        }

        public static void RotateSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.rotate);
            sound.Play();
        }

        public static void FallSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.fall);
            sound.Play();
        }

        public static void LineClearSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.line);
            sound.Play();
        }

        public static void ComboSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.NES_Clear);
            sound.Play();
        }

        public static void GameStartSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.pause);
            sound.Play();
        }

        public static void GameOverSFX()
        {
            SoundPlayer sound = new SoundPlayer(SirTetLogic.Properties.Resources.gameover);
            sound.Play();
        }
    }
}
