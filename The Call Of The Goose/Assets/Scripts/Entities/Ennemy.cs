using System;
using Item;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Player = Entities.PlayerScripts.Player;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Ennemy : PlayerScripts.Entity
    {
        public Animator Animator;

        public Ennemies[] Ennemies_list;
        public Ennemies Ennemies;
        
        public int gold_loot;
        public Relique[] loot;
        public Consumable[] c_loot;
        public int attack;

        public int Attaque => attack;

        public int Hp => health.health;

        public void Start()
        {
            Ennemies = Ennemies_list[Random.Range(0, Ennemies_list.Length)];
            
            gold_loot = Ennemies.gold_loot;
            loot = Ennemies.loot_r;
            c_loot = Ennemies.loot_c;
            attack = Ennemies.attack;
            health.maxHealth = Ennemies.hp;
            name = Ennemies.name;

            Animator.runtimeAnimatorController = Ennemies.animator;
            
            health.health = health.maxHealth;
        }

        private void Update()
        {
            if (health.health <= 0)
                Destroy(gameObject);
        }
    }
}