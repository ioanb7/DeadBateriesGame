using System;

namespace MyFirstXNAGame
{
    public class TheGame
    {
        public bool isHavingAUserMap { get; set; }
        public MapMaker.DataObject data;

        private int _userScore;
        public int userScore
        {
            get
            {
                return _userScore;
            }
            set
            {
                _userScore = value;
                if(value != 0)
                    numberDisplayer.NO = value;
            }
        }
        public World world { get; set; }
        public WorldDrawer worldDrawer { get; set; }
        public NumberDisplayer numberDisplayer { get; set; }
        public AnimationPopper animationPopper { get; set; }
        public bool isMultiplayer { get; set; }
        public SoundManager SoundManager { get; set; }
        public TheGame()
        {
            userScore = 0;
            //isMultiplayer = false;
            isMultiplayer = false;
        }
        
        public void changeLevel(Teleporter teleporter)
        {
            throw new Exception("level finished");
        }

        public void showDeadAnimation()
        {
            // TODO: do this
            //Texture2D texture = graphicsContentLoader
            //spriteBatch.Draw(texture, new Rectangle(0, 0, (int)(float)displaySize.X, (int)(float)displaySize.Y), Color.White);
        }










































        #region stuff

        static readonly TheGame _instance = new TheGame();

        public static TheGame Instance
        {
            get
            {
                return _instance;
            }
        }
#endregion
    }
}
