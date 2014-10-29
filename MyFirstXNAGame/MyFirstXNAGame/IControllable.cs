using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public interface IControllable
    {
        void UpdateControl(KeyboardStateCustom keyboardState);
    }
}
