using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Spawner : GameObject
    {
        Vector2 dimensions;
        Texture2D texture;
        Texture2D textureDead;

        private decimal lastTimeSpawned = 0;
        static private decimal spawnInterval = 1000 * 10 * 5;

        public bool isDead = false;
        public int spawnedMobs = 0;

        SpawnerType spawnerType;

        public Spawner(Vector2 pos, Vector2 dimensions, SpawnerType spawnerType) : base(pos, GameObjectType.Spawner)
        {
            this.dimensions = dimensions;
            this.spawnerType = spawnerType;
        }

        public override void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            texture = graphicsContentLoader.Get("Images/ghostSpawner");
            textureDead = graphicsContentLoader.Get("Images/ghostSpawnerDead");
            state = GameObjectState.Created;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isDead)
            {
                lastTimeSpawned += gameTime.TotalGameTime.Milliseconds;
                if (lastTimeSpawned - spawnInterval >= 0)
                {
                    lastTimeSpawned %= spawnInterval; // TODO: % really? hmm! fewer mobs if the pc is lagging really bad.
                }
                else
                {
                    return;
                }

                TheGame.Instance.world.SpawnAGhost(pos + new Vector2(30, 30));
                ++spawnedMobs;
            }
        }

        public override void Draw(WorldDrawer worldDrawer)
        {
            if(!isDead)
                worldDrawer.Draw(texture, getWorldRectangle(), Color.White);
            else
                worldDrawer.Draw(textureDead, getWorldRectangle(), Color.White);
        }

        public override Rectangle getWorldRectangle()
        {
            return new Rectangle((int)(float)pos.X, (int)(float)pos.Y, 50, 100);
        }

        public override Rectangle getCollisionRectangle()
        {
            return new Rectangle((int)(float)pos.X + 5, (int)(float)pos.Y + 5, 50 - 5 * 2, 100 - 5 * 2);
        }
    }
}
