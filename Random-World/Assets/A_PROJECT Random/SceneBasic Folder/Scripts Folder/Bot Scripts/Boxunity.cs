using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RandomWorld
{
    public class Boxunity : MonoBehaviour, IDamage
    {
        [SerializeField] private float boxHealthMax;
        [SerializeField] private float boxHealth;

        public Transform PlayerTransform;
        private NavMeshAgent agent;
        private CapsuleCollider capsulecollider;
        private Animator animator;

        [SerializeField] private float regenerateCoolTime = 5f;
        private float lastGenerateTime = 0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            capsulecollider = GetComponent<CapsuleCollider>();
        }
        
        private void Start()
        {
            ResetSetting();
            agent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            SetTime();
            agent.destination = PlayerTransform.position;
        }
        private void NavMeshAgentAI_1()
        {
            // -�Ӽ�-

            agent.destination = PlayerTransform.position;
            // destination : Ai�� �̵��Ϸ��� ��ǥ ��ġ�� �����ϰų� ������

            agent.speed = 5f;
            // speed : Ai�� �̵� �ӵ��� ����. �⺻���� 3.5

            agent.isStopped = true;
            // IsStopped : true�� �����ϸ� �̵��� ����. false�� �����ϸ� �̵��� ��ӵ�.

            if(agent.remainingDistance < 0.5f)
            {
                
            }
            // ��ǥ���������� ���� �Ÿ��� ��ȯ�Ѵ�. ��ǥ�� �������� ��� 0�� �ȴ�.

            if(agent.pathStatus == NavMeshPathStatus.PathComplete)
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
        private void ResetSetting()
        {
            boxHealth = boxHealthMax;
            Debug.Log(boxHealth);
            animator.SetFloat("CurrentHealth", boxHealth);
        }
            


        private void SetTime()
        {
            if (Time.time > lastGenerateTime + regenerateCoolTime)
            {
                lastGenerateTime = Time.time;
            }
        }
        private void RefreshAnimationByHealth()
        {
            animator.SetFloat("CurrentHealth", boxHealth);
            capsulecollider.enabled = boxHealth > 0;
        }
        public void ApplyHeal(float Heal)
        {
            RefreshAnimationByHealth();

            if (boxHealth < boxHealthMax)
            {
                boxHealth += Heal;
                Debug.Log($"��� : {Heal}");

                if (boxHealth > boxHealthMax)
                {
                    boxHealth = boxHealthMax;
                }
            }
            Debug.Log($"boxUnity ü�� = {boxHealth}");
        }

        public void ApplyDamage(float damage)
        {
            boxHealth -= damage;
            RefreshAnimationByHealth();

            lastGenerateTime = Time.time;

            Debug.Log($"CurrentHealth : {boxHealth}");
        }
        
    }
}
