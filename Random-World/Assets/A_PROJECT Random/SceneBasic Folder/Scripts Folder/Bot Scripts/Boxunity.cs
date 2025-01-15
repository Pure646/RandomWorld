using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class Boxunity : MonoBehaviour
    {
        private bool check;
        public GameObject character;
        public LayerMask layer;
        public float radian = 1.5f;

        private void Update()
        {
            //Check();

        }
        private void Check()
        {
            //check = Physics.CheckSphere(transform.position + new Vector3(0, 1, 0), radian, layer, QueryTriggerInteraction.Ignore);

            //if(check)
            //{
            //    transform.position = new Vector3(Mathf.Lerp(transform.position.x, character.transform.position.x, Time.deltaTime)
            //        , 0, Mathf.Lerp(transform.position.z, character.transform.position.z, Time.deltaTime));
            //}
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            
        }
    }
}
