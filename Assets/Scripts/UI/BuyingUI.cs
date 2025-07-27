using Game.GameplayHandling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class BuyingUI : MonoBehaviour
    {
        [SerializeField] private BuyingUIData[] buyingUIData;

        private Dictionary<CropType, BuyingUIData> buyingUIDataDict;

        public void SetPriceText(CropType cropType, float price)
        {
            buyingUIDataDict[cropType].cropUI.SetPriceText(price);
        }

        private void Awake()
        {
            buyingUIDataDict = new Dictionary<CropType, BuyingUIData>();
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var data in buyingUIData)
            {
                CropType type = data.cropType;
                buyingUIDataDict.Add(type, data);
            }
        }
    }
}
