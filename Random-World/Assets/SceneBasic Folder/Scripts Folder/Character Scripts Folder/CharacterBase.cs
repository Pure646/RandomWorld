using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace RandomWorld
{
    public class CharacterBase : MonoBehaviour
    {
        private Animator animator;

        [Header("¡å---Character Move---¡å")]
        private Vector3 movement;
        public float moveSpeed = 3f;

        private float SpinRotationY = 0f;
        private float SpinRotationX = 0f;

        private UnityEngine.CharacterController unityCharacterController;
        private bool isGrounded;
        
        public float GroundedRadius = 0.28f;
        public float GroundedOffset = -0.14f;
        public LayerMask GroundLayers;

        public float gravity;
        public float jumpHeight;
        public float fallTimeout;
        public float jumpTimeout;
        public float terminalVelocity;

        private float verticalVelocity;
        private float fallTimeoutDelta;
        private float jumpTimeoutDelta;
        private bool isJump;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            bool Grounded = unityCharacterController.isGrounded;
            JumpAndGravity();
            GroundedCheck();

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.z);
            animator.SetFloat("Magnitude", movement.magnitude);
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            //// update animator if using character
            //if (_hasAnimator)
            //{
            //    _animator.SetBool(_animIDGrounded, Grounded);
            //}
        }

        private void JumpAndGravity()
        {
            if (isGrounded)
            {
                // reset the fall timeout timer
                fallTimeoutDelta = fallTimeout;

                // update animator if using character
                //if (_hasAnimator)
                //{
                //    _animator.SetBool(_animIDJump, false);
                //    _animator.SetBool(_animIDFreeFall, false);
                //}

                // stop our velocity dropping infinitely when grounded
                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                // Jump
                if (isJump && jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    // update animator if using character
                    //if (_hasAnimator)
                    //{
                    //    _animator.SetBool(_animIDJump, true);
                    //}
                }

                // jump timeout
                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                jumpTimeoutDelta = jumpTimeout;

                // fall timeout
                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    //if (_hasAnimator)
                    //{
                    //    _animator.SetBool(_animIDFreeFall, true);
                    //}
                }

                // if we are not grounded, do not jump
                //_input.jump = false;
                isJump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }

        }

        public void Move(Vector2 input)
        {
            movement = new Vector3(input.x, 0, input.y);
            Vector3 moveVec = movement * Time.deltaTime * moveSpeed;
            moveVec.y = verticalVelocity;

            //transform.Translate(movement * Time.deltaTime * moveSpeed, Space.Self);

            unityCharacterController.Move(moveVec);
        }

        public void Rotate(float inputX)
        {
            SpinRotationY += inputX;
            transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
        }

        //private float transformY = 0f;
        //public float jumpPower = 1f;
        //public float GroundedOffset = -0.14f;

        //private void FreeFall()
        //{
        //    bool Air = transformY > 0f ? true : false;               
        //    if (transformY > 0f || !unityCharacterController.isGrounded)
        //    {
        //        animator.SetBool("OnAir", Air);
        //        transformY += Physics.gravity.y * Time.deltaTime;
        //    }
        //}

        public void Jump()
        {
            if (isGrounded)
            {
                isJump = true;
                animator.SetBool("Grounded", isGrounded);
            }
        }

        //public void CheckGround()
        //{
        //    Vector3 CheckSphereGrounded = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        //    GroundedOffset = Physics.CheckSphere(CheckSphereGrounded, Grounded)
        //}
    }

}
