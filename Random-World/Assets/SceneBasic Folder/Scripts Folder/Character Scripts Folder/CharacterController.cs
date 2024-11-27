using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterBase characterBase;

    private void Awake()
    {
        characterBase = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        Vector2 MoveInput = InputSystem.Instance.Movement;
        characterBase.Move(MoveInput);

        Vector2 LookInput = InputSystem.Instance.Looking;
        characterBase.Rotate(LookInput.x, LookInput.y);
    }
    
}
