using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameObjectPeas : GameObjectBlockBonus
    {
        uint no;
        public GameObjectPeas(int id, Vector2 pos, uint no, int blockSizeWidth, int blockSizeHeight) :
            base(id, pos, GameObjectType.Peas, "Images/peas", blockSizeWidth, blockSizeHeight) { }

        public static GameObjectPeas get(int id, Point pos, uint no, int blockSizeWidth, int blockSizeHeight)
        {
            return new GameObjectPeas(id, new Vector2(pos.X * blockSizeWidth, pos.Y * blockSizeHeight), no, blockSizeWidth, blockSizeHeight);
        }

        public void Consume(Player player)
        {
            player.addPeas(no);

            base.Consume(player);
        }
    }
}
