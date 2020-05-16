using Entities;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
    public class Consumable : Item
    {
        public int heal = 0;
        
        public override void Use()
        {
            GameObject player = Inventory.instance.player;
            
        }
    }
}