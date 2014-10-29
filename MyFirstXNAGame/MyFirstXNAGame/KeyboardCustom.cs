using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public enum KeysMe{
        Player1Up,
        Player1Right,
        Player1Down,
        Player1Left,
        Player1HoldBulletDirection,
        Player1Jump,
        Player1Fire,

        Player2Up,
        Player2Right,
        Player2Down,
        Player2Left,
        Player2HoldBulletDirection,
        Player2Jump,
        Player2Fire,

        GMHealthPlus,
        GMVelocityPlus,
        GMVelocityMinus,

        ViewportMoveLeft,
        ViewportMoveUp,
        ViewportMoveRight,
        ViewportMoveDown,

        PauseGame,
        ExitGame,
        InDevActivate,
        InDevDeactivate,
        GameActivateGM
    }

    public class KeyboardStateCustom
    {
        private Dictionary<Keys, KeysMe> keys;
        private Dictionary<KeysMe, bool> keysPressed;
        private void initializeKeys()
        { // TODO: at final project, change these to W A S D 

#if DEBUG
            keys.Add(Keys.Up, KeysMe.Player1Up);
            keys.Add(Keys.Right, KeysMe.Player1Right);
            keys.Add(Keys.Down, KeysMe.Player1Down);
            keys.Add(Keys.Left, KeysMe.Player1Left);
#else
            keys.Add(Keys.W, KeysMe.Player1Up);
            keys.Add(Keys.D, KeysMe.Player1Right);
            keys.Add(Keys.S, KeysMe.Player1Down);
            keys.Add(Keys.A, KeysMe.Player1Left);
#endif

            /*
            keys.Add(Keys.LeftShift, KeysMe.Player1HoldBulletDirection);
            keys.Add(Keys.Space, KeysMe.Player1Jump);
            keys.Add(Keys.LeftControl, KeysMe.Player1Fire);*/
            keys.Add(Keys.LeftControl, KeysMe.Player1HoldBulletDirection);
            keys.Add(Keys.LeftShift, KeysMe.Player1Jump);
            keys.Add(Keys.Space, KeysMe.Player1Fire);

            keys.Add(Keys.NumPad8, KeysMe.Player2Up);
            keys.Add(Keys.NumPad6, KeysMe.Player2Right);
            keys.Add(Keys.NumPad2, KeysMe.Player2Down);
            keys.Add(Keys.NumPad4, KeysMe.Player2Left);

            /*
            keys.Add(Keys.RightShift, KeysMe.Player2HoldBulletDirection);
            keys.Add(Keys.Enter, KeysMe.Player1Jump);
            keys.Add(Keys.RightControl, KeysMe.Player2Fire);*/
            keys.Add(Keys.RightControl, KeysMe.Player2HoldBulletDirection);
            keys.Add(Keys.RightShift, KeysMe.Player2Jump);
            keys.Add(Keys.Enter, KeysMe.Player2Fire);
            
            keys.Add(Keys.PageUp, KeysMe.GMVelocityPlus);
            keys.Add(Keys.PageDown, KeysMe.GMVelocityMinus);
            keys.Add(Keys.End, KeysMe.GMHealthPlus);

            keys.Add(Keys.I, KeysMe.ViewportMoveUp);
            keys.Add(Keys.J, KeysMe.ViewportMoveLeft);
            keys.Add(Keys.K, KeysMe.ViewportMoveDown);
            keys.Add(Keys.L, KeysMe.ViewportMoveRight);


            keys.Add(Keys.Escape, KeysMe.PauseGame);
            //keys.Add(Keys.F2, KeysMe.ExitGame);
            keys.Add(Keys.OemMinus, KeysMe.InDevDeactivate);
            keys.Add(Keys.OemPlus, KeysMe.InDevActivate);
            keys.Add(Keys.Back, KeysMe.GameActivateGM);
        }

        public KeyboardStateCustom(KeyboardState keyboardState)
        {
            keys = new Dictionary<Keys, KeysMe>();
            keysPressed = new Dictionary<KeysMe, bool>();
            initializeKeys();
            foreach (KeysMe key in (KeysMe[])Enum.GetValues(typeof(KeysMe)))
            {
                keysPressed[key] = false;
            }

            foreach(Keys key in (Keys[]) Enum.GetValues(typeof(Keys)))
            {
                if(keyboardState.IsKeyDown(key))
                {
                    if(keys.ContainsKey(key))
                    {
                        keysPressed[keys[key]] = true; // TODO: LOL
                    }
                }
            }
        }

        public bool IsKeyDown(KeysMe keysMe)
        {
            return keysPressed[keysMe];
        }

        public static KeyboardStateCustom getKeyboardState(KeyboardState keyboardState)
        {
            return new KeyboardStateCustom(keyboardState);
        }
    }
}
