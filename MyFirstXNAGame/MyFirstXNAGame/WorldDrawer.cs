using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class WorldDrawer
    {
        SpriteBatch spriteBatch;
        public bool isCentered { get; set; }
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return spriteBatch.GraphicsDevice;
            }
        }

        public WorldDrawer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            isCentered = true;
            //playerPos = Vector2.Zero;
        }

        public Vector2 displaySize { get; set; }//{ private get; public set; }
        public Vector2 displayPos { get; set; }
        public Vector2 pos { get; set; }//{ private get; public set; }

        public Rectangle getScreenRect()
        {
            return new Rectangle((int)(float)displayPos.X, (int)(float)displayPos.Y, (int)(float)displaySize.X, (int)(float)displaySize.Y);
        }

        //got to modify destination as well as source when im-drawing--it's bugged if it goes beyond the screensize. :-/'
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            destinationRectangle = adjustDestinationRectangle(destinationRectangle);
            if (destinationRectangle.Intersects(getScreenRect()) && sourceRectangle.HasValue == true)
                spriteBatch.Draw(texture, destinationRectangle, adjustSourceRectangle((Rectangle)sourceRectangle, destinationRectangle, texture.Bounds), color, rotation, origin, effects, layerDepth);
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            Draw(texture, destinationRectangle, new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height), color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="destinationRectangle"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="color"></param>
        /// <param name="doAdjustSourceRectangle">false if it's a sprite</param>
        public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle, Color color, bool doAdjustSourceRectangle = true)
        {
            if (doAdjustSourceRectangle)
                sourceRectangle = adjustSourceRectangle(sourceRectangle, destinationRectangle, texture.Bounds);
            else
                sourceRectangle = adjustSourceRectangle(sourceRectangle, destinationRectangle, sourceRectangle);

            destinationRectangle = adjustDestinationRectangle(destinationRectangle);

            ///
            destinationRectangle.X += (int)displayPos.X;
            destinationRectangle.Y += (int)displayPos.Y;

            ///
            //if (destinationRectangle.Intersects(getScreenRect()))
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

        public Rectangle adjustDestinationRectangle(Rectangle destinationRectangle)
        {
            int x = destinationRectangle.X - (int)(float)pos.X;
            int y = destinationRectangle.Y - (int)(float)pos.Y;
            int width = destinationRectangle.Width;
            int height = destinationRectangle.Height;

            if (x + width > displaySize.X)
                width = (int)displaySize.X - x;
            if (y + height > displaySize.Y)
                height = (int)displaySize.Y - y;

            return new Rectangle(x, y, width, height);
        }

        public Rectangle adjustSourceRectangle(Rectangle sourceRectangle, Rectangle originalDestinationRectangle, Rectangle textureBounds)
        {
            return new Rectangle(sourceRectangle.X, sourceRectangle.Y,

                (int)(Math.Min((float)originalDestinationRectangle.Width,
                    (float)(pos.X + displaySize.X - (float)originalDestinationRectangle.X)) / (float)originalDestinationRectangle.Width * textureBounds.Width),

                (int)(Math.Min((float)originalDestinationRectangle.Height,
                    (float)(pos.Y + displaySize.Y - (float)originalDestinationRectangle.Y)) / (float)originalDestinationRectangle.Height * textureBounds.Height)
            );
        }
    }
}
