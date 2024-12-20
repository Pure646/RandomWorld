using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class WeaponBase : MonoBehaviour
    {
        public enum WeaponName
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
        public WeaponName TypeWeapon;
        public GameObject[] gameObjects;
        public int WeaponNumber;

        private CharacterBase characterBase;
        public void SetCharacterBase(CharacterBase character)
        {
            characterBase = character;
        }


        public void WeaponPositionAndRotation()
        {
            GameObject CreateWeapon = null;

            switch (TypeWeapon)
            {
                case WeaponName.AK12:
                    CreateWeapon = gameObjects[0];
                    if (WeaponNumber == 1)   // 1번 무기
                    {
                        // 총에 뒤에 있을 때
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.014f, 0.007f, -0.002f), new Quaternion(-25.446f, -85.708f, 1.14f, 0f));
                    }
                    else if (WeaponNumber == 2)  // 2번 무기
                    {
                        // 총에 뒤에 있을 떄
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(0.014f, 0.007f, -0.002f), new Quaternion(-25.662f, -91.36f, 0.904f, 0f));
                    }
                    // 손에 총을 잡을 떄
                    CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.288f, 0.103f, 0.025f), new Quaternion(0f, 98.332f, 0f, 0f));
                    break;
                case WeaponName.AK74:
                    CreateWeapon = gameObjects[1];
                    if (WeaponNumber == 1)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.123f, 0.073f, 0.011f), new Quaternion(-26.946f, -88.329f, -4.571f, 0f));
                    }
                    else if (WeaponNumber == 2)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.145f, 0.034f, -0.022f), new Quaternion(-32.049f, -92.47f, 6.13f, 0f));
                    }
                    CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.1819f, 0.0747f, 0.0104f), new Quaternion(0f, 100.581f, 0f, 0f));
                    break;
                case WeaponName.G3A4:
                    CreateWeapon = gameObjects[2];
                    if (WeaponNumber == 1)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.187f, 0.126f, 0.033f), new Quaternion(-25.519f, -86.9f, 1.829f, 0f));
                    }
                    else if (WeaponNumber == 2)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.201f, 0.094f, -0.026f), new Quaternion(-24.433f, -92.924f, -6.359f, 0f));
                    }
                    CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.0777f, 0.1248f, -0.0199f), new Quaternion(0f, 96.547f, 0f, 0f));
                    break;
                case WeaponName.TAR21:
                    CreateWeapon = gameObjects[3];
                    if (WeaponNumber == 1)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.075f, 0.022f, -0.017f), new Quaternion(-30.956f, -82.883f, -3.388f, 0f));
                    }
                    else if (WeaponNumber == 2)
                    {
                        CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.053f, 0.009f, -0.008f), new Quaternion(-29.8f, -92.785f, 0f, 0f));
                    }
                    CreateWeapon.transform.SetLocalPositionAndRotation(new Vector3(-0.328f, 0.061f, 0.032f), new Quaternion(0f, 99.727f, 0f, 0f));
                    break;

                //보조 무기류
                case WeaponName.Glock_17:
                    CreateWeapon = gameObjects[4];
                    break;
                case WeaponName.Tec_9:
                    CreateWeapon = gameObjects[5];
                    break;
                case WeaponName.Deagle:
                    CreateWeapon = gameObjects[6];
                    break;
            }
        }
    }
}
