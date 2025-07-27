using UnityEngine;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class CropPoolData
    {
        public Crop prefab;
        [Min(1)]
        public int count = 1;
    }
}
