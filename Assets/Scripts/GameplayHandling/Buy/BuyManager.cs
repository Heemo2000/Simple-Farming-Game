using Game.Core;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameplayHandling
{
    public class BuyManager : MonoBehaviour
    {
        [SerializeField] private float initialMoneyAmount = 100;
        [SerializeField] private BuyCropData[] buyCropData;
        [SerializeField] private Inventory inventory;

        private Dictionary<CropType, BuyCropData> buyDataDict;
        private float currentMoney = 0.0f;

        public UnityEvent<CropType, float> OnCropPriceDataModified;
        public UnityEvent<float> OnTotalMoneyModified;


        public void BuyCrop(CropType cropType)
        {
            float price = buyDataDict[cropType].price;
            if (currentMoney >= price)
            {
                currentMoney -= price;
                OnTotalMoneyModified?.Invoke(currentMoney);
                inventory.IncreaseCropCount(cropType);
            }
        }

        public void LoadData()
        {
            OnTotalMoneyModified?.Invoke(currentMoney);
            foreach (var pair in buyDataDict)
            {
                CropType cropType = pair.Key;
                BuyCropData data = pair.Value;
                OnCropPriceDataModified?.Invoke(cropType, data.price);
            }
        }

        private void Awake()
        {
            buyDataDict = new Dictionary<CropType, BuyCropData>();
            currentMoney = initialMoneyAmount;
        }

        private void Start()
        {
            for (int i = 0; i < buyCropData.Length; i++)
            {
                CropType current = buyCropData[i].cropType;
                buyDataDict.TryAdd(current, buyCropData[i]);
            }
        }
    }
}
