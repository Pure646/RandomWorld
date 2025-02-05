using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace RandomWorld
{
    public class MonsterA : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask layer;

        private NavMeshAgent agent;
        private TextMeshPro textMeshPro;
        private Rigidbody rb;
        private MeshRenderer meshRenderer;

        public float interactionSphereRadius = 8f;
        public float maxSphereRadius = 10f;
        private Collider[] interactionSphereOverlapCollider;
        private Collider[] maxSphereOverlapCollider;
        private Collider[] StopSphereOverlapCollider;

        public float MonsterSpeed = 5f;
        private float rotationSpeed = 3f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionSphereRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxSphereRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, interactionSphereRadius - 2f);
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody>();
            meshRenderer = GetComponent<MeshRenderer>();
        }
        private void Start()
        {
            Transform textMeshTransform = transform.Find("TextMesh");
            if (textMeshTransform != null)
            {
                textMeshPro = textMeshTransform.GetComponent<TextMeshPro>();
            }
            NavMeshSetting();
        }
        private void Update()
        {
            Overlap();
        }

        private void OnDestroy()
        {
            Dead();
        }
        private void NavMeshSetting()
        {
            agent.speed = MonsterSpeed;
            agent.acceleration = MonsterSpeed;
            agent.updateRotation = false;
        }
        private void Overlap()
        {
            interactionSphereOverlapCollider = Physics.OverlapSphere(transform.position, interactionSphereRadius, layer, QueryTriggerInteraction.Ignore);
            maxSphereOverlapCollider = Physics.OverlapSphere(transform.position, maxSphereRadius, layer, QueryTriggerInteraction.Ignore);
            StopSphereOverlapCollider = Physics.OverlapSphere(transform.position, interactionSphereRadius - 2f, layer, QueryTriggerInteraction.Ignore);
            for (int i = 0; i < maxSphereOverlapCollider.Length; i++) 
            {
                MonsterMove();
                Rotate();
                for (int k = 0; k < interactionSphereOverlapCollider.Length; k++)
                {
                    UI_interaction();
                    for(int j = 0; j < StopSphereOverlapCollider.Length; j++)
                    {
                        StopMove();
                    }
                }
            }
        }
        private void MonsterMove()
        {
            agent.SetDestination(target.position);

            Vector3 movement = agent.steeringTarget - transform.position;
            Vector3 localposition = transform.InverseTransformPoint(movement);
            if(agent.isStopped == true && interactionSphereOverlapCollider.Length == 0)
            {
                meshRenderer.material.color = Color.red;
                agent.speed = MonsterSpeed;
                agent.isStopped = false;
                textMeshPro.enabled = false;
            }
        }
        
        private void UI_interaction()
        {
            agent.speed = 0f;
            agent.isStopped = true;
            meshRenderer.material.color = Color.yellow;
            if (textMeshPro != null)
            {
                textMeshPro.enabled = true;
            }
        }

        private void StopMove()
        {
            meshRenderer.material.color = Color.green;
            agent.velocity = Vector3.zero;
        }

        private void Rotate()
        {
            transform.LookAt(target.position + transform.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void Dead()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}
