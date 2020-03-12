using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class HealthPoint : MonoBehaviour
    {
        public Slider slider;
    
        public void MaxHealth(int hp)
        {
            slider.maxValue = hp;
            slider.value = hp;
        }
        
        public void SetHp(int hp)
        {
            slider.value = hp;
        }
    }
}

