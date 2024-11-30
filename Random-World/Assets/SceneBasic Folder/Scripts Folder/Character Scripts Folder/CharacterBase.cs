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
        public float moveSpeed = 3f;
        private Vector3 movement;

        private float SpinRotationY = 0f;
        private float SpinRotationX = 0f;

        //private UnityEngine.CharacterController unityCharacterController;


        [Space(10)]
        [Header("¡å---Character Jump---¡å")]
        public float GroundedRadius = 0.28f;
        public GameObject FootIsGrounded;
        public LayerMask GroundLayers;

        private bool isGrounded;
        private bool isJump;

        private float VerticalVelocity = 0f;
        private float Gravity = -9.81f;
        public float JumpHeight = 3f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            //unityCharacterController = GetComponent<UnityEngine.CharacterController>();

        }

        private void Update()
        {
            JumpPower();
            GroundedCheck();
            transform.position = new Vector3(transform.position.x, VerticalVelocity, transform.position.z);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.z);
            animator.SetFloat("Magnitude", movement.magnitude);
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, FootIsGrounded.transform.position.y, transform.position.z);

            isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        public void Move(Vector2 input)
        {
            movement = new Vector3(input.x, 0, input.y);
            Vector3 moveVec = movement * Time.deltaTime * moveSpeed;

            transform.Translate(moveVec, Space.Self);
            //unityCharacterController.Move(moveVec);
        }

        public void Rotate(float inputX)
        {
            SpinRotationY += inputX;
            transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
        }
        
        public void Jump()
        {
            if (isGrounded)
            {
                isJump = true;
                animator.SetBool("ClickJump", isJump);
            }
        }
        public void JumpPower()
        {
            if(isGrounded)
            {
                if (VerticalVelocity <= GroundedRadius)
                {
                    animator.SetBool("Grounded", isGrounded);
                    animator.SetBool("OnAir", false);

                    if (isJump)
                    {
                        VerticalVelocity = Mathf.Sqrt(JumpHeight * -1f * Gravity);

                        animator.SetBool("Grounded", false);
                    }
                }
            }
            else
            {
                isJump = false;

                animator.SetBool("ClickJump", isJump);

                if (VerticalVelocity > GroundedRadius)
                {
                    animator.SetBool("OnAir", true);

                    VerticalVelocity += Gravity * Time.deltaTime;
                }
                
            }
        }
    }

}
