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
            if (player == null || player.Length == 0)
                GetPlayers();
            Vector3 closest;
            if (player.Length > 1)
            {
                float dist0 = player[0].transform.position.x - rb.position.x + player[0].transform.position.y -
                              rb.position.y;
                float dist1 = player[1].transform.position.x - rb.position.x + player[1].transform.position.y -
                              rb.position.y;
                if (dist1 > dist0)
                {
                    closest = player[0].transform.position;
                }
                else
                {
                    closest = player[1].transform.position;
                }
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
            {
                _movement.x = 0;
                _movement.y = 0;
            }
        }


        private void FixedUpdate()
        {
            Ennemy.transform.position = rb.position + Time.fixedDeltaTime * moveSpeed * _movement;
        }
    }
}

