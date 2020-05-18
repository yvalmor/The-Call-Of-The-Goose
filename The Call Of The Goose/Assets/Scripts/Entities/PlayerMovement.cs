using System;
using Photon.Pun;
using UnityEngine;

namespace Entities
{
    public class PlayerMovement : MonoBehaviourPun
    {
        public float moveSpeed = 5f;

        public Rigidbody2D rb;
        public Animator Animator;

        private Vector2 _movement;
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Left = Animator.StringToHash("Left");
        private static readonly int Right = Animator.StringToHash("Right");

        void Update()
        {
            if (photonView.IsMine) //ajouter script 'photon view' au joueur (observe player)
            {
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
            
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + _movement * Time.fixedDeltaTime);
        }
    }
}
