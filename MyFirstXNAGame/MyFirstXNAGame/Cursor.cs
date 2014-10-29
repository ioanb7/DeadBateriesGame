using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    /// <summary>
    /// Options:
    /// change number,
    /// display
    /// loadContent
    /// position
    /// dimensions
    /// </summary>
    public class Cursor
    {
        Texture2D texture;
        public Vector2 pos {get; set;}
        public Cursor()
        {
            pos = Vector2.Zero;
        }
        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/cursor2");
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                pos,
                Color.White);
        }
    }
}
