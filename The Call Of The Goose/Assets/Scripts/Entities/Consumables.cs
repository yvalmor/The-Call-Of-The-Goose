using System;
using UnityEngine;

namespace Entities
{
    public class Consumables
    {
        private int manaRegen;
        private int hpRegen;
        private int enduRegen;

        public Consumables(int manaRegen, int hpRegen, int enduRegen)
        {
            this.manaRegen = manaRegen;
            this.hpRegen = hpRegen;
            this.enduRegen = enduRegen;
        }

        public int ManaRegen => manaRegen;

        public int HpRegen => hpRegen;

        public int EnduRegen => enduRegen;

        public void addToInventory(Consumables consumables, PlayerScripts.Player player)
        {
            int i = 0;
            bool vrai = true;
            while (i < player.consumablesInventory.Length && vrai)
            {
                if (player.consumablesInventory[i] == null)
                {
                    player.consumablesInventory[i] = consumables;
                    vrai = false;
                }
            }
            if (vrai)
            {
                throw new NotImplementedException();
            }
        }
    }
    
    
     
}