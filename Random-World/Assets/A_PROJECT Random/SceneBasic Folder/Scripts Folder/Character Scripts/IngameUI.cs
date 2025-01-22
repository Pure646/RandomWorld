using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RandomWorld
{
    public class IngameUI : MonoBehaviour
    {
        public static IngameUI Instance { get; private set; }

        public GameObject weaponHolder;
        private GameObject weapon;
        private WeaponBase weaponBase;
        private CharacterBase character;

        public Image hpBar;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI ammoText;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            weaponBase = weaponHolder.GetComponentInChildren<WeaponBase>();
        }

        private void Update()
        {
            if(weaponBase == null)
            {
                weaponBase = weaponHolder.GetComponentInChildren<WeaponBase>();
            }
            else
            {
                SetAmmo(weaponBase.bullet_remain, weaponBase.remain_Max_bullet);
            }
        }
        public void SetHP(float current, float max)
        {
            hpBar.fillAmount = current / max;

            if(hpBar.fillAmount > 0.55 && hpBar.fillAmount <= 1)
            {
                hpBar.color = Color.green;
            }
            else if(hpBar.fillAmount <= 0.55 && hpBar.fillAmount > 0.25)
            {
                hpBar.color = Color.yellow;
            }
            else if (hpBar.fillAmount <= 0.25 && hpBar.fillAmount >= 0)
            {
                hpBar.color = Color.red;
            }

            hpText.text = $"{current} / {max}";
        }

        public void SetAmmo(int current, int max)
        {
            ammoText.text = $"{current} / {max}";
            if(max == 0)
            {
                ammoText.enabled = false;
            }
            else
            {
                ammoText.enabled = true;
            }
        }
    }
}
