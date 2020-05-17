using System;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;
        public Animator Animator;

        public bool Activated = true;

        private Vector2 _movement;
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Left = Animator.StringToHash("Left");
        private static readonly int Right = Animator.StringToHash("Right");

        void Update()
        {
            if (!Activated)
            {
                _movement = Vector2.zero;
                return;
            }
            
            _movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
            _movement.y = Input.GetAxisRaw("Vertical") * moveSpeed;
            
            if (Math.Abs(_movement.x) > 0f || Math.Abs(_movement.y) > 0f)
                Animator.SetBool(Running, true);
            else Animator.SetBool(Running, false);
            
            if (_movement.x > 0)
            {
                Animator.SetBool(Right, true);
                Animator.SetBool(Left, false);
            }
            else if (_movement.x < 0)
            {
                Animator.SetBool(Left, true);
                Animator.SetBool(Right, false);
            }
            else
            {
                Animator.SetBool(Right, false);
                Animator.SetBool(Left, false);
            }
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + _movement * Time.fixedDeltaTime);
        }
    }
}
