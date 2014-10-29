using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public static class SoundPlayer
    {
        public static void gunShot()
        {
            TheGame.Instance.SoundManager.playOnce("gunShot");
        }
        public static void beginGame()
        {
            // TODO: we do this currently in SoundManager.Update() :/
            //TheGame.Instance.SoundManager.play("loop");
        }
    }
}
