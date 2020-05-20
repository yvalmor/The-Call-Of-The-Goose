using Entities;
using Entities.PlayerScripts;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
    public class Consumable : Item
    {
        public int PvHeal = 0;
        public int ManaHeal = 0;
        public int EnduranceHeal = 0;
        
        public override void Use()
        {
            GameObject player = Inventory.instance.player;
            player.GetComponent<Health>().Heal(PvHeal);
            player.GetComponent<Mana>().RegenMana(ManaHeal);
            player.GetComponent<Endurance>().RegenEndurance(EnduranceHeal);

            string healed = "Gained:\n";
            if (PvHeal != 0) healed += $"PV: +{PvHeal}";
            if (ManaHeal != 0) healed += $"PV: +{ManaHeal}";
            if (EnduranceHeal != 0) healed += $"PV: +{EnduranceHeal}";
            
            dialogue.name = itemName;
            dialogue.sentences = new[] { healed };
            
            Inventory.instance.Remove(this);
            Destroy(this);
        }
    }
}