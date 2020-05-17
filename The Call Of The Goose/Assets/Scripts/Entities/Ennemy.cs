using Item;
using UnityEngine;

namespace Entities
{
    public class Ennemy : MonoBehaviour
    {
	    
        public int maxHp;
        public int hp;
        private int gold_loot;
        private Relic loot;
        private Consumable c_loot;
        private new string name;
        private int armor;
        private int attack;
        public HealthPoint HPE;
        
        public int Attaque => attack;

        public int Hp
        {
            get => hp;
            set => hp = value;
        }

        public void Start()
        {
            hp = maxHp;
            HPE.Set(maxHp);
        }
    

        public void TakeDamage(int damage)
        {
            Hp -= damage + armor;
            HPE.Set(Hp);
        }
        
    }
}