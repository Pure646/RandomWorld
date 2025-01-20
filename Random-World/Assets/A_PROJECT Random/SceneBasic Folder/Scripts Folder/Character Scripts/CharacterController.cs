using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterBase characterBase;
        public IngameUI ingameUI;
        public CharacterHP characterHP;

        private float targetAngle;
        [Range(0f, 2f)]
        public float rotationSpeed = 1.5f;

        public Transform cameraPivot;
        public float cameraClampMax = 80f;
        public float cameraClampMin = -80f;

        public GameObject weaponHolder;
        private WeaponBase weaponBase;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            InputSystem.Instance.Run += Running;
            InputSystem.Instance.Walk += Walking;
            InputSystem.Instance.Jump += Jumping;
            InputSystem.Instance.OnEquipWeapon += CommandEquip;
            InputSystem.Instance.OnHolsterWeapon += CommandHolster;
            InputSystem.Instance.Fire += BulletFire;
            InputSystem.Instance.ReloadWeapon += WeaponReload;

        }
        private void WeaponReload()
        {
            if (weaponBase != null)
            {
                if (weaponBase.bullet_remain < weaponBase.remain_Max_bullet)
                {
                    characterBase.ReloadWeapon();
                    weaponBase.Reloading();
                    //StartCoroutine(wating());
                }
            }
        }
        private void BulletFire()
        {
            characterBase.Fire();
        }
        private void Walking()
        {
            characterBase.Walking();
        }
        private void Running()
        {
            characterBase.Running();
        }
        private void CommandHolster()
        {
            characterBase.HolsterWeapon(null);
            Cursor.lockState = CursorLockMode.None;
        }

        private void CommandEquip(int index)
        {
            characterBase.EquipWeapon(index);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Jumping()
        {
            characterBase.Jump();
        }


        private void Update()
        {
            characterBase.AimingPoint = CameraSystem.Instance.CameraAimingPoint;

            Vector2 MoveInput = InputSystem.Instance.Movement;
            characterBase.Move(MoveInput);

            Vector2 LookInput = InputSystem.Instance.Looking;
            characterBase.Rotate(LookInput.x * rotationSpeed);

            UpdateCameraRotate(LookInput.y);

            if (weaponBase == null)
            {
                weaponBase = weaponHolder.GetComponentInChildren<WeaponBase>();
            }
            else
            {
                return;
            }
        }
        private IEnumerable wating()
        {
            yield return new WaitForSeconds(2f);

            characterBase.Reloading = false;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }

        private void UpdateCameraRotate(float axisY)
        {            
            targetAngle = ClampAngle(
                targetAngle + axisY, 
                cameraClampMin, cameraClampMax);
            Quaternion rotationResult = Quaternion.Euler(targetAngle * -1f * rotationSpeed, cameraPivot.rotation.eulerAngles.y, 0);
            cameraPivot.rotation = rotationResult;
        }
    }
}

