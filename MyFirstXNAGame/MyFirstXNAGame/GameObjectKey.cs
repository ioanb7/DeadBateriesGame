using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameObjectKey : GameObject
    {
        Texture2D texture;

        public GameObjectKey(Vector2 pos)
            : base(pos, GameObjectType.Key)
        {

        }

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/key");
            state = GameObjectState.Created;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            //if (IsAlive())
                worldDrawer.Draw(texture, getWorldRectangle(), Color.White);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 75, 75);
        }

        public override Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)(float)pos.X + 10, (int)(float)pos.Y + 10, 75 - 20, 75 - 30);
        }
    }
}
