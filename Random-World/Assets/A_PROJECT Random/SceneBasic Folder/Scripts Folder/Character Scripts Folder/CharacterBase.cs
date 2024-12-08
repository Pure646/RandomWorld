using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

namespace RandomWorld
{
    public class CharacterBase : MonoBehaviour
    {
        private Animator animator;

        private Vector3 movement;
        [Header("��---Character Move---��")]
        public float moveSpeed = 3f;

        private float horizontalBlend;
        private float verticalBlend;
        private float SpinRotationY = 0f;

        private UnityEngine.CharacterController unityCharacterController;

        public float jumpHeight = 1.2f;
        public float fallTimeout = 0.15f;
        public float jumpTimeout = 0.50f;
        public float gravity = -15.0f;

        public float groundedOffset = -0.14f;
        public float groundedRadius = 0.28f;
        public LayerMask groundLayers;

        private bool isJump = false;
        private bool isGrounded;
        private float fallTimeoutDelta;
        private float jumpTimeoutDelta;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        private bool isRun;
        public float RunSpeed;

        [Header("��---UsedWeapon Base---��")]
        public GameObject OneMainWeaponStand;
        public GameObject TwoMainWeaponStand;
        public GameObject OneWeapon;
        public GameObject TwoWeapon;

        private bool OneMainWeapon;
        private bool TwoMainWeapon;
        private bool EquipWeapon;
        private bool ChangeWeapon;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
        }

        private void Update()
        {
            Running();
            JumpAndGravity();
            GroundedCheck();
            UsedWeapon();
            OnWeapon();

            animator.SetFloat("Horizontal", horizontalBlend);
            animator.SetFloat("Vertical", verticalBlend);
            animator.SetFloat("Magnitude", movement.magnitude);
            animator.SetFloat("IsRun", isRun ? 1f : 0f);
        }
        #region GroundedCheck
        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            if (animator)
            {
                animator.SetBool("IsGrounded", isGrounded);
            }
        }
        #endregion
        #region Running
        public void Running()
        {
            if(UnityEngine.Input.GetKey(KeyCode.LeftShift))
            {
                isRun = true;
            }
            else
            {
                isRun = false;
            }
        }
        #endregion
        #region JumpAndGravity
        private void JumpAndGravity()
        {
            if (isGrounded)
            {
                fallTimeoutDelta = fallTimeout;

                if (animator)
                {
                    animator.SetBool("IsJump", false);
                    animator.SetBool("IsFreeFall", false);
                }

                if (verticalVelocity < 0.0f)
                {
                    verticalVelocity = -2f;
                }

                // Jump
                if (isJump && jumpTimeoutDelta <= 0.0f)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                    if(animator)
                    {
                        animator.SetBool("IsJump", true);
                    }
                }

                if (jumpTimeoutDelta >= 0.0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;
                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    if (animator)
                    {
                        animator.SetBool("IsFreeFall", true);
                    }
                }

                isJump = false;
            }

            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }
        #endregion

        #region Move
        public void Move(Vector2 input)
        {
            movement = (transform.right * input.x) + (transform.forward * input.y);
            horizontalBlend = Mathf.Lerp(horizontalBlend, input.x, 10 * Time.deltaTime);
            verticalBlend = Mathf.Lerp(verticalBlend, input.y, 10 * Time.deltaTime);

            Vector3 moveVec = movement * Time.deltaTime;
            if(isRun)
            {
                unityCharacterController.Move(moveVec * RunSpeed);
            }
            else
            {
                unityCharacterController.Move(moveVec * moveSpeed);
            }

            moveVec.y += verticalVelocity * Time.deltaTime;
        }
        #endregion
 
        public void OnWeapon()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha1) && OneMainWeapon == true)
            {
                if(EquipWeapon == false)
                {
                    EquipWeapon = true;
                    animator.SetTrigger("Equip Trigger");
                }
                if(EquipWeapon == true)
                {
                    EquipWeapon = false;
                    animator.SetTrigger("Holster Trigger");
                }
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha2) && (TwoMainWeapon == true && EquipWeapon == false))
            {

            }
            
        }
        public void UsedWeapon()
        {
            if (OneWeapon == null || TwoWeapon == null)
                return;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha8))
            {
                GameObject newWeapon = Instantiate(OneWeapon, OneMainWeaponStand.transform);
                newWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(-24f, -93f, -11.195f));
                newWeapon.SetActive(true);
                OneMainWeapon = true;
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha9))
            {
                GameObject newWeapon = Instantiate(TwoWeapon, TwoMainWeaponStand.transform);
                newWeapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(-26.313f, -97.603f, 0f));
                newWeapon.SetActive(true);
                TwoMainWeapon = true;
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.T))
            {
                OneMainWeapon = false;
                TwoMainWeapon = false;
            }
        }
        public void Rotate(float inputX)
        {
            SpinRotationY += inputX;
            transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
        }

        public void Jump()
        {
            isJump = true;
        }
        public void OnChangeWeaponToBack()
        {
            animator.SetTrigger("Equip Trigger");
        }
    }
}
