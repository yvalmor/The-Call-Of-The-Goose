using Item;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "Ennemy", menuName = "Ennemy", order = 0)]
    public class Ennemies : ScriptableObject
    {
        public bool Boss;
        
        public int hp;
        public int armor;
        public int attack;

        public int gold_loot;
        public Consumable[] loot_c;
        public Relique[] loot_r;
        
        public AnimatorOverrideController animator;
        
    }
}