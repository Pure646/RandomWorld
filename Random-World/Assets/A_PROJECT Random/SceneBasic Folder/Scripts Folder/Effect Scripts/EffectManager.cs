using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    [System.Serializable]
    public class EffectData
    {
        public string effectID;
        public GameObject prefab;
        public float lifeTime;
    }

    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance { get; private set; }

        public List<EffectData> effectDatas = new List<EffectData>();


        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SpawnEffect(string effectId, Vector3 position, Quaternion rotation)
        {
            var targetEffectData = effectDatas.Find(x => x.effectID.Equals(effectId));      // Equals , == : Equals가 성능이 좋다.
            if (targetEffectData != null)
            {
                var newEffect = Instantiate(targetEffectData.prefab, position, rotation);
                newEffect.gameObject.SetActive(true);
                Destroy(newEffect, targetEffectData.lifeTime);
            }
        }
    }
}
