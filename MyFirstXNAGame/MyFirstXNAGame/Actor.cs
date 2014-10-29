using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    
    public enum ActorState
    {
        Alive,
        Dead
    }
    public class Actor : GameObject
    {
        private int HP { get; set; } //current hp
        public int MaxHP { get; set; } //the start hp

        public ActorState actorState; // is it dead, or alive? or?..
        public decimal timeOfDeath; // used for animations

        //Actor's health
        HealthDisplayer healthDisplayer; 
        private bool isHealthDisplayerActivated;
        public Actor(Vector2 pos, GameObjectType type, int hp, bool isHealthDisplayerActivated = true)
            : base(pos, type)
        {
            this.HP = this.MaxHP = hp;
            actorState = ActorState.Alive;
            this.isHealthDisplayerActivated = isHealthDisplayerActivated;

            commonInitializers();
        }
        public Actor(Vector2 pos, GameObjectType type, int hp, int maxHp, bool isHealthDisplayerActivated = true)
            : base(pos, type)
        {
            this.HP = hp;
            this.MaxHP = maxHp;
            this.isHealthDisplayerActivated = isHealthDisplayerActivated;

            commonInitializers();
        }

        /// <summary>
        /// little helper
        /// </summary>
        private void commonInitializers()
        {

            int asd = 15;
            if (isHealthDisplayerActivated)
            {
                healthDisplayer = new HealthDisplayer(new Vector2(asd, asd));
                healthDisplayer.NO = Math.Max(0, this.HP);
            }
        }
        public void ChangeHP(int hp)
        {
            this.HP += hp;
            if (isHealthDisplayerActivated)
            {
                healthDisplayer.NO = Math.Max(0, this.HP);
            }

            if (this.HP <= 0)
            {
                actorState = ActorState.Dead;
                timeOfDeath = TheGame.Instance.world.getCurrentTime();
            }
        }
        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            state = GameObjectState.Created;
            if (isHealthDisplayerActivated)
            {
                healthDisplayer.LoadContent(graphicsContentLoader);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsDead() && TheGame.Instance.world.getCurrentTime() - timeOfDeath > 1 && isPermanentlyOnTheMap == false)
                shouldBeRemoved = true;

            if (isHealthDisplayerActivated)
            {
                healthDisplayer.Update(gameTime);
            }
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            if (isHealthDisplayerActivated)
            {
                //draw health
                healthDisplayer.pos = new Vector2(getCollisionRectangle().X, getCollisionRectangle().Y - 20);
                healthDisplayer.Draw(worldDrawer);
            }
        }
        public bool IsDead()
        {
            return this.actorState == ActorState.Dead;
        }
        public bool IsAlive()
        {
            return this.actorState == ActorState.Alive;
        }
        public override Rectangle getWorldRectangle()
        {
            throw new NotImplementedException();
        }
        public override Rectangle getCollisionRectangle()
        {
            throw new NotImplementedException();
        }

    }
}
