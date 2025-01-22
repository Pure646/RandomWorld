using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class Boxunity : MonoBehaviour, IDamage
    {
        [SerializeField] private float boxHealthMax;
        [SerializeField] private float boxHealth;
        [SerializeField] private float Damage;
        [SerializeField] private float Healing;

        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            StartCoroutine(HPReset());
            animator.SetFloat("CurrentHealth", boxHealth);
        }
        private IEnumerator HPReset()
        {
            yield return new WaitForSeconds(5f);
            boxHealth = boxHealthMax;
        }
        public void ApplyDamage(out float NumDamage)
        {
            NumDamage = Damage;
            boxHealth -= NumDamage;
            Debug.Log($"CurrentHealth : {boxHealth}");
        }
        public void ApplyHeal(out float Heal)
        {
            Heal = Healing;
            boxHealth += Heal;
            Debug.Log($"Full_Heal : {boxHealth}");
        }
    }
}
