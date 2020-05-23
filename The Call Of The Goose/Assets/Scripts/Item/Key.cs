using Entities.PlayerScripts;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Key", menuName = "Items/Consumable/Key")]
    public class Key : Consumable
    {
        public string description;

        public override void Use()
        {
            dialogue.name = itemName;
            dialogue.sentences = new[] { description };
            
            GameObject boss = GameObject.FindWithTag("playerDeactivated").GetComponent<Player>().boss;            
            
            Inventory.instance.Remove(this);
            boss.SetActive(true);
        }
    }
}