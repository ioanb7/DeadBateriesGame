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
        Vaccuum,
        PlayerSpawner,
        Portal
    }
    public class Block
    {
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
        }


        public BlockType Type { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
    /*
     * 
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
        public int blockSize;
        private Point portalAt;
        private List<Point> bonusPeaList;
        private List<Block> map;
        public Point playerPos;
        public int playerPeasAtBeginning;

        public Vector2 getPlayerPos()
        {
            return new Vector2((playerPos.X - 1) * _width * blockSize, (playerPos.Y - 1) * blockSize);
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
        }
        public void setBlockSize(int inBlockSize)
        {
            blockSize = inBlockSize;
        }
        public void create()
        {
            map = new List<Block>();
            for(int i = 0; i < _width * _height; i++)
            {
                map.Add(new Block { Type = BlockType.Vaccuum });
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
    }
}
