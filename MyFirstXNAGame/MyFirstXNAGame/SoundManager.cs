using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace MyFirstXNAGame
{
    public class SoundManager
    {
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue trackCue;
        public SoundManager(string audioEngineFilePath, string waveBankFilePath, string soundBankFilePath)
        {
            audioEngine = new AudioEngine(audioEngineFilePath);
            waveBank = new WaveBank(audioEngine, waveBankFilePath);
            soundBank = new SoundBank(audioEngine, soundBankFilePath);
        }

        public void playOnce(string soundLocation)
        {
            play(soundLocation);
        }

        public Cue play(string sound)
        {
            trackCue = soundBank.GetCue(sound);
            trackCue.Play();
            
            return trackCue;
        }

        private static Cue beginCue;
        private static bool played = false;
        public void Update()
        {
            audioEngine.Update();

            // TODO: copy this out of here..........
            if (beginCue == null)
                beginCue = play("start");
            
            if(beginCue.IsStopped && played == false)
            {
                play("loop");
                played = true;
            }
        }
    }
}
