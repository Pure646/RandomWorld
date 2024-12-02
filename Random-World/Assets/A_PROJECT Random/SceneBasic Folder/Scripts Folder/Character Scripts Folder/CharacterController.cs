using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterBase characterBase;

        private void Awake()
        {
            characterBase = GetComponent<CharacterBase>();
        }

        private void Start()
        {
            InputSystem.Instance.Jump += ExecuteJump;
            InputSystem.Instance.Run += Running;
        }
        void Running()
        {
            characterBase.Running();
        }
        void ExecuteJump()
        {
            characterBase.Jump();
        }

        private void Update()
        {
            Vector2 MoveInput = InputSystem.Instance.Movement;
            characterBase.Move(MoveInput);

            Vector2 LookInput = InputSystem.Instance.Looking;
            characterBase.Rotate(LookInput.x);


        }
    }
}
