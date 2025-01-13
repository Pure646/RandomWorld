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
        private float fallTimeoutDelta;
        private float jumpTimeoutDelta;
        private float verticalVelocity;
        private float terminalVelocity = 53.0f;

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

        public LayerMask GroundLayer;
        private Collider[] CheckCollider;
        private float GroundRadius = 0.51f;
        private bool IsGrounded;
        private float JumpValueNumber = 0f;
        public float JumpHeightNumber = 1.5f;

        private float CurrentHeight;
        private float GroundedOffset = 0.5f;
        private float Gravity = -9.81f;
        private float JumpUPTime;
        public float JumpDownTime = 2f;

        private bool ClickJump;
        private bool PossibleJump;
        private float JumpRemainTime = 0f;
        private float sqrting;
        public float JumpCoolTimeSet = 1.5f;
        public void Jump()
        {
            if (JumpRemainTime <= 0f)
            {
                ClickJump = true;
            }
        }
        public void JumpCoolTime()
        {

            if (JumpRemainTime > 0)
            {
                JumpRemainTime -= Time.deltaTime;

                if (PossibleJump == false && JumpRemainTime <= 0)
                {
                    PossibleJump = true;
                    Debug.Log("PossibleJump = true");
                }
            }
            else if (JumpRemainTime <= 0 && ClickJump == true)
            {
                JumpRemainTime = JumpCoolTimeSet;
                ClickJump = false;

                if (PossibleJump == true)
                {
                    PossibleJump = false;
                }
            }
            if (JumpUPTime >= 0f)
            {
                JumpUPTime -= Time.deltaTime;
            }
        }
        public void JumpingAndGravity()
        {
            if (ClickJump == false)
            {
                if (IsGrounded == false && JumpUPTime < 0f)
                {
                    CurrentHeight += Gravity * Time.deltaTime;
                    //CurrentHeight = Mathf.Lerp(CurrentHeight, transform.position.y + Gravity, Time.deltaTime);
                }
                else
                {
                    CurrentHeight = Mathf.Lerp(CurrentHeight, JumpHeightNumber, Time.deltaTime);
                    //Ai.Translate(transform.up * CurrentHeight, Space.Self);

                }
                //Debug.Log(CurrentHeight);
            }
            else if (ClickJump == true)
            {
                //JumpValueNumber = transform.position.y + JumpHeightNumber;
                JumpUPTime = JumpDownTime;
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

            bool isAlreadySameWeapon =
                (index == 0 && currentWeapon == primaryWeapon) ||
                (index == 1 && currentWeapon == secondaryWeapon) ||
                (index == 2 && currentWeapon == thirdWeapon);

            if (isAlreadySameWeapon)
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

        public void Fire()
        {
            if (currentWeapon != null)
            {
                currentWeapon.Fire();
            }
        }
    }
}
