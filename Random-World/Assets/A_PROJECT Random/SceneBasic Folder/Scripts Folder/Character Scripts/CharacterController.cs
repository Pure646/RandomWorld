using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class CharacterController : MonoBehaviour
    {
        private IDamage Damage;
        public CharacterBase characterBase;
        public IngameUI ingameUI;

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
            Damage = gameObject.GetComponent<IDamage>();
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
            InputSystem.Instance.SelfDamage += SelfDamage;
            InputSystem.Instance.SelfHeal += SelfHealing;

            characterBase.OnDamaged += OnReceiveDamaged;

            IngameUI.Instance.SetHP(characterBase.CharacterHP, characterBase.CharacterMax);
        }

        private void OnReceiveDamaged()
        {
            IngameUI.Instance.SetHP(characterBase.CharacterHP, characterBase.CharacterMax);
        }

        private void SelfDamage()
        {
            Damage.ApplyDamage(out float damage);
        }
        private void SelfHealing()
        {
            Damage.ApplyHeal(out float Heal);
        }
        private void WeaponReload()
        {
            if (weaponBase != null)
            {
                if (weaponBase.bullet_remain < weaponBase.remain_Max_bullet)
                {
                    characterBase.ReloadWeapon();
                    weaponBase.Reloading();
                    StartCoroutine(wating());
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

            MoveController();
            RotateController();

            if (weaponBase == null)
            {
                weaponBase = weaponHolder.GetComponentInChildren<WeaponBase>();
            }
            else
            {
                return;
            }
        }
        private void RotateController()
        {
            if(characterBase.CharacterHP > 0)
            {
                Vector2 LookInput = InputSystem.Instance.Looking;
                characterBase.Rotate(LookInput.x * rotationSpeed);
                UpdateCameraRotate(LookInput.y * -1f * rotationSpeed);
            }
        }
        private void MoveController()
        {
            if(characterBase.CharacterHP > 0)
            {
                Vector2 MoveInput = InputSystem.Instance.Movement;
                characterBase.Move(MoveInput);
            }
        }
        public IEnumerator wating()
        {
            yield return new WaitForSeconds(1.8f);
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
            Quaternion rotationResult = Quaternion.Euler(targetAngle, cameraPivot.rotation.eulerAngles.y, 0);
            cameraPivot.rotation = rotationResult;
        }
    }
}

