using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameObjectPortal : GameObjectBlock
    {
        public GameObjectPortal(int id, Vector2 pos, int blockSizeWidth, int blockSizeHeight) :
            base(id, pos, GameObjectType.Portal, "Images/tele", blockSizeWidth, blockSizeHeight) { }

        public static GameObjectPortal get(int id, Point pos, int blockSizeWidth, int blockSizeHeight)
        {
            return new GameObjectPortal(id, new Vector2(pos.X * blockSizeWidth, pos.Y * blockSizeHeight), blockSizeWidth, blockSizeHeight);
        }
    }
}
