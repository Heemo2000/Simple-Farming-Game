using UnityEngine;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class InventoryData
    {
        [SerializeField] private CropType cropType;
        [SerializeField] private int count;

        public CropType CropType { get => cropType; }
        public int Count { get => count; }
    }
}
