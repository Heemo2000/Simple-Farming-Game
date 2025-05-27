using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class PlacementData
    {
        public List<Vector3Int> occupiedPositions;
        public int ID { get; private set; }
        public int PlacedObjectIndex { get; private set; }

        public PlacementData(List<Vector3Int> occupiedPositions,
                             int iD,
                             int placedObjectIndex)
        {
            this.occupiedPositions = occupiedPositions;
            ID = iD;
            PlacedObjectIndex = placedObjectIndex;
        }

        
    }
}
