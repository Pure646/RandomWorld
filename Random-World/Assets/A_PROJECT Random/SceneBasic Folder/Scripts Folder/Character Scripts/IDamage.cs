using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public interface IDamage
    {
        public void ApplyDamage(float damage);
        public void ApplyHeal(float Heal);
    }
}
