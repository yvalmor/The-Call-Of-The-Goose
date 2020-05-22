using DialogueSystem;
using UnityEngine;

namespace Item
{
    public class Item : ScriptableObject
    {
        public Dialogue dialogue;

        public int price;
        public string itemName;
        public Sprite icon;

        public virtual void Use()
        {
            
        }
    }
}