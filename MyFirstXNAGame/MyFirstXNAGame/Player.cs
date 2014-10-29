using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Player : Actor, IControllable
    {
        Vector2 velocity;
        Vector2 initialVelocity; // TODO: wtf is this?

        Texture2D texture;
        Texture2D textureDead;

        Direction direction;
        Direction fixedBulletDirection;


        Weapon weapon;

        public int id;

        GameObjectBonusList bonusList;

        public Player(Vector2 pos, Vector2 velocity, int id)
            : base(pos, GameObjectType.Player, 100)
        {
            this.initialVelocity = velocity;
            this.id = id;

            this.velocity = Vector2.Zero;
            this.direction = Direction.Up;
            this.isPermanentlyOnTheMap = true;
            this.fixedBulletDirection = Direction.None;
            this.bonusList = new GameObjectBonusList();
        }

        public GameObjectBonusList getBonusList()
        {
            return bonusList;
        }

        public Direction getDirection()
        {
            return direction;
        }
        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            this.weapon = TheGame.Instance.world.getWeapon();


            texture = graphicsContentLoader.Get("Images/player");

            textureDead = graphicsContentLoader.Get("Images/ghostSpawner");

            weapon.LoadContent(graphicsContentLoader);
            base.LoadContent(graphicsContentLoader);

        }

        public override void Update(GameTime gameTime)
        {
            pos += velocity * gameTime.ElapsedGameTime.Milliseconds / 1000;

            weapon.Update(gameTime);
            base.Update(gameTime);
        }

        public void UpdateControl(KeyboardStateCustom keyboardState)
        {
            if (IsDead())
                return;

            bool isAnyMovementButtonPressed = false;

            Vector2 stateVelocity = new Vector2(0, 0);
            if (id == 0)
            {
                if (keyboardState.IsKeyDown(KeysMe.Player1Right))
                {
                    stateVelocity.X = 1;
                    direction = Direction.Right;
                    isAnyMovementButtonPressed = true;
                }
                else if (keyboardState.IsKeyDown(KeysMe.Player1Left))
                {
                    stateVelocity.X = -1;
                    direction = Direction.Left;
                    isAnyMovementButtonPressed = true;
                }
                else
                {
                    stateVelocity.X = 0;
                }

                if (keyboardState.IsKeyDown(KeysMe.Player1Down))
                {
                    stateVelocity.Y = 1;
                    direction = Direction.Down;
                    isAnyMovementButtonPressed = true;
                }
                else if (keyboardState.IsKeyDown(KeysMe.Player1Up))
                {
                    stateVelocity.Y = -1;
                    direction = Direction.Up;
                    isAnyMovementButtonPressed = true;
                }
                else
                {
                    stateVelocity.Y = 0;
                }

                if (keyboardState.IsKeyDown(KeysMe.Player1HoldBulletDirection))
                {
                    fixedBulletDirection = direction;
                }
                else
                {
                    fixedBulletDirection = Direction.None;
                }


                if (keyboardState.IsKeyDown(KeysMe.Player1Fire))
                {
                    Direction bulletDirection = fixedBulletDirection != Direction.None ? fixedBulletDirection : direction;
                    weapon.Fire(pos + new Vector2(getWorldRectangle().Width, getWorldRectangle().Height) / 2, bulletDirection);
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(KeysMe.Player2Right))
                {
                    stateVelocity.X = 1;
                    direction = Direction.Right;
                    isAnyMovementButtonPressed = true;
                }
                else if (keyboardState.IsKeyDown(KeysMe.Player2Left))
                {
                    stateVelocity.X = -1;
                    direction = Direction.Left;
                    isAnyMovementButtonPressed = true;
                }
                else
                {
                    stateVelocity.X = 0;
                }

                if (keyboardState.IsKeyDown(KeysMe.Player2Down))
                {
                    stateVelocity.Y = 1;
                    direction = Direction.Down;
                    isAnyMovementButtonPressed = true;
                }
                else if (keyboardState.IsKeyDown(KeysMe.Player2Up))
                {
                    stateVelocity.Y = -1;
                    direction = Direction.Up;
                    isAnyMovementButtonPressed = true;
                }
                else
                {
                    stateVelocity.Y = 0;
                }

                if (keyboardState.IsKeyDown(KeysMe.Player2HoldBulletDirection))
                {
                    fixedBulletDirection = direction;
                }
                else
                {
                    fixedBulletDirection = Direction.None;
                }


                if (keyboardState.IsKeyDown(KeysMe.Player2Fire))
                {
                    Direction bulletDirection = fixedBulletDirection != Direction.None ? fixedBulletDirection : direction;
                    weapon.Fire(pos + new Vector2(getWorldRectangle().Width, getWorldRectangle().Height) / 2, bulletDirection);
                }
            }

            // TODO: the rest, make it later.


            // TODO: if got mode... AND
            if (keyboardState.IsKeyDown(KeysMe.GMVelocityPlus))
            {
                initialVelocity += new Vector2(2,2);
            }
            if (keyboardState.IsKeyDown(KeysMe.GMVelocityMinus))
            {
                initialVelocity -= new Vector2(2, 2);
            }

            //don't go backwords
            if (initialVelocity.X < 0 || initialVelocity.Y < 0)
                initialVelocity = new Vector2(1, 1);
            pos += stateVelocity * initialVelocity;

            pos = TheGame.Instance.world.GetRightCoordinates(getWorldRectangle(), getCollisionRectangle()); //TODO: lol.. +200?

        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            if (IsAlive())
            {
                Rectangle sourceRectangle = new Rectangle((int)direction * 50, 0, 50, 200);
                //if dead.. draw something else
                worldDrawer.Draw(texture, getWorldRectangle(), sourceRectangle, Color.White, false);
            }
            else
            {
                worldDrawer.Draw(textureDead, getWorldRectangle(), Color.White);
            }


            base.Draw(worldDrawer);
        }
        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 20, 20);
        }
        public override Rectangle getCollisionRectangle()
        {
            return getWorldRectangle();
        }
    }
}
