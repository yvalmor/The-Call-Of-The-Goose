namespace Assets.Scripts
{
    public class Test_player : Player
    {
        public Test_player(string name) : base(name)
        {
            maxHp = 20;
            maxEndurance = 50;
            maxMana = 50;
            hp = maxHp;
            endurance = maxEndurance;
            mana = maxMana;
        }
    }
}