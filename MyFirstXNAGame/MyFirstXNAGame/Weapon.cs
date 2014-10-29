using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class Weapon
    {
        //bullet texture
        Texture2D bulletTexture;
        GameObjectList gameObjectList;

        private decimal lastFiredBullet = 0;
        static private decimal bulletFireInterval = 10000;

        private Weapon(GameObjectList gameObjectList)
        {
            this.gameObjectList = gameObjectList;
        }
        public void LoadContent(GraphicsContentLoader graphicsContentLoader)
        {
            bulletTexture = graphicsContentLoader.Get("Images/bullet");
        }
        public void Update(GameTime gameTime)
        {
            lastFiredBullet += gameTime.TotalGameTime.Milliseconds;
        }

        public void Fire(Vector2 firePos, Direction playerDirection)
        {
            if (bulletTexture == null)
                throw new Exception("Load bullet texture");

            if (lastFiredBullet - bulletFireInterval >= 0)
            {
                lastFiredBullet %= bulletFireInterval;
                //fire a bullet
            }
            else
            {
                return;
            }
            float bulletSpeed = 20.0f * 10;

            Vector2 bulletVelocity = new Vector2(0, 0);
            if (playerDirection == Direction.Right)
                bulletVelocity.X += bulletSpeed;
            if (playerDirection == Direction.Down)
                bulletVelocity.Y += bulletSpeed;
            if (playerDirection == Direction.Left)
                bulletVelocity.X += -bulletSpeed;
            if (playerDirection == Direction.Up)
                bulletVelocity.Y += -bulletSpeed;

            gameObjectList.Add(new Bullet(firePos,
                bulletVelocity,
                GameObjectType.Player));

            SoundPlayer.gunShot();
        }

        public static Weapon getWeapon(GameObjectList gameObjectList)
        {
            return new Weapon(gameObjectList);
        }
    }
}
