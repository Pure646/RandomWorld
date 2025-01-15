using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class Boxunity : MonoBehaviour, IDamage
    {
        public float boxHealth;
        public float Damage;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            animator.SetFloat("CurrentHealth", boxHealth);
        }
        public void ApplyDamage(out float Health)
        {
            boxHealth -= Damage;
            Health = boxHealth;
            Debug.Log($"CurrentHealth : {Health}");
        }
        public void ApplyHeal(float Heal)
        {
            boxHealth += Heal;

            Debug.Log($"Full_Heal : {boxHealth}");
        }
    }
}
