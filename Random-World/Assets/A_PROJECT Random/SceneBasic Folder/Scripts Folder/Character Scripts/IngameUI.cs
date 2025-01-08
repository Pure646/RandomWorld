using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RandomWorld
{
    public class IngameUI : MonoBehaviour
    {
        public Image hpBar;
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI ammoText;
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
        }
    }
}
