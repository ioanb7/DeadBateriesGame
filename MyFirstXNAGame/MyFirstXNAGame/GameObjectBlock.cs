using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public abstract class GameObjectBlock : GameObject
    {
        Texture2D texture;
        string textureLocation;
        protected int blockSizeWidth;
        protected int blockSizeHeight;
        public int id;

        public GameObjectBlock(int id, Vector2 pos, GameObjectType type, string textureLocation, int blockSizeWidth, int blockSizeHeight) :
            base(pos, type)
        {
            this.id = id;
            this.textureLocation = textureLocation;
            
            this.blockSizeWidth = blockSizeWidth;
            this.blockSizeHeight = blockSizeHeight;
        }

        /*
        public static GameObjectBlock get(Point pos, int blockSizeWidth, int blockSizeHeight)
        {
            return new GameObjectBlock(new Vector2(pos.X * blockSizeWidth, pos.Y * blockSizeHeight), blockSizeWidth, blockSizeHeight);
        }*/

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get(textureLocation);
            state = GameObjectState.Created;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(WorldDrawer worldDrawer)
        {
            worldDrawer.Draw(texture, getWorldRectangle(), Color.White);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, blockSizeWidth, blockSizeHeight);
        }

        public override Rectangle getCollisionRectangle()
        {
            return getWorldRectangle();
        }
    }
}
