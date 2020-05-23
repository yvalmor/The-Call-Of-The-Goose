using System;
using Entities.PlayerScripts;
using Photon.Pun;
using UnityEngine;

namespace Entities
{
    public class EnemyMovement : MonoBehaviour
    {
        public GameObject[] players;
        public GameObject Ennemy;

        private Vector2 _movement;

        public float moveSpeed = 4f;
        public float detectionRange = 200;

        public Rigidbody2D rb;

        private void Start()
        {
            Ennemy = gameObject;
        }

        private void GetPlayers()
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            GetPlayers();

            if (players.Length == 0)
            {
                _movement = Vector2.zero;
                return;
            }

            Vector3 closest;
            
            if (players.Length > 1)
            {
                float dist0 = players[0].transform.position.x - rb.position.x + players[0].transform.position.y -
                              rb.position.y,
                    dist1 = players[1].transform.position.x - rb.position.x + players[1].transform.position.y -
                                                     rb.position.y;
                
                closest = dist1 > dist0 ? players[0].transform.position : players[1].transform.position;
            }
            else
            {
                closest = players[0].transform.position;
            }

            if (Math.Abs(closest.x - rb.position.x) + Math.Abs(closest.y - rb.position.y) < detectionRange)
            {
                _movement.x = closest.x - rb.position.x;
                _movement.y = closest.y - rb.position.y;
            }
            else
                _movement = Vector2.zero;
            
        }

        private void FixedUpdate()
        {
            rb.transform.position = rb.position + Time.fixedDeltaTime * moveSpeed * _movement;
            Ennemy.transform.position = rb.transform.position;
            CheckFight();
        }

        private void CheckFight()
        {
            if(players.Length == 0)
                return;

            foreach (GameObject player in players)
            {
                if (!rb.IsTouching(player.GetComponent<Collider2D>())) continue;
                player.GetComponent<CombatEncounter>().BeginFight(Ennemy.GetComponent<Ennemy>());
                return;
            }
        }
    }
}

