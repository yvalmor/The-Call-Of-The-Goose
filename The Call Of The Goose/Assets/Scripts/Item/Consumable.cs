using Entities;
using Entities.PlayerScripts;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
    public class Consumable : Item
    {
        public int PvHeal = 0;
        public int manaHeal = 0;
        public int EnduranceHeal = 0;
        
        public override void Use()
        {
            GameObject player = Inventory.instance.player;
            player.GetComponent<Health>().Heal(PvHeal);
            player.GetComponent<Mana>().RegenMana(manaHeal);
            player.GetComponent<Endurance>().RegenEndurance(EnduranceHeal);
            Inventory.instance.Remove(this);
        }
    }
}