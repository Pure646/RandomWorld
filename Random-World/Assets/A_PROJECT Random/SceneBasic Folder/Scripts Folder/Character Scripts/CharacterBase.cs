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
            //Running();
            //JumpAndGravity();
            GroundedCheck();
            CheckLayer();

            animator.SetFloat("Horizontal", horizontalBlend);
            animator.SetFloat("Vertical", verticalBlend);
            animator.SetFloat("Magnitude", movement.magnitude);
            //animator.SetFloat("IsRun", isRun ? 1f : 0f);
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
        #region Move
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

            moveVec.y += verticalVelocity * Time.deltaTime;
        }
        #endregion
        #region Rotate
        public void Rotate(float inputX)
        {
            SpinRotationY += inputX;
            transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
        }
        #endregion


        private Transform holsterTargetSocket;
        private bool isEquipmentChanging = false; // 장비를 바꾸는 중일 때, TRUE (:장비를 꺼내거나 넣을 때)

        public LayerMask DropWeeapon;
        public LayerMask lay_2;
        private Collider[] DropWeaponOverlapped;
        public float rad;

        public void CheckLayer()
        {
            if (DropWeaponOverlapped != null && DropWeaponOverlapped.Length > 0)
            {
                for (int i = 0; i < DropWeaponOverlapped.Length; i++)
                {
                    var dropWeaponText = DropWeaponOverlapped[i].GetComponentInChildren<TextMeshPro>();
                    dropWeaponText.enabled = false;
                }
            }
            DropWeaponOverlapped = Physics.OverlapSphere(transform.position, rad, DropWeeapon, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < DropWeaponOverlapped.Length; i++)
            {
                var dropWeaponText = DropWeaponOverlapped[i].GetComponentInChildren<TextMeshPro>();
                dropWeaponText.enabled = true;
            }
        }

        //public bool RoomClear;
        //public bool RoomBlocken;
        //public GameObject Door;
        //public void OpentheDoor()
        //{
        //    //방을 클리어하면 문이 열린다.
        //    if(RoomClear)
        //    {
        //        float a = Mathf.Lerp(1, 10, 1);
        //        Door.transform.position = new Vector3(0, a, 0);
        //    }

        //    //특정 조건으로 문을 부수면 문이 열린다.
        //}

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
