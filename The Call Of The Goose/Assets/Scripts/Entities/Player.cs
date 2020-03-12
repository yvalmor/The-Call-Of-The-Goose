using System;

namespace Entities
{
    public class Player
    {
        protected int maxHp;
        protected int maxMana;
        protected int maxEndurance;
        protected int hp;
        protected int armor;
        protected int lvl = 1;
        protected int exp = 0;
        protected int mana;
        protected int endurance;
        protected int gold = 0;
        public string name;
        protected int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up
        //public Item[] consumablesInventory;
        //public List<Relic> relicInventory;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public Player(string name)
        {
            this.name = name;
            //consumablesInventory = new Item[10];
            //relicInventory = new List<Relic>();
        }

        public void TakeDamage(int damage)
        {
            if (hp - damage <= 0)
                throw new NotImplementedException();
            hp -= damage;
        }

        public void Heal(int heal)
        {
            if (hp + heal > maxHp)
                hp = maxHp;
            hp += heal;
        }

        public void RegenMana(int regen)
        {
            if (mana + regen > maxMana)
                mana = maxMana;
            mana += regen;
        }
        
        public void RegenEndurance(int regen)
        {
            if (endurance + regen > maxEndurance)
                endurance = maxEndurance;
            endurance += regen;
        }
        
        public bool GainExp(int gain)
        {
            exp += gain;
            if (lvl < 10 && exp >= expTreshold[lvl - 1])
            {
                exp -= expTreshold[lvl - 1];
                lvl++;
                return true;
            }

            return false;
        }


    }
}
