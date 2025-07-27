using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class BuyingCropUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;

        public void SetPriceText(float price)
        {
            priceText.text = "$" + Mathf.FloorToInt(price).ToString();
        }
    }
}
