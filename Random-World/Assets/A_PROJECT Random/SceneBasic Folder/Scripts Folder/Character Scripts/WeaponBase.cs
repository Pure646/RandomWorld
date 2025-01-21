using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public CharacterBase character;
        public WeaponType weaponType;
        public GameObject Bullet;
        
        public Transform ResponeBullet;
        public float powerBullet = 10f;

        private float FireLatingTime = 0f;
        public float FireLatingOffsetTime;

        [field: SerializeField] public Vector3 OffsetPosition { get; private set; }
        [field: SerializeField] public Vector3 OffsetRotation { get; private set; }
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
        [field: SerializeField] public int remain_Max_bullet { get; private set; }
        [field: SerializeField] public int bullet_remain { get; private set; }

        private void Start()
        {
            GameObject gameComponent = GameObject.Find("unitychan");

            if(gameComponent != null)
            {
                character = gameComponent.GetComponent<CharacterBase>();
            }
        }
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
                if(FireLatingTime <= 0 && bullet_remain > 0 && bullet_remain <= remain_Max_bullet)
                {
                    if(character.Reloading == false)
                    {
                        FireLatingTime = FireLatingOffsetTime;
                        GameObject newBullet = Instantiate(Bullet, ResponeBullet.position, ResponeBullet.rotation);
                        Rigidbody newBulletRigid = newBullet.GetComponent<Rigidbody>();
                        newBullet.SetActive(true);

                        newBulletRigid.AddForce(ResponeBullet.forward * powerBullet, ForceMode.Impulse);

                        bullet_remain -= 1;
                        if (newBullet != null)
                        {
                            Destroy(newBullet, 5);
                        }
                    }
                }
            }
        }

        public void Reloading()
        {
            bullet_remain = remain_Max_bullet;
        }
    }
}
