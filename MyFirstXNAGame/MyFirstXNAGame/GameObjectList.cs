using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameObjectList : List<GameObject>
    {
        public GameObjectList()
        {
            playersCache = new List<Player>();
        }

        private List<Player> playersCache;
        /// <summary>
        /// Warning: once this called, the cache doesn't get updated
        /// </summary>
        /// <returns></returns>
        public List<Player> getPlayers()
        {
            if (playersCache.Count == 0)
            {
                foreach (GameObject gameObject in this)
                {
                    if (gameObject.Type == GameObjectType.Player)
                        playersCache.Add((Player)gameObject);
                }
            }

            return playersCache;
        }

        /// <summary>
        /// Warning: once this called, the cache doesn't get updated
        /// </summary>
        /// <returns></returns>
        public Player getPlayer(int id)
        {
            return getPlayers()[id];
        }


        public IEnumerable<GameObjectBlock> getGameObjects()
        {
            foreach (GameObject gameObject in this)
            {
                 yield return (GameObjectBlock)gameObject;
            }
        }

        public IEnumerable<Bullet> getBullets()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Bullet)
                    yield return (Bullet)gameObject;
            }
        }

        public IEnumerable<Actor> getActors()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Player ||
                    gameObject.Type == GameObjectType.Monster)
                    yield return (Actor)gameObject;
            }
        }

        public IEnumerable<Monster> getMonsters()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Monster)
                    yield return (Monster)gameObject;
            }
        }

        public IEnumerable<ChasingGhost> getChasingGhosts()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Monster && ((Monster)gameObject).monsterType == MonsterType.ChasingGhost)
                    yield return (ChasingGhost)gameObject;
            }
        }

        public IEnumerable<Spawner> getSpawners()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Spawner)
                    yield return (Spawner)gameObject;
            }
        }

        public IEnumerable<GameObjectBonus> getBonuses()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Bonus)
                    yield return (GameObjectBonus)gameObject;
            }
        }

        public IEnumerable<Teleporter> getTeleporters()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Teleporter)
                    yield return (Teleporter)gameObject;
            }
        }

        public GameObjectKey getGameObjectKey()
        {
            foreach (GameObject gameObject in this)
            {
                if (gameObject.Type == GameObjectType.Key)
                    return (GameObjectKey)gameObject;
            }

            return null;
        }

        /// <summary>
        /// Delete the object that are no use anymore
        /// </summary>
        public void Clean()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].shouldBeRemoved == true)
                    this.RemoveAt(i);
            }
        }
    }
}
