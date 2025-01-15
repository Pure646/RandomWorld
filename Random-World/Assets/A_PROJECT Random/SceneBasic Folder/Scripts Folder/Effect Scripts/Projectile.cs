using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RandomWorld
{
    public class Projectile : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Vector3 position = collision.contacts[0].point;
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

            if (collision.transform.root.TryGetComponent(out CharacterBase character))
            {

            }
            else
            {
                if (collision.collider.material.name.Contains("Ground"))
                {
                    // TODO : Ground Effect 출력
                    EffectManager.Instance.SpawnEffect("Impact_Ground", position, rotation);
                }
                else if (collision.collider.material.name.Contains("Wall"))
                {
                    // TODO : Wall Effect 출력
                    EffectManager.Instance.SpawnEffect("Impact_Wall", position, rotation);
                }
                else if (collision.collider.CompareTag("Bot"))
                {
                    EffectManager.Instance.SpawnEffect("Impact_Bot", position, rotation);

                    IDamage damageInterface = collision.collider.GetComponent<IDamage>();
                    damageInterface.ApplyDamage(out float Health);
                    if(Health <= -1000 )
                    {
                        damageInterface.ApplyHeal(5000);
                    }
                }
            }

            Destroy(gameObject);
        }
        //if(collision.collider.CompareTag("Character"))
        //{

        //}
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        //{

        //}
        //else
        //{

        //}
    }
}
