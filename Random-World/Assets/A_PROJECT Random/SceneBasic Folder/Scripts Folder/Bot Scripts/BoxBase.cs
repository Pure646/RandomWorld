using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RandomWorld
{
    public class BoxBase : MonoBehaviour
    {
        [SerializeField] private Transform UnityChan;
        [SerializeField] private Transform Object;
        [SerializeField] private float MaxSpeed;
        [SerializeField] private float CurrentSpeed = 1f;

        private float CurrentTime;
        private float RandomNumber;

        private Animator animator;
        private NavMeshAgent agent;

        private float Magnitude;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            agent.speed = 1f;
            agent.acceleration = 1f;
            agent.stoppingDistance = 1f;
        }
        private void Update()
        {
            Move();
            Rotate();
            SetTime();
        }
        private void SetTime()
        {
            if (Time.time > CurrentTime + 2f)
            {
                CurrentTime = Time.time;
                Order_AI();
                Speed();
            }
        }
        private void Speed()
        {
            if(CurrentSpeed < MaxSpeed)
            {
                CurrentSpeed += 1f;
                agent.speed += 1f;
                agent.acceleration += 1f;
            }
            else if(CurrentSpeed >= MaxSpeed)
            {
                CurrentSpeed = MaxSpeed;   
            }
        }
        private void Order_AI()
        {
            RandomNumber = Random.Range(1, 5);
            //Debug.Log(RandomNumber);
            switch (RandomNumber)
            {
                case 1:
                    Object.position = new Vector3(0, 1, 0);
                    break;
                case 2:
                    Object.position = new Vector3(20, 1, 0);
                    break;
                case 3:
                    Object.position = new Vector3(0, 1, 20);
                    break;
                case 4:
                    Object.position = new Vector3(20, 1, 20);
                    break;
                default:
                    break;
            }
        }
        private void Move()
        {
            agent.SetDestination(Object.position);
            Vector3 movement = agent.steeringTarget - transform.position;
            Vector3 localposition = transform.InverseTransformPoint(movement);

            animator.SetFloat("Horizontal", localposition.x);
            animator.SetFloat("Vertical", localposition.z);
            animator.SetFloat("Magnitude", movement.magnitude);
        }

        private void Rotate()
        {
            float distance = Vector3.Distance(transform.position, Object.position);
            if(distance > 2f)
            {
                transform.LookAt(Object.position);
            }
        }

        private void NavMeshAgentAI_1()
        {
            // -속성-

            //agent.destination = PlayerTransform.position;
            // destination : Ai가 이동하려는 목표 위치를 설정하거나 가져옴

            agent.speed = 5f;
            // speed : Ai의 이동 속도를 설정. 기본값은 3.5

            agent.isStopped = true;
            // IsStopped : true로 설정하면 이동이 멈춤. false로 설정하면 이동이 계속됨.

            if (agent.remainingDistance < 0.5f)
            {

            }
            // 목표지점까지의 남은 거리를 반환한다. 목표를 도달했을 경우 0이 된다.

            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                // NavMeshPathStatus.PathComplete   : 경로가 완전함
                // NavMeshPathStatus.PathPartial    : 경로가 일부만 있음
                // NavMeshPathStatus.PathInvalid    : 경로가 유효하지 않음
            }
            // Ai가 현재 목표로 설정된 경로의 상태를 반환.

            agent.velocity = Vector3.zero;
            // 현재 Ai의 이동속도(벡터) 반환. 이동 중인 속도나 방향을 알 수 있음.

            agent.stoppingDistance = 2f;
            // stoppingDistance : 목표 지점에 도달하기 전에 멈출 거리. 목표 근처에서 Ai가 멈추게 설정.
        }

        private void NavMeshAgentAI_2()
        {
            // -메서드-

            //agent.SetDestination(Vedctor3 target)
            // 목표 지점을 설정하는 메서드

            //agent.Warp(Vector3 position)
            // Ai를 지정한 위치로 즉시 이동시킴(순간이동)

            //agent.Resume();
            // isStopped가 true 인 경우, 이동을 재개하는 메서드

            //agent.Stop();
            // Ai의 이동을 멈추게 하는 메서드

            //agent.MonsterMove(Vector3 offset)
            // 지정된 오픗겟만큼 AI를 즉시 이동시킴 (AI가 NavMesh를 벗어나지 않음.

            //agent.updateRotation = false;
            // 이동 중 회전 여부를 설정하는 속성. true로 설정하면 AI가 목표를 향해 회전함.
        }
    }
}
