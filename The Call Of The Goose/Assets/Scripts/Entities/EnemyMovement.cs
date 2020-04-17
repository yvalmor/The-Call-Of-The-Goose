using System;
using UnityEngine;

namespace Entities
{
    public class EnemyMovement : MonoBehaviour
    {

        private GameObject[] player;

        private Vector2 _movement;

        public float moveSpeed = 4f;
        public float detectionRange = 200;

        public Rigidbody2D rb;


        private void Awake()
        {
            player = GameObject.FindGameObjectsWithTag("Player");
        }

        void Update()
        {
            Vector3 closest;
            if (player.Length > 1)
            {
                float dist0 = player[0].transform.position.x - rb.position.x + player[0].transform.position.y -
                              rb.position.y;
                float dist1 = player[1].transform.position.x - rb.position.x + player[1].transform.position.y -
                              rb.position.y;
                closest = dist1 > dist0 ? player[0].transform.position : player[1].transform.position;
            }
            else
                closest = player[0].transform.position;

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
            rb.MovePosition(rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
        }
    }
}

