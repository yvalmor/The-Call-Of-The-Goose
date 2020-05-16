using UnityEngine;

namespace Entities.PlayerScripts
{
    public class Health : MonoBehaviour
    {
        public int maxHealth, health;

        private void Start()
        {
            health = maxHealth;
        }

        public void Heal(int value)
        {
            health = health + value > maxHealth ? maxHealth : health + value;
        }

        public void TakeDamage(int value)
        {
            health = health - value < 0 ? 0 : health - value;
        }

        public void GainMaxHp(int value)
        {
            maxHealth += value;
            Heal(value);
        }
    }
}
