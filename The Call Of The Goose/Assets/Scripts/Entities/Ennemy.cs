using System;
using Item;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Player = Entities.PlayerScripts.Player;

namespace Entities
{
    public class Ennemy : PlayerScripts.Entity
    {
        public GameObject ennemy;
        
        public int gold_loot;
        public Relique[] loot;
        public Consumable[] c_loot;
        public int attack;

        private GameObject[] players;
        private Rigidbody2D rb2d;
        
        public int Attaque => attack;

        public int Hp => health.health;

        public void Start()
        {
            health.health = health.maxHealth;
            players = !PhotonNetwork.IsConnected ? GameObject.FindGameObjectsWithTag("Player") : null;
            rb2d = ennemy.GetComponent<Rigidbody2D>();
        }
    
        
        private void Update()
        {
            if (players == null)
                players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                Collider2D colliderPlayer = player.GetComponent<Collider2D>();

                if (rb2d.IsTouching(colliderPlayer))
                    player.GetComponent<Player>().BeginFight(this);
            }
        }
    }
}