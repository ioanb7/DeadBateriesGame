using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public abstract class ChasingGhost : Monster
    {
        private Vector2 goalPos;
        public ChasingGhost(Vector2 pos, Vector2 velocity, int hp, int damageOnCollision, string textureLocation, string textureDeadLocation)
            : base(pos, velocity, 30, 5, textureLocation, textureDeadLocation, MonsterType.ChasingGhost)
        {
            goalPos = Vector2.Zero;
        }

        public void setGoalPos(Vector2 pos)
        {
            goalPos = pos;
        }

        public Vector2 getCenterPoint()
        {
            return new Vector2(
                getCollisionRectangle().X,
                getCollisionRectangle().Y)
                + new Vector2(
                    getCollisionRectangle().Width,
                    getCollisionRectangle().Height) / 2;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive())
            {
                Vector2 velocityMultiplier = Vector2.Zero;

                Vector2 centerGhostPoint = getCenterPoint();

                // DEVELOPERS: >= and <= .. are okey. but won't happen, anyway. only if they are dead.. maybe
                if (goalPos.X > centerGhostPoint.X)
                    velocityMultiplier.X = 1;
                else
                    velocityMultiplier.X = -1;

                if (goalPos.Y > centerGhostPoint.Y)
                    velocityMultiplier.Y = 1;
                else
                    velocityMultiplier.Y = -1;

                if (velocityMultiplier == new Vector2(1, 1)) // prevent running faster if it goes in corners. 
                    //velocityMultiplier /= 2;//not the best solution, tho. (1^2 + 1^2) / 2 = 1 but this is not the speed. we need V(1^2 + 1^2)
                    velocityMultiplier /= 1.4142f;

                pos += velocity * velocityMultiplier / gameTime.ElapsedGameTime.Milliseconds;
            }

            base.Update(gameTime);
        }
    }
}
