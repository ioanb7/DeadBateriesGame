using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Ghost : ChasingGhost
    {
        public Ghost(Vector2 pos, Vector2 velocity)
            : base(pos, velocity, 30, 5, "Images/ghost", "Images/ghostDead") { }
    }
}

