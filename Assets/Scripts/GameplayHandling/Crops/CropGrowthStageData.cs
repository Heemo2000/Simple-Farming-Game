using UnityEngine;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class CropGrowthStageData
    {
        [Min(1.0f)]
        public float time = 5.0f;
        public GameObject graphics;
    }
}
