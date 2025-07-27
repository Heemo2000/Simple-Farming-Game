using UnityEngine;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class InventoryData
    {
        public CropType cropType = CropType.None;
        [Min(0)]
        public int count = 1;
    }
}
