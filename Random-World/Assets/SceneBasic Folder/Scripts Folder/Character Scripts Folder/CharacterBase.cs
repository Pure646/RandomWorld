using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [Header("¡å---Character Move---¡å")]
    [SerializeField] private Vector3 movement;
    public float moveSpeed;
    private void Update()
    {
        Vector3 moveVector = new Vector3(movement.x, movement.y, movement.z);
        transform.Translate(moveVector * Time.deltaTime * moveSpeed, Space.Self);
    }
}
