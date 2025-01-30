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
            // -속성-

            agent.destination = PlayerTransform.position;
            // destination : Ai가 이동하려는 목표 위치를 설정하거나 가져옴

            agent.speed = 5f;
            // speed : Ai의 이동 속도를 설정. 기본값은 3.5

            agent.isStopped = true;
            // IsStopped : true로 설정하면 이동이 멈춤. false로 설정하면 이동이 계속됨.

            if(agent.remainingDistance < 0.5f)
            {
                
            }
            // 목표지점까지의 남은 거리를 반환한다. 목표를 도달했을 경우 0이 된다.

            if(agent.pathStatus == NavMeshPathStatus.PathComplete)
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

            //agent.Move(Vector3 offset)
            // 지정된 오픗겟만큼 AI를 즉시 이동시킴 (AI가 NavMesh를 벗어나지 않음.

            //agent.updateRotation = false;
            // 이동 중 회전 여부를 설정하는 속성. true로 설정하면 AI가 목표를 향해 회전함.
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
                Debug.Log($"재생 : {Heal}");

                if (boxHealth > boxHealthMax)
                {
                    boxHealth = boxHealthMax;
                }
            }
            Debug.Log($"boxUnity 체력 = {boxHealth}");
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
