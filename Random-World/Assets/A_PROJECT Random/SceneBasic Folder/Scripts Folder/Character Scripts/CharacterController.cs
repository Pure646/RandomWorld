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
            InputSystem.Instance.Jump += OnJump;

            InputSystem.Instance.OnEquipWeapon += CommandEquip;
            InputSystem.Instance.OnHolsterWeapon += CommandHolster;
        }
        private void CommandHolster()
        {
            characterBase.HolsterWeapon(null);
        }

        private void CommandEquip(int index)
        {
            characterBase.EquipWeapon(index);
        }
        
        private void OnJump()
        {
            characterBase.Jump();
        }

        private void Update()
        {
            Vector2 MoveInput = InputSystem.Instance.Movement;
            characterBase.Move(MoveInput);

            Vector2 LookInput = InputSystem.Instance.Looking;
            characterBase.Rotate(LookInput.x);

            ingameUI.SetHP(characterBase.CurrentHP, characterBase.characterData.MaxHP);
        }
    }
}

