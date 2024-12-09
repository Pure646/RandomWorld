using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RandomWorld.CharacterBase;
using static RandomWorld.WeaponEquip;

namespace RandomWorld
{
    public enum MainWeaponType
    {
        None,

        Weapon_AK12,
        Weapon_AK74,
        Weapon_G3A4,
        Weapon_TAR21,
    }
    public enum SeconderyWeaponType
    {
        None,

        Weapon_Glock_17,
        Weapon_Tec9,
        Weapon_Deagle,
    }

    [System.Serializable]
    public class GameWeapon
    {
        public GameObject Weapon;
    }

    [System.Serializable]
    public class MainWeapon
    {
        public MainWeaponType mainWeaponType;
        public GameObject mainWeapon;
        public float EquipWeaponNumber;
    }

    [System.Serializable]
    public class SecondaryWeapon
    {
        public SeconderyWeaponType seconderyWeaponType;
        public GameObject secondaryWeapon;
        public float EquipWeaponNumber;
    }

    public class WeaponEquip : MonoBehaviour
    {
        public static WeaponEquip instance { get; private set; }
        public List<MainWeapon> MainWeaponList = new List<MainWeapon>();
        public List<SecondaryWeapon> SecondaryWeaponList = new List<SecondaryWeapon>();
        public List<GameWeapon> gameWeapons = new List<GameWeapon>();

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
        }
        private void Update()
        {

        }
        public void EquipWeapon(MainWeaponType type)
        {
            //if()
            //{
            //    GameObject Obj = MainWeaponList.Find(mainWeapon => );
            //    if (Obj == null)
            //        return;
            //}
        }
    }
}
