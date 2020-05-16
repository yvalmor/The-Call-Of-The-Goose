using UnityEngine;

namespace Entities
{
    public class Relic : MonoBehaviour
    {
        public int hp;
        public int armor;
        public int mana;
        public int endurance;
        public int gold;
        public int attack;

        public Relic(int hp, int armor, int mana, int endurance, int gold, int attack)
        {
            this.hp = hp;
            this.armor = armor;
            this.mana = mana;
            this.endurance = endurance;
            this.gold = gold;
            this.attack = attack;
        }

        public int Hp => hp;
        public int Armor => armor;
        public int Mana => mana;
        public int Endurance => endurance;
        public int Gold => gold;
        public int Attack => attack;

        public void addToInventory(Relic relic, Player player)
        {
            player.relicInventory.Add(relic);
            if (hp != 0)
            {
                player.MaxHp += hp;
                player.Hp += hp;
            }

            if (armor != 0)
                player.Armor += armor;

            if (mana != 0)
            {
                player.MaxMana += mana;
                player.Mana += mana;
            }

            if (endurance != 0)
            {
                player.MaxEndurance += endurance;
                player.Endurance += endurance;
            }

            if (gold != 0)
                player.Gold += gold;

            if (attack != 0)
                player.Attack += attack;
        }
        
        Relic shield = new Relic(0, 10, 0,0,0,0);
        Relic sword = new Relic(0,0,0,0,0,10);
        Relic cloak = new Relic(0,0,10,0,0,0);
        Relic boots = new Relic(0,0,0,10,0,0);
        Relic purse = new Relic(0,0,0,0,10,0);
        Relic ring = new Relic(50,0,0,0,0,0);
    }
}