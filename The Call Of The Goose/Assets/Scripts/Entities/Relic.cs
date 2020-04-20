using UnityEngine;

namespace Entities
{
    public class Relic
    {
        private int hp;
        private int armor;
        private int mana;
        private int endurance;
        private int gold;
        private int attack;

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
    }
}