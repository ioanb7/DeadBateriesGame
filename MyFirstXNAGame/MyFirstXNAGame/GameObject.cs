using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public abstract class GameObject : IDrawable, IObject
    {
        public GameObjectState state; // waiting to be in the removed state? is it recycled?
        public bool shouldBeRemoved;
        protected bool isPermanentlyOnTheMap; // the player or someother objectives that are killed might be.
        public Vector2 mpos;
        public Vector2 pos
        {
            get { return mpos; }
            set { mpos = value; }
        }

        public GameObjectType Type { get; private set; }

        public GameObject(Vector2 pos, GameObjectType type)
        {
            this.pos = pos;
            this.Type = type;
            shouldBeRemoved = false;
            state = GameObjectState.NewlyCreated;
            isPermanentlyOnTheMap = false;
        }

        public abstract void LoadContent(GraphicsContentLoader graphicsContentLoader);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(WorldDrawer worldDrawer);
        public abstract Rectangle getCollisionRectangle();
        public abstract Rectangle getWorldRectangle();
    }
}
