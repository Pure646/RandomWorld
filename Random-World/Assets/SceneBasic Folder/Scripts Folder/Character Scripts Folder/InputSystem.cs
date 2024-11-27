using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

    private Vector2 Movement => movement;
    public Vector2 movement;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputY = Input.GetAxis("Vertical");
        movement = new Vector2(InputX, InputY);
    }
}
