using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class TotalMoneyUI : MonoBehaviour
    {
        private TMP_Text totalMoneyAmountUI;

        public void SetTotalAmount(float amount)
        {
            totalMoneyAmountUI.text = "$" + Mathf.FloorToInt(amount).ToString();
        }

        private void Awake()
        {
            totalMoneyAmountUI = GetComponent<TMP_Text>();
        }
    }
}
