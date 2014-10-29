using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyFirstXNAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsContentLoader graphicsContentLoader;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool isPaused = false;
        bool isHavingAUserMap
        {
            get
            {
                return TheGame.Instance.isHavingAUserMap;
            }
            set
            {
                TheGame.Instance.isHavingAUserMap = value;
            }
        }

        World world;
        NumberDisplayer numberDisplayer;
        Cursor cursor;

        Vector2 displaySize;
        public Vector2[] mapPos;

        Direction mapDirection;

        bool isGameFinished = false;

#if DEBUG
        bool isInDev = true;
#else
        bool isInDev = false;
#endif

        bool isMultiplayer
        {
            get
            {
                return TheGame.Instance.isMultiplayer;
            }
        }

        public Game1(bool isHavingAUserDefinedMap)
        {
            this.isHavingAUserMap = isHavingAUserDefinedMap;
            int width = 1024;
            int height = 600;

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            Content.RootDirectory = "Content";

            // DELETE THIS WHEN A DEVELOPER SEES IT: wtf? why is this wrong?... the window is not this size. THIS IS STUPID. IT's not always 800x600, fools.
            //world = new World(new Vector2(0, 0), new Vector2(2000, 2000), new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height), ref gameObjectList);


            if (this.isHavingAUserMap == true)
            {
                //load the json here....
                string json = System.IO.File.ReadAllText("Content/Maps/map1/map.data");
                TheGame.Instance.data = JsonConvert.DeserializeObject<MapMaker.DataObject>(json);
                JToken token = JObject.Parse(json);
                MapMaker.GameObjectSpawnerBase spawner = (MapMaker.GameObjectSpawnerBase)TheGame.Instance.data.gameObjects[0];
                world = World.getWorld(TheGame.Instance.data);
            }
            else
            {
                int mapWidth = 10;
                int mapHeight = 10;

                Map map = new Map();
                map.setWidth(mapWidth);
                map.setHeight(mapHeight);
                //map.setBlockSize(20);
                map.setBlockSizeWidth(40); // TODO: replace this with displaySize.X / mapWidth ----  Math.floor( it )
                map.setBlockSizeHeight(40);
                map.create();
                map.makeEveryBlockDirty();
                map.setPlayerStartPos(new Point(2, 2));
                map.setPlayerPeasAtTheBeggining(50);
                map.setBonusPeasAt(new Point(5, 5));
                map.setEndPortalAt(new Point(10, 10));

                world = new World(new Vector2(500, 500), map);
            }



            displaySize = new Vector2(width, height);
            mapPos = new Vector2[2];
            mapPos[0] = new Vector2(0, 0);
            mapPos[1] = new Vector2(0, 0);
            numberDisplayer = new NumberDisplayer(new Vector2(5, 5), new Vector2(100, 100));
            TheGame.Instance.numberDisplayer = numberDisplayer;
            TheGame.Instance.world = world;
            TheGame.Instance.SoundManager = new SoundManager(@"Content\Audio\GameAudio.xgs", @"Content\Audio\Wave Bank.xwb", @"Content\Audio\Sound Bank.xsb");

            cursor = new Cursor();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Add your initialization logic here

            //initialize the player(s)
            // TODO: add world here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TheGame.Instance.worldDrawer = new WorldDrawer(spriteBatch);

            graphicsContentLoader = new GraphicsContentLoader(Content);
            TheGame.Instance.animationPopper = new AnimationPopper(spriteBatch);
            cursor.LoadContent(graphicsContentLoader);
            TheGame.Instance.numberDisplayer.LoadContent(graphicsContentLoader);

            //precache somethings
            graphicsContentLoader.Load("Images/bullet");
            graphicsContentLoader.Load("Images/logo"); //@TODO: make this dynamic.. .tt file pt ca e rapid

            SoundPlayer.beginGame();

            world.LoadContent(graphicsContentLoader);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardStateCustom keyboardState = KeyboardStateCustom.getKeyboardState(Keyboard.GetState());
            cursor.pos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            BlockMouse();
            cursor.Update(gameTime);
            CheckGlobalKeyboardControls(keyboardState);
            TheGame.Instance.SoundManager.Update();

            if (!isPaused)
            {
                world.Update(gameTime);
                UpdateGameObjectList(gameTime);
                world.UpdateControl(keyboardState);
                AdjustViewport(keyboardState, mouseState, 0);
                if (isMultiplayer)
                    AdjustViewport(keyboardState, mouseState, 1);
                LookForGameEnd();
                world.CheckForCollisions(); // TODO: world?
                TheGame.Instance.numberDisplayer.Update(gameTime);
                TheGame.Instance.world.gameObjectList.Clean();
            }


            base.Update(gameTime);
        }

        private void CheckGlobalKeyboardControls(KeyboardStateCustom keyboardState)
        {
            if (keyboardState.IsKeyDown(KeysMe.PauseGame))
                isPaused = !isPaused;
            if (keyboardState.IsKeyDown(KeysMe.ExitGame))
                this.Exit();
            if (keyboardState.IsKeyDown(KeysMe.InDevDeactivate))
                isInDev = false;
            if (keyboardState.IsKeyDown(KeysMe.InDevActivate))
                isInDev = true;
            if (keyboardState.IsKeyDown(KeysMe.GameActivateGM))
                world.activateGM();
        }

        private void UpdateGameObjectList(GameTime gameTime)
        {
            for (int i = 0; i < TheGame.Instance.world.gameObjectList.Count; ++i)
            {
                GameObject gameObject = TheGame.Instance.world.gameObjectList[i];
                if (gameObject.state == GameObjectState.NewlyCreated)
                {
                    gameObject.LoadContent(graphicsContentLoader);
                }
                else
                {
                    gameObject.Update(gameTime);
                }
            }
        }

        private void LookForGameEnd()
        {
            /*
            //look for game end
            bool areAllSpawnersDead = true;
            foreach (Spawner spawner in TheGame.Instance.world.gameObjectList.getSpawners())
            {
                if (spawner.isDead == false)
                {
                    areAllSpawnersDead = false;
                }
            }
            if (areAllSpawnersDead)
            {
                //check if all the mobs are dead
                bool areAllMonstersDead = true;
                foreach (Monster monster in TheGame.Instance.world.gameObjectList.getMonsters())
                {
                    if (monster.IsAlive())
                    {
                        areAllMonstersDead = false;
                        break;
                    }
                }

                if (areAllMonstersDead && isGameFinished == false)
                {
                    if (TheGame.Instance.isHavingAUserMap)
                    {
                        //make the key visible
                    }
                    else
                    {
                        TheGame.Instance.world.gameObjectList.Add(new GameObjectKey(new Vector2(500, 500)));
                    }
                    isGameFinished = true;
                }
            }
            // TODO: game finished => gameState make a game state instead.
            if (isGameFinished) // if so, it should be a key on the map.
            {
                GameObjectKey key = TheGame.Instance.world.gameObjectList.getGameObjectKey();
                if (key != null)
                {
                    foreach (Player player in TheGame.Instance.world.gameObjectList.getPlayers())
                    {
                        if (key.getCollisionRectangle().Intersects(player.getCollisionRectangle()))
                        {
                            TheGame.Instance.world.gameObjectList.Remove(
                                TheGame.Instance.world.gameObjectList.getGameObjectKey()
                            );


                            if (TheGame.Instance.isHavingAUserMap)
                            {
                                //make the teleporter visible
                            }
                            else
                            {
                                TheGame.Instance.world.gameObjectList.Add(new Teleporter(new Vector2(0, 0)));
                            }
                            break;
                        }
                    }
                }
            }
             * */
        }

        private void AdjustViewport(KeyboardStateCustom keyboardState, MouseState mouseState, int playerId)
        {
            Vector2 internalDisplaySize = displaySize;
            if(isMultiplayer)
                internalDisplaySize = new Vector2(displaySize.X / 2, displaySize.Y);

            if (TheGame.Instance.worldDrawer.isCentered == false)
            {
                Vector2 stateVelocity = new Vector2(0, 0);


                if (keyboardState.IsKeyDown(KeysMe.ViewportMoveRight))
                {
                    stateVelocity.X = 1;
                    mapDirection = Direction.Right;
                }
                else if (keyboardState.IsKeyDown(KeysMe.ViewportMoveLeft))
                {
                    stateVelocity.X = -1;
                    mapDirection = Direction.Left;
                }
                else
                {
                    stateVelocity.X = 0;
                }

                if (keyboardState.IsKeyDown(KeysMe.ViewportMoveDown))
                {
                    stateVelocity.Y = 1;
                    mapDirection = Direction.Down;
                }
                else if (keyboardState.IsKeyDown(KeysMe.ViewportMoveUp))
                {
                    stateVelocity.Y = -1;
                    mapDirection = Direction.Up;
                }
                else
                {
                    stateVelocity.Y = 0;
                }

                Vector2 multiplyVector = new Vector2(5, 5);
                if (!isMultiplayer)
                {
                    int boundery = 20;
                    int bounderySmaller = boundery / 3;
                    float bounderyMultiplyValue = 1.5f;
                    float bounderySmallerMultiplyValue = 1.0f + bounderyMultiplyValue;
                    if (mouseState.X < boundery)
                    {
                        stateVelocity.X = -1;
                        multiplyVector *= bounderyMultiplyValue;
                    }
                    if (mouseState.Y < boundery)
                    {
                        stateVelocity.Y = -1;
                        multiplyVector *= bounderyMultiplyValue;
                    }
                    if (mouseState.X > displaySize.X - boundery)
                    {
                        stateVelocity.X = 1;
                        multiplyVector *= bounderyMultiplyValue;
                    }
                    if (mouseState.Y > displaySize.Y - boundery * 8)
                    {
                        stateVelocity.Y = 1;
                        multiplyVector *= bounderyMultiplyValue;
                    }


                    if (mouseState.X < bounderySmaller)
                    {
                        stateVelocity.X = -1;
                        multiplyVector *= bounderySmallerMultiplyValue;
                    }
                    if (mouseState.Y < bounderySmaller)
                    {
                        stateVelocity.Y = -1;
                        multiplyVector *= bounderySmallerMultiplyValue;
                    }
                    if (mouseState.X > displaySize.X - bounderySmaller)
                    {
                        stateVelocity.X = 1;
                        multiplyVector *= bounderySmallerMultiplyValue;
                    }
                    if (mouseState.Y > displaySize.Y - boundery * 8 + boundery / 4)
                    {
                        stateVelocity.Y = 1;
                        multiplyVector *= bounderySmallerMultiplyValue;
                    }
                }
                mapPos[0] += stateVelocity * multiplyVector;

                mapPos[0] = TheGame.Instance.world.GetRightCoordinates(new Rectangle(
                    (int)(float)mapPos[0].X,
                    (int)(float)mapPos[0].Y,
                    (int)(float)displaySize.X,
                    (int)(float)displaySize.Y));
            }
            else
            {
                Vector2 mapPosTemp = Vector2.Zero;
                Vector2 playerPos = TheGame.Instance.world.gameObjectList.getPlayer(playerId).pos + new Vector2(
                    TheGame.Instance.world.gameObjectList.getPlayer(playerId).getWorldRectangle().Width,
                    TheGame.Instance.world.gameObjectList.getPlayer(playerId).getWorldRectangle().Height) / 2;

                mapPosTemp = playerPos - internalDisplaySize / 2;

                if (playerPos.X > internalDisplaySize.X / 2)
                    mapPosTemp.X = playerPos.X - internalDisplaySize.X / 2;
                if (playerPos.Y > internalDisplaySize.Y / 2)
                    mapPosTemp.Y = playerPos.Y - internalDisplaySize.Y / 2;

                if (mapPosTemp.X < 0)
                    mapPosTemp.X = 0;
                if (mapPosTemp.Y < 0)
                    mapPosTemp.Y = 0;

                if (mapPos[playerId].X + internalDisplaySize.X > world.mapSize.X)
                    mapPos[playerId].X = world.mapSize.X - internalDisplaySize.X;

                if (mapPos[playerId].Y + internalDisplaySize.Y > world.mapSize.Y)
                    mapPos[playerId].Y = world.mapSize.Y - internalDisplaySize.Y;

                mapPos[playerId] = mapPosTemp;
                mapPos[playerId] = TheGame.Instance.world.GetRightCoordinates(new Rectangle(
                    (int)(float)mapPos[playerId].X,
                    (int)(float)mapPos[playerId].Y,
                    (int)(float)internalDisplaySize.X,
                    (int)(float)internalDisplaySize.Y));
            }
        }

        private void BlockMouse()
        {
            int minValue = 3;
            Point mousePoint = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            if (mousePoint.X < minValue)
                mousePoint.X = minValue;
            if (mousePoint.Y < minValue)
                mousePoint.Y = minValue;
            if (mousePoint.Y > (int)displaySize.Y - minValue)
                mousePoint.Y = (int)displaySize.Y - minValue;
            if (mousePoint.X > (int)displaySize.X - minValue)
                mousePoint.X = (int)displaySize.X - minValue;
            if (IsActive)
                Mouse.SetPosition(mousePoint.X, mousePoint.Y);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // Add your drawing code here
            Vector2 displayPos = Vector2.Zero;


            // TODO: do show a dead animation on the player's screen (player1 or player2) if she's dead.
            spriteBatch.Begin();
                world.Draw(spriteBatch);
                bool displayViewport2 = isMultiplayer;

                if(displayViewport2 == true && 
                    new Rectangle(
                        (int)(float)mapPos[0].X,
                        (int)(float)mapPos[0].Y,
                        (int)(float)displaySize.X / 2,
                        (int)(float)displaySize.Y / 2
                    ).Intersects(new Rectangle(
                        (int)(float)mapPos[1].X,
                        (int)(float)mapPos[1].Y,
                        (int)(float)displaySize.X / 2,
                        (int)(float)displaySize.Y / 2
                    )))
                    displayViewport2 = false;
                //draw the player(s) screen
                for (int i = 0; i < (displayViewport2 ? 2 : 1); i++)
                {
                    Vector2 internalDisplaySize = displaySize;
                    if (isMultiplayer && displayViewport2)
                    {
                        internalDisplaySize = new Vector2(displaySize.X / 2, displaySize.Y);
                    }

                    if(displayViewport2)
                        if (i == 1)
                        {
                            displayPos.X += internalDisplaySize.X;
                        }

                    //set up the worldDrawer
                    TheGame.Instance.worldDrawer.displaySize = internalDisplaySize;
                    TheGame.Instance.worldDrawer.pos = mapPos[i];
                    TheGame.Instance.worldDrawer.displayPos = displayPos;

                    //display the map background
                    DrawBackgroundImage(ref internalDisplaySize, ref displayPos, i);
                    DrawVisibleGameObjects(ref internalDisplaySize, ref displayPos, i);
                }

                ///=====================================
                TheGame.Instance.numberDisplayer.Draw(spriteBatch);
                cursor.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawVisibleGameObjects(ref Vector2 internalDisplaySize, ref Vector2 displayPos, int playerId)
        {
            //draw every object on the map, except those that aren't in the viewport
            foreach (GameObject gameObject in TheGame.Instance.world.gameObjectList)
            {
                if (gameObject.state == GameObjectState.Created)
                {
                    gameObject.Draw(TheGame.Instance.worldDrawer);
                    if (isInDev)
                    {
                        Rectangle worldRectangle = gameObject.getWorldRectangle(); // change it just a bit in case the sprite and world collisions are the same sizes
                        worldRectangle.X -= 1;
                        worldRectangle.Y -= 1;
                        worldRectangle.Width += 2;
                        worldRectangle.Height += 2;
                        RectangleSprite.DrawRectangle(TheGame.Instance.worldDrawer, gameObject.getCollisionRectangle(), Color.Yellow, 1);
                        RectangleSprite.DrawRectangle(TheGame.Instance.worldDrawer, worldRectangle, Color.Red, 1);
                    }
                }
            }
        }

        private void DrawBackgroundImage(ref Vector2 internalDisplaySize, ref Vector2 displayPos, int playerId)
        {
            Rectangle worldRectangle = new Rectangle(0, 0, 500, 500);
            RectangleSprite.DrawRectangle(TheGame.Instance.worldDrawer, worldRectangle, Color.Yellow, 1);


            return;
            if (displaySize.X > world.mapSize.X &&
               displaySize.Y > world.mapSize.Y) // if the map is smaller than the displaySize
                spriteBatch.Draw(world.texture, new Rectangle((int)(float)displayPos.X, (int)(float)displayPos.Y, (int)(float)displaySize.X, (int)(float)displaySize.Y), Color.White);
            else
                spriteBatch.Draw(world.texture, new Rectangle((int)(float)displayPos.X, (int)(float)displayPos.Y, (int)(float)internalDisplaySize.X, (int)(float)internalDisplaySize.Y),
                                          new Rectangle((int)(float)mapPos[playerId].X, (int)(float)mapPos[playerId].Y, (int)(float)(internalDisplaySize.X), (int)(float)(internalDisplaySize.Y)),
                    Color.White);
        }
    }
}
