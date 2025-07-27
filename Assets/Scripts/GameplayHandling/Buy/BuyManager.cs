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

        private Dictionary<CropType, BuyCropData> buyDataDict;
        private float currentMoney = 0.0f;

        public UnityEvent<CropType, float> OnCropMoneyDataModified;
        public UnityEvent<float> OnTotalMoneyModified;

        public void LoadData()
        {
            OnTotalMoneyModified?.Invoke(currentMoney);
            foreach (var pair in buyDataDict)
            {
                CropType cropType = pair.Key;
                BuyCropData data = pair.Value;
                OnCropMoneyDataModified?.Invoke(cropType, data.price);
            }
        }

        private void Awake()
        {
            buyDataDict = new Dictionary<CropType, BuyCropData>();
        }

        private void Start()
        {
            currentMoney = initialMoneyAmount;
            for (int i = 0; i < buyCropData.Length; i++)
            {
                CropType current = buyCropData[i].cropType;
                buyDataDict.TryAdd(current, buyCropData[i]);
            }
        }
    }
}
