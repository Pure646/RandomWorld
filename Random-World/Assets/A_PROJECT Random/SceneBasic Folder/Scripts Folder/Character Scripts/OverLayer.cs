using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RandomWorld
{
    public class OverLayer : MonoBehaviour
    {
        public LayerMask DropWeeapon;
        public Transform Character;
        private Collider[] DropWeaponOverlapped;

        public float rad = 2.5f;

        private void Update()
        {
            DropWeaponLayer();
        }
        private void DropWeaponLayer()
        {
            if(DropWeaponOverlapped != null && DropWeaponOverlapped.Length > 0)
            {
                for (int i = 0; i < DropWeaponOverlapped.Length; i++)
                {
                    var dropWeaponText = DropWeaponOverlapped[i].GetComponentInChildren<TextMeshPro>();
                    dropWeaponText.enabled = false;
                }
            }
            DropWeaponOverlapped = Physics.OverlapSphere(Character.position, rad, DropWeeapon, QueryTriggerInteraction.Ignore);

            for(int i = 0;i < DropWeaponOverlapped.Length;i++)
            {
                var dropWeaponText = DropWeaponOverlapped[i].GetComponentInChildren<TextMeshPro>();
                dropWeaponText.enabled = true;
            }
        }

    }
}
