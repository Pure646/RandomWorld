using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public enum EffectType
    {
        None,

        Impact_MonsterA,
        Impact_MonsterB,
        Impact_MonsterC,

        Impact_Ground,

        Impact_ConcreteWall,
        Impact_WoodWall
    }
    [System.Serializable]
    public class EffectData
    {
        public EffectType effectType;
        public GameObject effectPrefab;
        public float lifeTime;
    }
    public class EffectCollect : MonoBehaviour
    {
        public static EffectCollect instance { get; private set; } = null;
        public List<EffectData> effectDataList = new List<EffectData>();

        private void Awake()
        {
            instance = this;
        }

        public void CreateEffect(EffectType type, Vector3 position, Quaternion rotation)
        {
            var targetEffectData = effectDataList.Find(x => x.effectType == type);  //?
            if (targetEffectData == null)
                return;

            var newEffect = Instantiate(targetEffectData.effectPrefab, position, rotation);
            newEffect.gameObject.SetActive(true);
            Destroy(newEffect, targetEffectData.lifeTime);
        }
    }
}
