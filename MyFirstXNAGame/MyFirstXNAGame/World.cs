using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class World
    {
        public Vector2 mapSize;
        public Texture2D texture;
        private decimal currentTime;

        public GameObjectList gameObjectList { get; set; }

        public Map map { get; set; }
        public World(Vector2 mapSize, Map map)
        {
            this.map = map;
            this.mapSize = mapSize;
            gameObjectList = new GameObjectList();

            if(TheGame.Instance.isHavingAUserMap)
            {
                /*
                float playerSpeed = 3.0f;

                

                map.AddMapToGameObjectList(gameObjectList);
                gameObjectList.Add(new Player(map.getPlayerPos(), new Vector2(playerSpeed, playerSpeed), 0));
                //TheGame.Instance.data.
                */
            }
            else
            {

                float playerSpeed = 3.0f;
                map.AddMapToGameObjectList(gameObjectList);
                gameObjectList.Add(new Player(map.getPlayerPos() + new Vector2(5,5), new Vector2(playerSpeed, playerSpeed), 0));

                /*
                float playerSpeed = 3.0f;

                gameObjectList.Add(new Player(new Vector2(50, 50), new Vector2(playerSpeed, playerSpeed), 0));
                if (TheGame.Instance.isMultiplayer)
                    gameObjectList.Add(new Player(new Vector2(50 * 2, 50 * 2), new Vector2(playerSpeed, playerSpeed), 1));

                //add world objects
                gameObjectList.Add(new FiniteSpawner(new Vector2(400, 10), Vector2.Zero, 2)); //.zero is useless
                gameObjectList.Add(new GameObjectBonusHp(new Vector2(300, 300), 30));*/
            }
        }

        public void deleteBlock(Direction direction, Rectangle rect)
        {
            map.deleteBlock(direction, rect);
        }

        public static World getWorld(MapMaker.DataObject data)
        {
            // TODO: this has to be changed.
            return new World(new Vector2(1700, 1080), new Map());
        }

        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            /*
            Block.LoadContent(graphicsContentLoader);
            texture = graphicsContentLoader.Get("Images/map");
            foreach (GameObject gameObject in gameObjectList)
                gameObject.LoadContent(graphicsContentLoader);*/
        }

        public void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.Seconds;
        }

        public decimal getCurrentTime()
        {
            return currentTime;
        }

        public void activateGM()
        {
            foreach (Player player in gameObjectList.getPlayers())
                player.ChangeHP(+10000);
        }

        public void CheckForCollisions()
        {



            /*
            //playerBullet(s)-monster(s)
            foreach(Bullet bullet in gameObjectList.getBullets())
            {
                foreach(Monster monster in gameObjectList.getMonsters())
                {
                    Rectangle bulletWorldRectangle = bullet.getCollisionRectangle();
                    Rectangle actorWorldRectangle = monster.getCollisionRectangle();
                    //check for hit
                    if (bullet.getFirer() == GameObjectType.Player
                        && monster.IsAlive()
                        && bulletWorldRectangle.Intersects(actorWorldRectangle))
                    {
                        monster.ChangeHP(-bullet.getDamage());

                        if (monster.IsDead()) // if he is dead now
                            ++TheGame.Instance.userScore;


                        if(TheGame.Instance.isHavingAUserMap)
                        {
                            if(TheGame.Instance.userScore >= 5)
                            {
                                //show first hp
                            }
                            if (TheGame.Instance.userScore >= 15)
                            {
                                //show second hp
                            }
                        }



                        bullet.shouldBeRemoved = true;
                    }
                    else if (!bullet.getWorldRectangle().Intersects(new Rectangle(0, 0, (int)(float)mapSize.X, (int)(float)mapSize.Y)))
                    {
                        bullet.shouldBeRemoved = true;
                    }
                }
            }

            //player-ghost(s)
            foreach (Player player in gameObjectList.getPlayers())
            {
                if (player.IsAlive())
                {
                    Rectangle playerWorldRectangle = player.getCollisionRectangle();
                    foreach (Monster monster in gameObjectList.getMonsters())
                    {
                        Rectangle ghostWorldRectangle = monster.getCollisionRectangle();
                        if (playerWorldRectangle.Intersects(ghostWorldRectangle) && monster.IsAlive())
                        {
                            //gameObjectList.Remove((GameObject)gameObjectList.getPlayer()); //bug it
                            //see where it hit

                            double atanResult = Math.Atan2(monster.pos.Y - player.pos.Y,
                                monster.pos.X - player.pos.X);

                            float jumpInfluenceSpeed = 35.0f;
                            Vector2 velocity = Vector2.Zero;

                            if (atanResult > 0 && atanResult < Math.PI / 2)
                                velocity = new Vector2(-jumpInfluenceSpeed, +jumpInfluenceSpeed); //quad1
                            if (atanResult > Math.PI / 2 && atanResult <= Math.PI)
                                velocity = new Vector2(+jumpInfluenceSpeed, +jumpInfluenceSpeed); //quad2
                            if (atanResult > -Math.PI && atanResult <= -Math.PI / 2)
                                velocity = new Vector2(+jumpInfluenceSpeed, -jumpInfluenceSpeed); //quad3
                            if (atanResult >= -Math.PI / 2 && atanResult <= 0)
                                velocity = new Vector2(-jumpInfluenceSpeed, -jumpInfluenceSpeed); //quad4

                            //velocity += monster.velocity;
                            monster.pos += -velocity;

                            monster.pos = GetRightCoordinates(monster.getWorldRectangle());


                            player.ChangeHP(-monster.damageUponPlayerCollision);
                        }
                    }
                }
            }

            //player(s)-bonus(es)
            foreach (Player player in gameObjectList.getPlayers())
            {
                foreach(GameObjectBonus bonus in gameObjectList.getBonuses())
                {
                    if (player.getCollisionRectangle().Intersects(bonus.getCollisionRectangle()))
                    {
                        bonus.Consume(player);
                        bonus.shouldBeRemoved = true;
                    }
                }
            }

            //player(s)-teleport(s)
            foreach (Player player in gameObjectList.getPlayers())
            {
                foreach (Teleporter teleporter in gameObjectList.getTeleporters())
                {
                    if (player.getCollisionRectangle().Intersects(teleporter.getCollisionRectangle()))
                    {
                        //this.Exit(); // TODO: change levels
                        TheGame.Instance.changeLevel(teleporter);
                    }
                }
            }
             * */
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    Rectangle worldRectangle = new Rectangle(i * 20, j * 20, 20, 20);
                    RectangleSprite.DrawRectangle(TheGame.Instance.worldDrawer, worldRectangle, Color.Yellow, 1);

                    //map.getBlock(i, j).Draw(spriteBatch);
                    //Block.DrawFor(i * 20, j * 20, map.getBlock(i, j).Type, spriteBatch);
                }
        }

        public void SpawnAGhost(Vector2 pos)
        {
            gameObjectList.Add(new Ghost(pos, new Vector2(10, 10)));
        }

        public void UpdateControl(KeyboardStateCustom keyboardState)
        {
            if (TheGame.Instance.isMultiplayer)
            {
                if (gameObjectList.getPlayers()[0].IsDead() && gameObjectList.getPlayers()[1].IsDead())
                {
                    //stop ghosts if they are dead... or .... GO TO THE LAST POSITION OF THEIRS. why not?
                }
                else if (gameObjectList.getPlayers()[0].IsDead() || gameObjectList.getPlayers()[1].IsDead())
                {
                    Player playerAlive = gameObjectList.getPlayers()[0].IsDead() ? gameObjectList.getPlayers()[1] : gameObjectList.getPlayers()[0];

                    Vector2 playerGoalPos = new Vector2(
                        playerAlive.getCollisionRectangle().X,
                        playerAlive.getCollisionRectangle().Y)
                        + new Vector2(
                            playerAlive.getCollisionRectangle().Width,
                            playerAlive.getCollisionRectangle().Height) / 2;


                    foreach (ChasingGhost chasingGhost in gameObjectList.getChasingGhosts())
                    {
                        chasingGhost.setGoalPos(playerGoalPos);
                    }
                }
                else
                {
                    foreach (ChasingGhost chasingGhost in gameObjectList.getChasingGhosts())
                    {
                        Vector2 player1GoalPos = new Vector2(
                            gameObjectList.getPlayers()[0].getCollisionRectangle().X,
                            gameObjectList.getPlayers()[0].getCollisionRectangle().Y )
                            + new Vector2(
                                gameObjectList.getPlayers()[0].getCollisionRectangle().Width,
                                gameObjectList.getPlayers()[0].getCollisionRectangle().Height )/ 2;
                        Vector2 player2GoalPos = new Vector2(
                            gameObjectList.getPlayers()[1].getCollisionRectangle().X,
                            gameObjectList.getPlayers()[1].getCollisionRectangle().Y)
                            + new Vector2(
                                gameObjectList.getPlayers()[1].getCollisionRectangle().Width,
                                gameObjectList.getPlayers()[1].getCollisionRectangle().Height) / 2;

                        Vector2 chasingGhostCenterPoint = chasingGhost.getCenterPoint();


                        Vector2 dif1 = chasingGhostCenterPoint - player1GoalPos;
                        Vector2 dif2 = chasingGhostCenterPoint - player2GoalPos;

                        if (Math.Abs(dif1.Length()) > Math.Abs(dif2.Length()))
                            chasingGhost.setGoalPos(player1GoalPos);
                        else
                            chasingGhost.setGoalPos(player2GoalPos);
                    }
                }
            }
            else
            {

                Player player = gameObjectList.getPlayers()[0];
                Vector2 playerGoalPos = new Vector2(
                        player.getCollisionRectangle().X,
                        player.getCollisionRectangle().Y)
                        + new Vector2(
                            player.getCollisionRectangle().Width,
                            player.getCollisionRectangle().Height) / 2;


                foreach (ChasingGhost chasingGhost in gameObjectList.getChasingGhosts())
                {
                    chasingGhost.setGoalPos(playerGoalPos);
                }
            }

            foreach (Player player in gameObjectList.getPlayers())
                player.UpdateControl(keyboardState);
        }

        public bool isOutsideMap(Rectangle rect)
        {
            if (rect.X < 0) return true;
            if (rect.X + rect.Height > mapSize.X) return true;

            if (rect.Y < 0) return true;
            if (rect.Y + rect.Width > mapSize.Y) return true;

            return false;
        }

        /// <summary>
        /// If a game collides a unit or it outside the world, put him back on the track.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public Vector2 GetRightCoordinates(Rectangle rect)
        {
            Vector2 newPos = new Vector2(rect.X, rect.Y);

            if (rect.X + rect.Width >= mapSize.X)
            {
                newPos.X = mapSize.X - rect.Width;
            }
            if (newPos.X < 0)
            {
                newPos.X = 0;
            }

            if (rect.Y + rect.Height >= mapSize.Y)
            {
                newPos.Y = mapSize.Y - rect.Height;
            }
            if (newPos.Y < 0)
            {
                newPos.Y = 0;
            }

            return newPos;
        }

        /// <summary>
        /// If a game collides a unit or it outside the world, put him back on the track.
        /// </summary>
        /// <param name="displayRect"></param>
        /// <returns></returns>
        public Vector2 GetRightCoordinates(Rectangle displayRect, Rectangle collisionRect)
        {
            //Vector2 newPos = new Vector2(displayRect.X, displayRect.Y);
            Vector2 newPos = new Vector2(collisionRect.X, collisionRect.Y);

            if (collisionRect.X + collisionRect.Width >= mapSize.X)
            {
                newPos.X = mapSize.X - collisionRect.Width;
            }
            if (newPos.X < 0)
            {
                newPos.X = 0;
            }

            if (collisionRect.Y + collisionRect.Height >= mapSize.Y)
            {
                newPos.Y = mapSize.Y - collisionRect.Height;
            }
            if (newPos.Y < 0)
            {
                newPos.Y = 0;
            }

            /////
            newPos.X -= (collisionRect.X - displayRect.X);
            newPos.Y -= (collisionRect.Y - displayRect.Y);

            return newPos;
        }

        public Weapon getWeapon()
        {
            return Weapon.getWeapon(gameObjectList);
        }
    }
}
