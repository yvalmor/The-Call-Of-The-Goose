using System;
using Item;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Player = Entities.PlayerScripts.Player;

namespace Entities
{
    public class Ennemy : MonoBehaviour
    {
        public GameObject ennemy;
        public int maxHp;
        public int hp;
        private int gold_loot;
        private Relique loot;
        private Consumable c_loot;
        private new string name;
        private int armor;
        private int attack;
        public HealthPoint HPE;

        private GameObject[] players;
        private Rigidbody2D rb2d;
        
        public int Attaque => attack;

        public int Hp
        {
            get => hp;
            set => hp = value;
        }

        public void Start()
        {
            hp = maxHp;
            HPE.Set(maxHp);
            players = !PhotonNetwork.IsConnected ? GameObject.FindGameObjectsWithTag("Player") : null;
            rb2d = ennemy.GetComponent<Rigidbody2D>();
        }
    

        public void TakeDamage(int damage)
        {
            Hp -= damage + armor;
            HPE.Set(Hp);
        }

        private void Update()
        {
            if (players == null)
                players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                Collider2D colliderPlayer = player.GetComponent<Collider2D>();

                if (rb2d.IsTouching(colliderPlayer))
                    player.GetComponent<Player>().LaunchFight(this);
            }
        }
    }
}