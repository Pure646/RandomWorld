using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public interface IBotCharacter
    {
        public void HealthPoint(float damage);

        public void ManaPoint(float used_mana);
    }
}
