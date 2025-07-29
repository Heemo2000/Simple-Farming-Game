using UnityEngine;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class BuyCropData
    {
        public CropType cropType;
        [Min(0)]
        public float costPrice = 1;
        [Min(0)]
        public float sellPrice = 1;
    }
}
