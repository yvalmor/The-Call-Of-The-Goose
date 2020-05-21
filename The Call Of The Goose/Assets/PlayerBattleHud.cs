﻿using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHud : MonoBehaviour
{
    public Text entityName;
    public Text entityLvl;
    public HealthBar hp;
    public HealthBar mana;
    public HealthBar endu;

    public void SetHUD(Player player)
    {
        entityName.text = player.name;
        entityLvl.text += $" {player.lvl}"; 
        hp.SetMaxHealth(player.health.maxHealth); 
        mana.SetMaxHealth(player._mana.maxMana); 
        endu.SetMaxHealth(player._endurance.maxEndurance);
    }
}
