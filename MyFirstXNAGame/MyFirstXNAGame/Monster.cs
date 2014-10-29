using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Monster : Actor
    {
        public Vector2 velocity { get; set; }

        Texture2D texture;
        Texture2D textureDead;
        string textureLocation;
        string textureDeadLocation;

        Direction direction;
        public int damageUponPlayerCollision;
        public MonsterType monsterType;

        public Monster(Vector2 pos, Vector2 velocity, int hp, int damageUponPlayerCollision, string textureLocation, string textureDeadLocation, MonsterType monsterType)
            : base(pos, GameObjectType.Monster, hp)
        {
            this.velocity = velocity;
            this.direction = Direction.Up;
            this.textureLocation = textureLocation;
            this.textureDeadLocation = textureDeadLocation;
            this.damageUponPlayerCollision = damageUponPlayerCollision;
            this.monsterType = monsterType;
        }

        public Direction getDirection()
        {
            return direction;
        }
        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get(textureLocation);
            textureDead = graphicsContentLoader.Get(textureDeadLocation);

            base.LoadContent(graphicsContentLoader);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            //if dead.. draw something else
            if (IsAlive())
                worldDrawer.Draw(texture, getWorldRectangle(), Color.White);
            else
                worldDrawer.Draw(textureDead, getWorldRectangle(), Color.White);
            base.Draw(worldDrawer);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 75, 75);
        }

        public override Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)(float)pos.X + 10, (int)(float)pos.Y + 10, 75 - 20, 75 - 20);
        }
    }
}
