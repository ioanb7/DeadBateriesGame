using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class FiniteSpawner : Spawner
    {
        int maxMobs;
        public FiniteSpawner(Vector2 pos, Vector2 dimensions, int maxMobs) : base(pos, dimensions, SpawnerType.GhostSpawner)
        {
            this.maxMobs = maxMobs;
        }

        public override void Update(GameTime gameTime)
        {
            if (maxMobs <= spawnedMobs)
                isDead = true;

            base.Update(gameTime);
        }
    }
}
