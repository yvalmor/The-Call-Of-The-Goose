using UnityEngine;

namespace Item
{
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;

        public virtual void Use()
        {
            
        }
        
        
    }
}