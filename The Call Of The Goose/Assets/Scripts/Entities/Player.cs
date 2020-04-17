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
        public Consumables[] consumablesInventory;
        //public List<Relic> relicInventory;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public Player(string name)
        {
            this.name = name;
            consumablesInventory = new Consumables[5];
            //relicInventory = new List<Relic>();
        }

        public void TakeDamage(int damage)
        {
            if (hp - damage <= 0)
                GameOver();
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

        public void UseConsumable(Consumables consumable)
        {
            bool faux = false;
            int i = 0;
            while (!faux && i < consumablesInventory.Length)
            {
                if (consumablesInventory[i] == consumable)
                    faux = true;
                else
                    i++;
            }

            if (faux)
            {
                if (consumable.HpRegen != 0)
                    Heal(consumable.HpRegen);
                
                if (consumable.ManaRegen != 0)
                    RegenMana(consumable.ManaRegen);
                
                if (consumable.EnduRegen != 0)
                    RegenEndurance(consumable.EnduRegen);
                
                consumablesInventory[i] = null;

                bool vrai = true;
                while(vrai && i < consumablesInventory.Length -1)
                {
                    if (consumablesInventory[i] == null && consumablesInventory[i+1] != null)
                    {
                        consumablesInventory[i] = consumablesInventory[i + 1];
                        consumablesInventory[i + 1] = null;
                    }
                    else if(consumablesInventory[i] == null && consumablesInventory[i+1] == null)
                    {
                        vrai = false;
                    }

                    i++;
                }
            }
            else
                throw new NotImplementedException();
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

        public void GameOver()
        {
            throw new NotImplementedException();
        }


    }
}
