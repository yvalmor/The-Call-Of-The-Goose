namespace Entities
{
    public class Ennemy
    {
	    
        private int maxHp;
        private int hp;
        private int gold_loot;
        private Relic loot;
        private Consumables c_loot;
        private string name;
        private int armor;
        protected int attack;
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