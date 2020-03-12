using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class Ennemis : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public HealthPoint HPE;
    protected int Attaque = 10;

    public int Attaque1 => Attaque;

    public void Start()
    {
        HP = MaxHP;
        HPE.SetHp(MaxHP);
    }
    

    public void TakeDamage(int damage)
    {
        HP -= damage;
        HPE.SetHp(HP);
    }
}
