using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class TestBase : MonoBehaviour
    {
        private Rigidbody rb;
        private float horizontal;
        private float vertical;
        private Vector3 MoveVec;

        [SerializeField] private float MoveSpeed = 2f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            MoveVec = new Vector3(horizontal, 0f , vertical).normalized;
            rb.MovePosition(transform.position + MoveVec * MoveSpeed * Time.deltaTime);

            int a = 10;
            float b = 20;
            GetComponent<Rigidbody>();
            //GetTotal<int, float>(a, b);
        }

        //public float GetTotal(float a, float b)
        //{
        //    return a + b;
        //}

        //public int GetTotal(int a, int b)
        //{
        //    return a + b;
        //}

        //public double GetTotal(double a, double b)
        //{
        //    return a + b;
        //}

        //public T GetTotal<T, K>(T a, K b)
        //{
        //    return a + b;
        //}
    }
}
