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
            // -�Ӽ�-

            //agent.destination = PlayerTransform.position;
            // destination : Ai�� �̵��Ϸ��� ��ǥ ��ġ�� �����ϰų� ������

            agent.speed = 5f;
            // speed : Ai�� �̵� �ӵ��� ����. �⺻���� 3.5

            agent.isStopped = true;
            // IsStopped : true�� �����ϸ� �̵��� ����. false�� �����ϸ� �̵��� ��ӵ�.

            if (agent.remainingDistance < 0.5f)
            {

            }
            // ��ǥ���������� ���� �Ÿ��� ��ȯ�Ѵ�. ��ǥ�� �������� ��� 0�� �ȴ�.

            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                // NavMeshPathStatus.PathComplete   : ��ΰ� ������
                // NavMeshPathStatus.PathPartial    : ��ΰ� �Ϻθ� ����
                // NavMeshPathStatus.PathInvalid    : ��ΰ� ��ȿ���� ����
            }
            // Ai�� ���� ��ǥ�� ������ ����� ���¸� ��ȯ.

            agent.velocity = Vector3.zero;
            // ���� Ai�� �̵��ӵ�(����) ��ȯ. �̵� ���� �ӵ��� ������ �� �� ����.

            agent.stoppingDistance = 2f;
            // stoppingDistance : ��ǥ ������ �����ϱ� ���� ���� �Ÿ�. ��ǥ ��ó���� Ai�� ���߰� ����.
        }

        private void NavMeshAgentAI_2()
        {
            // -�޼���-

            //agent.SetDestination(Vedctor3 target)
            // ��ǥ ������ �����ϴ� �޼���

            //agent.Warp(Vector3 position)
            // Ai�� ������ ��ġ�� ��� �̵���Ŵ(�����̵�)

            //agent.Resume();
            // isStopped�� true �� ���, �̵��� �簳�ϴ� �޼���

            //agent.Stop();
            // Ai�� �̵��� ���߰� �ϴ� �޼���

            //agent.MonsterMove(Vector3 offset)
            // ������ ���ưٸ�ŭ AI�� ��� �̵���Ŵ (AI�� NavMesh�� ����� ����.

            //agent.updateRotation = false;
            // �̵� �� ȸ�� ���θ� �����ϴ� �Ӽ�. true�� �����ϸ� AI�� ��ǥ�� ���� ȸ����.
        }
    }
}
