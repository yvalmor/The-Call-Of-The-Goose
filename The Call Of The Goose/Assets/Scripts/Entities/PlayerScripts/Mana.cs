using UnityEngine;

namespace Entities.PlayerScripts
{
    public class Mana : MonoBehaviour
    {
        public int maxMana, mana;

        private void Start()
        {
            mana = maxMana;
        }

        public void RegenMana(int value)
        {
            mana = mana + value > maxMana ? maxMana : mana + value;
        }

        public void UseMana(int value)
        {
            mana = mana - value < 0 ? 0 : mana - value;
        }
        
        public void GainMaxMana(int value)
        {
            maxMana += value;
            RegenMana(value);
        }
    }
}