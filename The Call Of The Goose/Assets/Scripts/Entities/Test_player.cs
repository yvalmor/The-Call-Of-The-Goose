namespace Entities
{
    public class TestPlayer : Player
    {
        public TestPlayer(string name) : base(name)
        {
            maxHp = 20;
            maxEndurance = 50;
            maxMana = 50;
            hp = maxHp;
            endurance = maxEndurance;
            mana = maxMana;
            //armor = 10;
        }
    }
}