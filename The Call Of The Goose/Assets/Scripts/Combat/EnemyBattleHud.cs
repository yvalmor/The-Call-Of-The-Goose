using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Entities.PlayerScripts;
using UnityEngine.UI;

public class EnemyBattleHud : MonoBehaviour
{
    
    public Text entityName;
    public Text entityLvl;
    public HealthBar hp;


    public void InitHUD(Ennemy ennemy)
    {
        entityName.text = ennemy.name;
        entityLvl.text += $" {ennemy.lvl}";
        hp.SetMaxHealth(ennemy.health.maxHealth);
    }
    
    public void SetHUD(Ennemy ennemy)
    {
        hp.SetHealth(ennemy.health.health);
    }
    
}
