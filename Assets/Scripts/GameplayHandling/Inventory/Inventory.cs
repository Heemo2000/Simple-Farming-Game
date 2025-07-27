using Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameplayHandling
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryData[] inventoryData;
        private Dictionary<CropType, InventoryData> inventoryDataDict;

        public UnityEvent<CropType, int> OnCropDataModified;

        public void LoadData()
        {
            foreach(var pair in inventoryDataDict)
            {
                CropType cropType = pair.Key;
                InventoryData data = pair.Value;
                OnCropDataModified?.Invoke(cropType, data.count);
            }
        }

        public int GetCropCount(CropType cropType)
        {
            return inventoryDataDict[cropType].count;
        }

        public void IncreaseCropCount(CropType cropType)
        {
            inventoryDataDict[cropType].count++;
            OnCropDataModified?.Invoke(cropType, inventoryDataDict[cropType].count);
        }

        public void DecreaseCropCount(CropType cropType)
        {
            inventoryDataDict[cropType].count--;
            OnCropDataModified?.Invoke(cropType, inventoryDataDict[cropType].count);
        }

        private void Awake()
        {
            inventoryDataDict = new Dictionary<CropType, InventoryData>();
        }

        private void Start()
        {
            for (int i = 0; i < inventoryData.Length; i++)
            {
                CropType current = inventoryData[i].cropType;
                inventoryDataDict.TryAdd(current, inventoryData[i]);
            }
        }
    }
}
