using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

    public Vector2 Movement => move;
    private Vector2 move;

    public Vector2 Looking => look;
    private Vector2 look;

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
    }
}
