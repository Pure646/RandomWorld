using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public interface IDamage
    {
        public void ApplyDamage(out float Health);
        public void ApplyHeal(out float Heal);
    }
}
