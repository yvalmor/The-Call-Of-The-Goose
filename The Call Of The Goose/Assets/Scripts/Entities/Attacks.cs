namespace Entities
{
    public class Attacks
    {
        private int manaCost;
        private int enduCost;
        private int damage;


        public int ManaCost => manaCost;

        public int EnduCost => enduCost;

        public int Damage => damage;

        public Attacks(int manaCost, int enduCost, int damage)
        {
            this.manaCost = manaCost;
            this.enduCost = enduCost;
            this.damage = damage;
        }
        
    }
}