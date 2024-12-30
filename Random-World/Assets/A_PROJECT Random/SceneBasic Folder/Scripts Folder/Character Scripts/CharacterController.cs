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
        }

        private void CommandEquip(int index)
        {
            characterBase.EquipWeapon(index);
        }
        
        private void Jumping()
        {
            characterBase.Jump();
        }

        private void Update()
        {
            Vector2 MoveInput = InputSystem.Instance.Movement;
            characterBase.Move(MoveInput);

            Vector2 LookInput = InputSystem.Instance.Looking;
            characterBase.Rotate(LookInput.x);

            //ingameUI.SetHP(characterBase.CurrentHP, characterBase.characterData.MaxHP);
        }
    }
}

