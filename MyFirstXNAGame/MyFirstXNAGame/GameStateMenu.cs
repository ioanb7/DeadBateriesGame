using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameStateMenu : IDrawable
    {
        public Vector2 pos;
        Vector2 dimensions;
        Texture2D texture;

        public GameStateMenu(Vector2 dimensions)
        {
            this.pos = Vector2.Zero;
            this.dimensions = dimensions;
        }
        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/spriteFont");
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(WorldDrawer worldDrawer)
        {

        }

    }
}
