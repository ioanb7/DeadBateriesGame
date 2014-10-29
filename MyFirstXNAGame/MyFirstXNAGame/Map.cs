using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public enum BlockType
    {
        Dirt,
        Free,
        Peas,
        //Vaccuum,
        PlayerSpawner,
        Portal,
        Vaccuum
    }
    public class Block
    {
        public int Id { get; set; }
        public BlockType Type { get; set; }
    }
    /*
     * 
     * Example of calls:
                Map map = new Map();
                map.setWidth(10);
                map.setHeight(10);
                map.setBlockSize(20);
                map.create();
                map.makeEveryBlockDirty();
                map.setPlayerStartPos(new Point(2, 2));
                map.setPlayerPeasAtTheBeggining(50);
                map.setBonusPeasAt(new Point(10, 10));
                map.setEndPortalAt(new Point(20, 20));
     * */
    public class Map
    {
        private int _width, _height;
        //public int blockSize;
        private Point portalAt;
        private List<Point> bonusPeaList;
        private List<Block> map;
        public Point playerPos;
        public int playerPeasAtBeginning;

        private int blockSizeWidth, blockSizeHeight;

        public Vector2 getPlayerPos()
        {
            return new Vector2((playerPos.X -1) * blockSizeWidth, (playerPos.Y -1 ) * blockSizeHeight);
        }

        public Block getBlock(int i, int j)
        {
            return map[i * _width + j];
        }
        public Map()
        {
            bonusPeaList = new List<Point>();
        }

        public void setWidth(int inWidth)
        {
            _width = inWidth;
        }

        public void setHeight(int inHeight)
        {
            _height = inHeight;
        }/*
        public void setBlockSize(int inBlockSize)
        {
            blockSize = inBlockSize;
        }*/
        public void setBlockSizeWidth(int inBlockSize)
        {
            blockSizeWidth = inBlockSize;
        }
        public void setBlockSizeHeight(int inBlockSize)
        {
            blockSizeHeight = inBlockSize;
        }
        
        public void create()
        {
            map = new List<Block>();
            for(int i = 0; i < _width * _height; i++)
            {
                map.Add(new Block { Type = BlockType.Vaccuum, Id = i });
            }
        }

        public void makeEveryBlockDirty()
        {
            for (int i = 0; i < _width * _height; i++)
            {
                map[i].Type = BlockType.Dirt;
            }
        }
        public void setPlayerStartPos(Point point)
        {
            playerPos = point;
            setBlockType(point, BlockType.PlayerSpawner);
            
        }
        public void setPlayerPeasAtTheBeggining(int nr)
        {
            playerPeasAtBeginning = nr;
        }
        

        public void setBonusPeasAt(Point point)
        {
            bonusPeaList.Add(point);
            setBlockType(point, BlockType.Peas);
        }
        
        public void setEndPortalAt(Point point)
        {
            bonusPeaList.Add(point);
            setBlockType(point, BlockType.Portal);
        }

        private void setBlockType(Point point, BlockType type)
        {
            //map[(point.X - 1) * (point.Y - 1)].Type = type;
            map[(point.X-1) * _width +  point.Y - 1].Type = type;
        }

        private Point getCoordinatesForBlock(int blockId)
        {
            if (blockId == 0) return new Point(0, 0);
            int x = blockId / _width;
            int y = blockId % _width;

            return new Point(x, y);
        }

        public void AddMapToGameObjectList(GameObjectList gameObjectList)
        {
            int i = 0;
            foreach(Block block in map)
            {
                Point point = getCoordinatesForBlock(i);
                GameObject gameObject = null;

                switch(block.Type)
                {
                    case BlockType.Dirt :
                        gameObject = GameObjectDirt.get(i, point, blockSizeWidth, blockSizeHeight);
                        break;
                    case BlockType.Free:
                        gameObject = null;
                        break;
                    case BlockType.Peas:
                        gameObject = GameObjectPeas.get(i, point, 3, blockSizeWidth, blockSizeHeight); // TODO: change 3
                        break;
                    case BlockType.PlayerSpawner:
                        gameObject = GameObjectSpawner.get(i, point, blockSizeWidth, blockSizeHeight);
                        break;
                    case BlockType.Portal:
                        gameObject = GameObjectPortal.get(i, point, blockSizeWidth, blockSizeHeight);
                        break;
                }

                gameObjectList.Add(gameObject);

                i++;
            }
        }
        private bool isTraversableBlock(Block block)
        {
            if (block.Type == BlockType.Dirt)
            {
                return false;
            }
            return true;
        }

        public void deleteBlock(Direction direction, Rectangle rect)
        {
            int x = direction == Direction.Left ? -1 : direction == Direction.Right ? 1 : 0;
            int y = direction == Direction.Up ? -1 : direction == Direction.Down ? 1 : 0;

            Block block = getBlock(rect.X / blockSizeWidth + x, rect.Y / blockSizeHeight + y);

            block.Type = BlockType.Free;

            foreach(GameObjectBlock gameObjectBlock in TheGame.Instance.world.gameObjectList.getGameObjects())
            {
                if (gameObjectBlock.id == block.Id)
                {
                    //TheGame.Instance.world.gameObjectList.Remove((GameObject)gameObjectBlock);
                    //TheGame.Instance.world.gameObjectList.Ad
                    gameObjectBlock.shouldBeRemoved = true;

                }
            }
        }

        public List<Direction> rectangleCollisions(Rectangle rect)
        {
            List<Direction> directions = new List<Direction>();

            if (isTraversableBlock(getBlock((rect.X + rect.Width) / blockSizeWidth, rect.Y / blockSizeHeight)))
            {
                directions.Add(Direction.Right);
            }

            if (isTraversableBlock(getBlock(rect.X / blockSizeWidth, rect.Y / blockSizeHeight)))
            {
                directions.Add(Direction.Left);
            }

            if (isTraversableBlock(getBlock(rect.X / blockSizeWidth, (rect.Y + rect.Height) / blockSizeHeight)))
            {
                directions.Add(Direction.Down);
            }

            if (isTraversableBlock(getBlock(rect.X / blockSizeWidth, rect.Y / blockSizeHeight)))
            {
                directions.Add(Direction.Up);
            }

            return directions;
        }

        public bool isRectangleFree(Rectangle rect)
        {
            return rectangleCollisions(rect).Count != 0;
        }


        /*
        public static Texture2D textureDirt, textureFree, textureVaccum, texturePeas, texturePlayerSpawner, texturePortal;
        public static List<GameObject> gameObject { get; set; }
        public static void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            textureDirt = graphicsContentLoader.Get("Images/dirt");
            textureFree = graphicsContentLoader.Get("Images/free");
            textureVaccum = graphicsContentLoader.Get("Images/vaccuum");
            texturePeas = graphicsContentLoader.Get("Images/peas");
            texturePlayerSpawner = graphicsContentLoader.Get("Images/playerSpawner");
            texturePortal = graphicsContentLoader.Get("Images/tele");
        }

        public static void DrawFor(int x, int y, BlockType type, SpriteBatch spriteBatch)
        {
            Texture2D texture = textureDirt;
            if (type == BlockType.Dirt)
                texture = textureDirt;
            if (type == BlockType.Free)
                texture = textureFree;
            if (type == BlockType.Peas)
                texture = texturePeas;
            if (type == BlockType.PlayerSpawner)
                texture = texturePlayerSpawner;
            if (type == BlockType.Portal)
                texture = texturePortal;
            if (type == BlockType.Vaccuum)
                texture = textureVaccum;

            spriteBatch.Draw(texture, new Rectangle(x, y, 20, 20), Color.Brown);
        }*/

    }
}
