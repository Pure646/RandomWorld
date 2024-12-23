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

        [field: SerializeField] public Vector3 OffsetPosition { get; private set; }
        [field: SerializeField] public Vector3 OffsetRotation { get; private set; }


        [field: SerializeField] public Vector3 HandOffsetPosition { get; private set; }
        [field: SerializeField] public Vector3 HandOffsetRotation { get; private set; }


        public WeaponType weaponType;

    }
}
