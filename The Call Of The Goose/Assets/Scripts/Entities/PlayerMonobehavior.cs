using System;
using UnityEngine;
using System.Collections.Generic;

namespace Entities
{
    public class PlayerMonobehavior : MonoBehaviour
    {
        protected int maxHp;
        protected int maxMana;
        protected int maxEndurance;
        protected int hp = 100;
        protected int lvl = 1;
        protected int exp = 0;
        protected int mana;
        protected int endurance;
        protected int gold = 0;
        public string name;
        public HealthPoint HPPlayer;
        public HealthPoint ManaPlayer;
        public HealthPoint EndurancePlayer;
        public int Attaque;
        protected int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up
        public Consumables[] consumablesInventory;
        public List<Relic> relicInventory;

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int Mana
        {
            get => mana;
            set => mana = value;
        }

        public int Endurance
        {
            get => endurance;
            set => endurance = value;
        }

        public PlayerMonobehavior(string name)
        {
            this.name = name;
            consumablesInventory = new Consumables[5];
            relicInventory = new List<Relic>();
        }

        public void Start()
        {
            hp = maxHp;
            mana = maxMana;
            endurance = maxEndurance;
        }


        public void TakeDamage(int damage)
        {
            if (hp - damage <= 0)
                throw new NotImplementedException();
            hp -= damage;
            HPPlayer.SetHp(hp);
        }

        public void Heal(int heal)
        {
            if (hp + heal > maxHp)
                hp = maxHp;
            hp += heal;
            HPPlayer.SetHp(hp);
        }

        public void RegenMana(int regen)
        {
            if (mana + regen > maxMana)
                mana = maxMana;
            mana += regen;
            ManaPlayer.SetHp(mana);
        }
        
        public void RegenEndurance(int regen)
        {
            if (endurance + regen > maxEndurance)
                endurance = maxEndurance;
            endurance += regen;
            EndurancePlayer.SetHp(endurance);
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
