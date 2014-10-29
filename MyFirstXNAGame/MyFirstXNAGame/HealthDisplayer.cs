using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class HealthDisplayer
    {
        public Vector2 pos;
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
                _no = Math.Abs(value);
                getDecomposedDigits(true);
            }
        }
        public HealthDisplayer(Vector2 dimensions)
        {
            this.pos = Vector2.Zero;
            this.dimensions = dimensions;
            decomposedDigits = new List<int>();
            NO = 0;
        }
        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/spriteFont");
            frameWidth = 10 * 10;//9;
            frameHeight = 15 * 10;
        }

        public void Update(GameTime gameTime)
        {

        }


        public void Draw(WorldDrawer worldDrawer)
        {
            Vector2 localPos = pos;

            foreach (int digit in getDecomposedDigits())
            {
                worldDrawer.Draw(texture,
                    new Rectangle((int)(float)localPos.X,
                        (int)(float)localPos.Y,
                        (int)(dimensions.X),
                        (int)(dimensions.Y)),
                    new Rectangle(0, digit * frameHeight, frameWidth, frameHeight),
                    Color.White, false);
                localPos += new Vector2(dimensions.X, 0);
                /*
                spriteBatch.Draw(texture,
                    new Rectangle((int)(float)localPos.X, (int)(float)localPos.Y, frameWidth, 15),
                    new Rectangle(digit * frameWidth, 0, frameWidth, 15),
                    Color.White);
                 * */
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
