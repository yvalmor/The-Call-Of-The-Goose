using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public Health _health;
        public Mana _mana;
        public Endurance _endurance;

        private int armor;
        private int lvl = 1;
        private int exp;
        private int gold;
        private int attack;
        public string name;
        public HealthPoint HPPlayer;
        public HealthPoint ManaPlayer;
        public HealthPoint EndurancePlayer;
        public int floor;
        protected int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up
        public Consumables[] consumablesInventory;
        public List<Relic> relicInventory;

        public int Hp => _health.health;
        public int Mana => _mana.mana;
        public int Endurance => _endurance.endurance;

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
            consumablesInventory = new Consumables[6];
            relicInventory = new List<Relic>();
        }

        public void TakeDamage(int value) => _health.TakeDamage(value);
        public void Heal(int value) => _health.Heal(value);
        public void GainMaxHp(int value) => _health.GainMaxHp(value);
        
        public void RegenMana(int value) => _mana.RegenMana(value);
        public void UseMana(int value) => _mana.UseMana(value);
        public void GainMaxMana(int value) => _mana.GainMaxMana(value);
        
        public void RegenEndurance(int value) => _endurance.RegenEndurance(value);
        public void UseEndurance(int value) => _endurance.UseEndurance(value);
        public void GainMaxEndurance(int value) => _endurance.GainMaxEndurance(value);
        
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
