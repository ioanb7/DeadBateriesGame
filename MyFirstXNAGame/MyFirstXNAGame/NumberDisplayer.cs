using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    /// <summary>
    /// change number,
    /// display
    /// loadContent
    /// position
    /// dimensions
    /// </summary>
    public class NumberDisplayer
    {
        Vector2 pos;
        Vector2 dimensions;
        Texture2D texture;
        int frameWidth;
        int frameHeight;

        List<int> decomposedDigits = new List<int>();

        private int _no;
        public int NO
        {
            get
            {
                return _no;
            }
            set
            {
                _no = Math.Abs(value); // would be a bad idea to throw an exception here if <0. lol
                getDecomposedDigits(true);
            }
        }
        public NumberDisplayer(Vector2 pos, Vector2 dimensions)
        {
            this.pos = pos;
            this.dimensions = dimensions;
            decomposedDigits = new List<int>();
            NO = 0;
        }
        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/spriteFont");
            frameWidth = 100;
            frameHeight = 150;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            return;
            Vector2 localPos = pos;

            foreach (int digit in getDecomposedDigits())
            {
                spriteBatch.Draw(texture,
                    new Rectangle((int)(float)localPos.X,
                        (int)(float)localPos.Y,
                        (int)((float)dimensions.X / 100 * frameWidth),
                        (int)((float)dimensions.Y / 100 * frameHeight)),
                    new Rectangle(0, digit * frameHeight, frameWidth, frameHeight),
                    Color.White);
                localPos += new Vector2((int)((float)dimensions.X / 100 * frameWidth), 0);
            }
        }

        private List<int> getDecomposedDigits(bool forceDoingIt = false)
        {
            if (forceDoingIt == false)
                return decomposedDigits;

            decomposedDigits.Clear();

            int no = _no;
            do
            {
                decomposedDigits.Add(no % 10);
                no /= 10;
            } while (no != 0);
            decomposedDigits.Reverse();

            return decomposedDigits;
        }
    }
}
