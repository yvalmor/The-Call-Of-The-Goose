using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class EnemyBattleHud : MonoBehaviour
    {
    
        public Text entityName;
        public Text entityLvl;
        public HealthBar hp;


        public void InitHUD(Ennemy ennemy)
        {
            entityName.text = ennemy.name;
            entityLvl.text = $"Level {ennemy.lvl}";
            hp.SetMaxHealth(ennemy.health.maxHealth);
        }
    
        public void SetHUD(Ennemy ennemy)
        {
            hp.SetHealth(ennemy.health.health);
        }
    
    }
}
