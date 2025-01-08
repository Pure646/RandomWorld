using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class Projectile : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Vector3 position = collision.contacts[0].point;
            Quaternion rotation = Quaternion.LookRotation(collision.contacts[0].normal);

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
        }
    }
}
