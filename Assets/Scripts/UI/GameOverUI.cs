using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]private TMP_Text totalMoneyRemainingText;

        public void SetTotalMoney(float amount)
        {
            totalMoneyRemainingText.text = "$" + Mathf.FloorToInt(amount).ToString();
        }
    }
}
