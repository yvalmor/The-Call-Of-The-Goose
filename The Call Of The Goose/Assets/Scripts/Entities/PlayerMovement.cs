using UnityEngine;

namespace Entities
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;

        private Vector2 _movement;
        void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
        }
    }
}
