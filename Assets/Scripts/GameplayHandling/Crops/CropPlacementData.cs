using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayHandling
{
    public class CropPlacementData
    {
        private CropType type = CropType.None;
        private Vector3Int position;

        public CropType Type { get => type; }
        public Vector3Int Position { get => position; }

        public CropPlacementData(CropType type, Vector3Int position)
        {
            this.type = type;
            this.position = position;
        }
    }
}
