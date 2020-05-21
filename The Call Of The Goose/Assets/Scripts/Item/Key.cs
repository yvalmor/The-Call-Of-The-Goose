using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Key", menuName = "Items/Consumable/Key")]
    public class Key : Consumable
    {
        public GameObject boss;
        public string description;

        public override void Use()
        {
            dialogue.name = itemName;
            dialogue.sentences = new[] { description };
            
            Inventory.instance.Remove(this);
            Destroy(this);
            boss.SetActive(true);
        }
    }
}