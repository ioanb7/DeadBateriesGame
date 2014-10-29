using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    // We only need to render this one.
    public class GameObjectSpawner : GameObjectBlock
    {
        public GameObjectSpawner(int id, Vector2 pos, int blockSizeWidth, int blockSizeHeight) :
            base(id, pos, GameObjectType.Spawner, "Images/playerSpawner", blockSizeWidth, blockSizeHeight) { }

        public static GameObjectSpawner get(int id, Point pos, int blockSizeWidth, int blockSizeHeight)
        {
            return new GameObjectSpawner(id, new Vector2(pos.X * blockSizeWidth, pos.Y * blockSizeHeight), blockSizeWidth, blockSizeHeight);
        }
    }
}
