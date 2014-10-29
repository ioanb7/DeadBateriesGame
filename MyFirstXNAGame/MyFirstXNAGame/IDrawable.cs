using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public interface IDrawable
    {
        void LoadContent(GraphicsContentLoader graphicsContentLoader);
        void Update(GameTime gameTime);
        void Draw(WorldDrawer worldDrawer);
    }
}
