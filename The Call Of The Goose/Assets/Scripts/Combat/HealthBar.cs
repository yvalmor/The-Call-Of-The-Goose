using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxHealth(int maxHealth)
        {
            slider.maxValue = maxHealth;
        }
    
        public void SetHealth(int health)
        {
            slider.value = health;
        }
    }
}
