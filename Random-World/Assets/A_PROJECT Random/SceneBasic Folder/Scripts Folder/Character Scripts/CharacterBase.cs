using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

namespace RandomWorld
{
    public class CharacterBase : MonoBehaviour
    {
        private Animator animator;

        private Vector3 movement;
        [Header("▼---Character Move---▼")]
        public float moveSpeed = 3f;

        private float horizontalBlend;
        private float verticalBlend;
        private float JumpBlend;
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
        private float Speed;
        private float fallTimeoutDelta;
        private float jumpTimeoutDelta;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

        public void Jump()
        {
            if(isGrounded)
            {
                fallTimeoutDelta = fallTimeout;
                isJump = true;
                animator.SetBool("IsJump", false);
                animator.SetBool("IsFreeFall", false);

                if(verticalVelocity < 0f)
                {
                    verticalVelocity = -2f;
                }
                if(isJump && jumpTimeoutDelta <= 0f)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    
                    animator.SetBool("IsJump", true);
                }
                if (jumpTimeoutDelta >= 0f)
                {
                    jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                jumpTimeoutDelta = jumpTimeout;
                if(fallTimeoutDelta >= 0f)
                {
                    fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool("IsFreeFall", true);
                }
                isJump = false;
            }
        }
        
        public void Gravity()
        {
            if (verticalVelocity < terminalVelocity)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
        }
        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            if (animator)
            {
                animator.SetBool("IsGrounded", isGrounded);
                animator.SetBool("IsFreeFall", false);
            }
        }


        private bool isRun;
        public float RunSpeed;

        [Header("▼---WeaponBase---▼")]
        [SerializeField] private WeaponBase weaponPrefab_1;
        [SerializeField] private WeaponBase weaponPrefab_2;
        [SerializeField] private WeaponBase weaponPrefab_3;

        [SerializeField] private WeaponBase primaryWeapon;
        [SerializeField] private WeaponBase secondaryWeapon;
        [SerializeField] private WeaponBase thirdWeapon;

        [SerializeField] private Transform primarySocket;
        [SerializeField] private Transform secondarySocket;
        [SerializeField] private Transform thirdSocket;
        [SerializeField] private Transform weaponHolder;

        public CharacterHP characterData;

        private Transform holsterTargetSocket;
        private bool isEquipmentChanging = false; // 장비를 바꾸는 중일 때, TRUE (:장비를 꺼내거나 넣을 때)
        public float CurrentHP => currentHP;
        private float currentHP;

        private int socketIndex = -1;
        private WeaponBase currentWeapon;
        private WeaponBase weaponToEquip;

        private Transform rightHandTransform;


        private void Awake()
        {

            animator = GetComponent<Animator>();
            unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            rightHandTransform = animator.GetBoneTransform(HumanBodyBones.RightHand);
        }

        private void Start()
        {
            InputSystem.Instance.ReloadWeapon += ReloadWeapon;

            primaryWeapon = Instantiate(weaponPrefab_1, primarySocket);
            primaryWeapon.transform.SetLocalPositionAndRotation(
                    primaryWeapon.OffsetPosition,
                    Quaternion.Euler(primaryWeapon.OffsetRotation));
            //currentHP = characterData.MaxHP;
        }

        private void Update()
        {
            GroundedCheck();
            Gravity();

            animator.SetFloat("Horizontal", horizontalBlend);
            animator.SetFloat("Vertical", verticalBlend);
            animator.SetFloat("Magnitude", movement.magnitude);
            animator.SetFloat("Running Blend", isRun ? RunSpeed : 0f);
        }
       
        public void Running()
        {
            isRun = true;
        }
        public void Walking()
        {
            isRun = false;
        }

        public void Move(Vector2 input)
        {
            movement = (transform.right * input.x) + (transform.forward * input.y);
            horizontalBlend = Mathf.Lerp(horizontalBlend, input.x, 10 * Time.deltaTime);
            verticalBlend = Mathf.Lerp(verticalBlend, input.y, 10 * Time.deltaTime);

            Vector3 moveVec = movement * Time.deltaTime;
            if (isRun)
            {
                unityCharacterController.Move(moveVec * RunSpeed);
            }
            else
            {
                unityCharacterController.Move(moveVec * moveSpeed);
            }
        }
        public void Rotate(float inputX)
        {
            SpinRotationY += inputX;
            transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
        }

        public void ReloadWeapon()
        {
            animator.SetTrigger("Reload Trigger");
        }

        public void HolsterWeapon(Transform targetSocket, WeaponBase nextWeapon = null)
        {
            if (isEquipmentChanging)
                return;

            holsterTargetSocket = targetSocket;
            isEquipmentChanging = true;
            animator.SetTrigger("Holster Trigger");
        }

        public void HolsterComplete()
        {
            isEquipmentChanging = false;

            currentWeapon.transform.SetParent(holsterTargetSocket);
            currentWeapon.transform.SetLocalPositionAndRotation(currentWeapon.OffsetPosition, Quaternion.Euler(currentWeapon.OffsetRotation));
            animator.SetFloat("Equip Blend", 0f);

            currentWeapon = null;
            if (weaponToEquip != null)
            {
                EquipWeapon(weaponToEquip);
            }
        }


        public void EquipWeapon(WeaponBase weapon)
        {
            if (isEquipmentChanging)
                return;

            isEquipmentChanging = true;
            currentWeapon = weapon;
            animator.SetTrigger("Equip Trigger");
            animator.SetFloat("Equip Blend", 1f);
        }

        public void EquipComplete()
        {
            isEquipmentChanging = false;

            currentWeapon = weaponToEquip;
            currentWeapon.transform.SetParent(weaponHolder);
            currentWeapon.transform.SetLocalPositionAndRotation(
                currentWeapon.HandOffsetPosition, Quaternion.Euler(currentWeapon.HandOffsetRotation));

            weaponToEquip = null;
        }

        public void EquipWeapon(int index)
        {
            if (isEquipmentChanging)
                return;

            if (index == 0)
            {
                weaponToEquip = primaryWeapon;
            }
            else if (index == 1)
            {
                weaponToEquip = secondaryWeapon;
            }
            else if (index == 2)
            {
                weaponToEquip = thirdWeapon;
            }

            if (currentWeapon != null)
            {
                Transform targetSocket = null;
                if (currentWeapon == primaryWeapon)
                {
                    targetSocket = primarySocket;
                }
                else if (currentWeapon == secondaryWeapon)
                {
                    targetSocket = secondarySocket;
                }
                else if (currentWeapon == thirdWeapon)
                {
                    targetSocket = thirdSocket;
                }

                HolsterWeapon(targetSocket, weaponToEquip);
            }
            else
            {
                EquipWeapon(weaponToEquip);
            }
        }
    }
}
