using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    // TODO: abstract?
    public abstract class GameObjectBonus : GameObject
    {
        public Vector2 velocity { get; set; }

        Texture2D texture;
        string textureLocation;

        public GameObjectBonus(Vector2 pos, string textureLocation)
            : base(pos, GameObjectType.Bonus)
        {
            this.textureLocation = textureLocation;
        }

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get(textureLocation);
            state = GameObjectState.Created;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            //if (IsAlive())
            worldDrawer.Draw(texture, getWorldRectangle(), Color.White);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 75, 75);
        }

        public override Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)(float)pos.X + 10, (int)(float)pos.Y + 10, 75 - 20, 75 - 30);
        }

        public abstract void Consume(Player player);
        //public abstract GameObjectBonusList Consume(Player player);
        //{
            /*
            GameObjectBonusList gameObjectBonusList = new GameObjectBonusList();

            gameObjectBonusList.Add(this);

            this.shouldBeRemoved = true;

            return gameObjectBonusList;*/
        //}
    }
}
