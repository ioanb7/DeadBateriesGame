using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstXNAGame
{
    public class GameObjectBonusHp : GameObjectBonus
    {
        int bonusHP;
        public GameObjectBonusHp(Vector2 pos, int bonusHP) : base(pos, "Images/bonusHP")
        {
            this.bonusHP = bonusHP;
            if (bonusHP < 0)
                throw new Exception("bonus hp > 0");
        }

        /*public override GameObjectBonusList Consume(Player player)
        {
            player.ChangeHP(+30);
        }*/
        public override void Consume(Player player)
        {
            player.getBonusList();
            player.ChangeHP(Math.Abs(bonusHP)); // TODO: respect maximum if not gm
        }
    }
}
