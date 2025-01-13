using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class WeaponBase : MonoBehaviour
    {
        public enum WeaponType
        {
            None,

            AK12,
            AK74,
            G3A4,
            TAR21,

            Glock_17,
            Tec_9,
            Deagle,
        }
        public GameObject Bullet;
        public Transform ResponeBullet;
        public float powerBullet = 10f;

        private float FireLatingTime = 0f;
        public float FireLatingOffsetTime;

        [field: SerializeField] public Vector3 OffsetPosition { get; private set; }
        [field: SerializeField] public Vector3 OffsetRotation { get; private set; }
        [field: SerializeField] public Vector3 HandOffsetPosition { get; private set; }
        [field: SerializeField] public Vector3 HandOffsetRotation { get; private set; }

        private void Update()
        {
            FireLating();
        }

        public void FireLating()
        {
            if(FireLatingTime > 0f)
            {
                FireLatingTime -= 10f * Time.deltaTime;
            }
        }

        public void Fire()
        {
            if (Bullet != null)
            {
                if(FireLatingTime <= 0)
                {
                    FireLatingTime = FireLatingOffsetTime;
                    GameObject newBullet = Instantiate(Bullet, ResponeBullet.position, ResponeBullet.rotation);
                    Rigidbody newBulletRigid = newBullet.GetComponent<Rigidbody>();
                    newBullet.SetActive(true);

                    newBulletRigid.AddForce(ResponeBullet.forward * powerBullet, ForceMode.Impulse);
                    if(newBullet != null)
                    {
                        Destroy(newBullet, 5);
                    }
                }
            }
        }
        
        public WeaponType weaponType;

    }
}
