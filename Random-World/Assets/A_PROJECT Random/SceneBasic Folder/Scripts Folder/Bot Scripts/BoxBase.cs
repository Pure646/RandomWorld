using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RandomWorld
{
    public class BoxBase : MonoBehaviour
    {
        [SerializeField] private Transform UnityChan;
        [SerializeField] private UnityEngine.CharacterController characterController;

        private CharacterBase characterbase;
        private Animator animator;
        private NavMeshAgent agent;

        private float Magnitude;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            characterController = GetComponent<UnityEngine.CharacterController>();
            
        }
        private void Start()
        {
            agent.speed = 1f;
        }
        private void Update()
        {
            Order_AI();
            Move();
            Rotate();
        }
        private void Order_AI()
        {
            agent.destination = UnityChan.transform.position;
        }
        private void Move()
        {
            Vector3 movement = (transform.forward * agent.velocity.z) + (transform.right * agent.velocity.x);

            //characterController.Move(movement);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.z);
            animator.SetFloat("Magnitude",movement.magnitude);
        }

        private void Rotate()
        {
            //transform.LookAt(UnityChan.position);
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

            //agent.Move(Vector3 offset)
            // ������ ���ưٸ�ŭ AI�� ��� �̵���Ŵ (AI�� NavMesh�� ����� ����.

            //agent.updateRotation = false;
            // �̵� �� ȸ�� ���θ� �����ϴ� �Ӽ�. true�� �����ϸ� AI�� ��ǥ�� ���� ȸ����.
        }
    }
}
