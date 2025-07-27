using Game.GameplayHandling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryUIData[] inventoryUIData;

        private Dictionary<CropType, InventoryUIData> inventoryUIDict;
        
        public void SetCropCount(CropType cropType, int count)
        {
            inventoryUIDict[cropType].cropUI.SetCount(count);
        }

        private void Awake()
        {
            inventoryUIDict = new Dictionary<CropType, InventoryUIData>();
        }

        private void Start()
        {
            foreach (InventoryUIData data in inventoryUIData)
            {
                inventoryUIDict.Add(data.cropType, data);
            }
        }
    }
}
