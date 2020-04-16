using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Entities
{
    public class EnnemyMovement
    {
        private GameObject ennemy;
        
        private GameObject[] player;
        
        private Vector3 _movement;
        
        public float moveSpeed = 4f;
        
        public Rigidbody2D rb;


        private void Awake()
        {
            player = GameObject.FindGameObjectsWithTag("player");
        }


        void Update()
        {
            Vector3 closest;
            if (player[1] != null) 
            {
                float dist0 = player[0].transform.position.x - rb.position.x + player[0].transform.position.y - 
                              rb.position.y;
                float dist1 = player[1].transform.position.x - rb.position.x + player[1].transform.position.y - 
                              rb.position.y;
                if (dist1 > dist0)
                    closest = player[0].transform.position;
                else
                    closest = player[1].transform.position;
            }
            else
                closest = player[0].transform.position;

            if (closest.x - rb.position.x + closest.y - rb.position.y < 100)
            {
                _movement.x = closest.x - rb.position.x;
                _movement.y = closest.y - rb.position.y;
            }
        }
        
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
        }
        
    }
}