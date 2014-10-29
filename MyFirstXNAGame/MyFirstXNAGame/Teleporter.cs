using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Teleporter : GameObject
    {
        Texture2D texture;

        int spriteI = 0;

        private decimal lastSpriteChange = 0;
        static private decimal spriteChangeInterval = 10000;
        public Teleporter(Vector2 pos)
            : base(pos, GameObjectType.Teleporter)
        {

        }

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/teleporter");
            state = GameObjectState.Created;
        }

        public override void Update(GameTime gameTime)
        {
            lastSpriteChange += gameTime.TotalGameTime.Milliseconds;

            if (lastSpriteChange - spriteChangeInterval >= 0)
            {
                lastSpriteChange %= spriteChangeInterval;

                spriteI++;
                spriteI %= 2;
            }
            else
            {
                return;
            }
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            Rectangle sourceRectangle = getWorldRectangle();
            sourceRectangle.Y = spriteI * sourceRectangle.Height;

            worldDrawer.Draw(texture, getWorldRectangle(), sourceRectangle, Color.White, false);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 125, 125);
        }

        public override Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)(float)pos.X + 25, (int)(float)pos.Y + 25, 75 - 20, 75 - 25);
        }
    }
}
