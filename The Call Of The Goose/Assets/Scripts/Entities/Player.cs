using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        protected int maxHp;
        protected int maxMana;
        protected int maxEndurance;
        protected int hp;
        protected int armor;
        protected int lvl = 1;
        protected int exp;
        protected int mana;
        protected int endurance;
        protected int gold;
        protected int attack;
        public string name;
        public HealthPoint HPPlayer;
        public HealthPoint ManaPlayer;
        public HealthPoint EndurancePlayer;
        public int floor;
        protected int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up
        public Consumables[] consumablesInventory;
        public List<Relic> relicInventory;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        
        public int MaxHp
        {
            get { return maxHp; }
            set { MaxHp = value; }
        }
        
        public int MaxMana
        {
            get { return maxMana; }
            set { maxMana = value; }
        }

        public int Mana
        {
            get { return mana; }
            set { mana = value; }
        }

        public int MaxEndurance
        {
            get { return maxEndurance; }
            set { maxEndurance = value; }
        }

        public int Endurance
        {
            get { return endurance; }
            set { endurance = value; }
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public int Attack
        {
            get => attack;
            set => attack = value;
        }


        public Player(string name)
        {
            this.name = name;
            consumablesInventory = new Consumables[5];
            relicInventory = new List<Relic>();
        }

        public void TakeDamage(int damage)
        {
            if (hp - damage <= 0)
                GameOver();
            hp -= damage;
            HPPlayer.Set(hp);
        }

        public void Heal(int heal)
        {
            if (hp + heal > maxHp)
                hp = maxHp;
            hp += heal;
            HPPlayer.Set(hp);
        }

        public void RegenMana(int regen)
        {
            if (mana + regen > maxMana)
                mana = maxMana;
            mana += regen;
            ManaPlayer.Set(mana);
        }
        
        public void RegenEndurance(int regen)
        {
            if (endurance + regen > maxEndurance)
                endurance = maxEndurance;
            endurance += regen;
            EndurancePlayer.Set(endurance);
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
        
        public void Start()
        {
            hp = maxHp;
            mana = maxMana;
            endurance = maxEndurance;
        }


        
        public void GameOver()
        {
            throw new NotImplementedException();
        }
    }
}
