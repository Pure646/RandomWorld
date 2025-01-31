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
        [SerializeField] private float regenerateCoolTime = 5f;

        public Transform PlayerTransform;
        private CapsuleCollider capsulecollider;
        private Animator animator;

        private float lastGenerateTime = 0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            capsulecollider = GetComponent<CapsuleCollider>();
        }
        
        private void Start()
        {
            ResetSetting();
        }
        void Update()
        {
            SetTime();
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
