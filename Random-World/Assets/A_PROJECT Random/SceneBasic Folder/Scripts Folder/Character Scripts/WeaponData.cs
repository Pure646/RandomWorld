using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "RandomWorld/Weapon/Create Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField] public Vector3 RightHandPosition { get; set; }
        [field: SerializeField] public Vector3 RightHandRotation { get; set; }
        [field: SerializeField] public Vector3 LeftHandPosition { get; set; }
        [field: SerializeField] public Vector3 LeftHandRotation { get; set; }
    }
}
