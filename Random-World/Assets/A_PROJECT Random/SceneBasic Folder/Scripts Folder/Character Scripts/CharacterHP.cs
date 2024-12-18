using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    [CreateAssetMenu(fileName = "Character HP", menuName = "RandomWorld/Character Stat Data/Character HP")]
    public class CharacterHP : ScriptableObject
    {
        [Range(0f, 100f)]
        public float MaxHP = 10f;
    }
}
