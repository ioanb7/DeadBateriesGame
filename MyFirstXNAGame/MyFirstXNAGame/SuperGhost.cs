using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class SuperGhost : ChasingGhost
    {
        public SuperGhost(Vector2 pos, Vector2 velocity)
            : base(pos, velocity, 100, 15, "Images/ghostSuper", "Images/ghostSuperDead") { }
    }
}
