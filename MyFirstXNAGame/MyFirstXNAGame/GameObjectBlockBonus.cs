using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public abstract class GameObjectBlockBonus : GameObjectBlock
    {
        public GameObjectBlockBonus(int id, Vector2 pos, GameObjectType type, string textureLocation, int blockSizeWidth, int blockSizeHeight) :
            base(id, pos, type, textureLocation, blockSizeWidth, blockSizeHeight) { }

        public virtual void Consume(Player player)
        {
            shouldBeRemoved = true;
        }
    }
}
