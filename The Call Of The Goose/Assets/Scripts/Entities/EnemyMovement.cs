using System;
using Photon.Pun;
using UnityEngine;

namespace Entities
{
    public class EnemyMovement : MonoBehaviour
    {
        public GameObject[] player;
        public GameObject Ennemy;

        private Vector2 _movement;

        public float moveSpeed = 4f;
        public float detectionRange = 200;

        public Rigidbody2D rb;

        public void GetPlayers()
        {
            player = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            GetPlayers();

            if (player.Length == 0)
            {
                _movement = Vector2.zero;
                return;
            }

            Vector3 closest;
            
            if (player.Length > 1)
            {
                float dist0 = player[0].transform.position.x - rb.position.x + player[0].transform.position.y -
                              rb.position.y,
                    dist1 = player[1].transform.position.x - rb.position.x + player[1].transform.position.y -
                                                     rb.position.y;
                
                closest = dist1 > dist0 ? player[0].transform.position : player[1].transform.position;
            }
            else
            {
                closest = player[0].transform.position;
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
        }
    }
}

