using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    private Animator animator;
    
    [Header("¡å---Character Move---¡å")]
    private Vector3 movement;
    public float moveSpeed = 3f;

    private float SpinRotationY = 0f;
    private float SpinRotationX = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.z);
        animator.SetFloat("Magnitude", movement.magnitude);
    }
    public void Move(Vector2 input)
    {
        movement = new Vector3(input.x, 0, input.y);
        transform.Translate(movement * Time.deltaTime * moveSpeed, Space.Self);
    }
    public void Rotate(float inputX, float inputY)
    {
        SpinRotationY += inputX;
        SpinRotationX -= inputY;
        transform.rotation = Quaternion.Euler(0, SpinRotationY, 0);
    }
}
