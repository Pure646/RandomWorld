using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class InputSystem : MonoBehaviour
    {
        public static InputSystem Instance { get; private set; }

        public Vector2 Movement => move;
        private Vector2 move;

        public Vector2 Looking => look;
        private Vector2 look;

        public System.Action Jump;
        public System.Action Run;
        public System.Action Walk;
        public System.Action Fire;
        public System.Action<int> OnEquipWeapon;
        public System.Action OnHolsterWeapon;
        public System.Action ReloadWeapon;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            float InputX = Input.GetAxis("Horizontal");
            float InputY = Input.GetAxis("Vertical");
            move = new Vector2(InputX, InputY);

            float LookX = Input.GetAxis("Mouse X");
            float LookY = Input.GetAxis("Mouse Y");
            look = new Vector2(LookX, LookY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnEquipWeapon?.Invoke(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnEquipWeapon?.Invoke(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnEquipWeapon?.Invoke(2);
            }
            if(Input.GetKey(KeyCode.Mouse0))
            {
                Fire?.Invoke();
            }

            if(Input.GetKey(KeyCode.LeftShift))
            {
                Run?.Invoke();
            }
            else
            {
                Walk?.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.T))
            {
                OnHolsterWeapon?.Invoke();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                ReloadWeapon?.Invoke();
            }
        }
    }
}

