using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Bullet : Actor
    {
        private static int BulletIdCounter = 0;
        public int id;

        Vector2 velocity;
        Texture2D texture;
        GameObjectType firer;
        public Bullet(Vector2 pos, Vector2 velocity, GameObjectType firer)
            : base(pos, GameObjectType.Bullet, 0, false)
        {
            this.velocity = velocity;
            this.firer = firer;

            this.id = ++BulletIdCounter;

            pos.X -= getWorldRectangle().Width / 2;
            pos.Y -= getWorldRectangle().Height / 2;
        }
        public GameObjectType getFirer()
        {
            return this.firer;
        }

        public int getDamage()
        {
            return 10;
        }

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/bullet");

            base.LoadContent(graphicsContentLoader);
        }

        public override void Update(GameTime gameTime)
        {
            pos += velocity * (float)(gameTime.ElapsedGameTime.TotalSeconds);

            if (TheGame.Instance.world.map.isRectangleFree(getWorldRectangle()))
            {
                List<Direction> list = TheGame.Instance.world.map.rectangleCollisions(getWorldRectangle());
                TheGame.Instance.world.deleteBlock(list[0], getWorldRectangle());
            }

            base.Update(gameTime);
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            float rotationGrades = MathHelper.Pi * 2 / 4;

            Rectangle outputRectangle = getTextureRectangle();

            //XNA doesn't rotate the sprite around the center of gravity.
            if (getDirection() == Direction.Right)
            {
                outputRectangle.X += outputRectangle.Height;
            }
            else if (getDirection() == Direction.Down)
            {
                outputRectangle.X += outputRectangle.Width;
                outputRectangle.Y += outputRectangle.Height;
            }
            else if (getDirection() == Direction.Left)
            {
                outputRectangle.Y += outputRectangle.Width;
            }
            else // if up don't to anything because that's the orientation in the sprite
            {

            }

            worldDrawer.Draw(texture, outputRectangle, texture.Bounds, Color.White,
                            ((int)getDirection() * rotationGrades), Vector2.Zero, SpriteEffects.None, 1.0f);

            base.Draw(worldDrawer);
        }

        public Direction getDirection()
        {
            if (velocity.Y < 0 && velocity.X == 0)
                return Direction.Up;
            else if (velocity.Y > 0 && velocity.X == 0)
                return Direction.Down;
            else if (velocity.Y == 0 && velocity.X > 0)
                return Direction.Right;
            else
                return Direction.Left;
        }

        private Rectangle getTextureRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 10, 15);
        }

        //get a rectangle with the sizes of the bullet in the world
        public override Rectangle getWorldRectangle()
        {
            //down, left.. not working right!
            if(getDirection() == Direction.Up || getDirection() == Direction.Down)
                return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 10, 15);
            else
                return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 15, 10);
        }
        public override Rectangle getCollisionRectangle()
        {
            return getWorldRectangle();
        }
    }
}
