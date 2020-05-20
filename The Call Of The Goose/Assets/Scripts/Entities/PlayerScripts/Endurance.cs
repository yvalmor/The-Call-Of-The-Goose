using UnityEngine;

namespace Entities.PlayerScripts
{
    public class Endurance : MonoBehaviour
    {
        public int maxEndurance, endurance;

        private void Start()
        {
            endurance = maxEndurance;
        }

        public void RegenEndurance(int value)
        {
            endurance = endurance + value > maxEndurance ? maxEndurance : endurance + value;
        }

        public void UseEndurance(int value)
        {
            endurance = endurance - value < 0 ? 0 : endurance - value;
        }

        public void GainMaxEndurance(int value)
        {
            maxEndurance += value;
            RegenEndurance(value);
        }
    }
}