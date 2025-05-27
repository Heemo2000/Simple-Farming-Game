using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class GridData
    {
        private Dictionary<Vector3Int, PlacementData> placedObjects;
        private List<Vector3Int> cachedPositions;

        public GridData()
        {
            placedObjects = new();
            cachedPositions = new List<Vector3Int>();
        }

        public void AddObjectAt(Vector3Int gridPosition,
                                Vector2Int objectSize,
                                int id,
                                int placedObjectIndex)
        {
            List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
            PlacementData data = new PlacementData(positionsToOccupy, id, placedObjectIndex);

            foreach (var position in positionsToOccupy)
            {
                if(placedObjects.ContainsKey(position))
                {
                    Debug.LogError($"{position} already occupied!");
                    return;
                }
                placedObjects[position] = data;
            }
            cachedPositions.AddRange(positionsToOccupy);
        }

        public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
            foreach (var position in positionsToOccupy)
            {
                if(IsOverlapping(position)) //if (placedObjects.ContainsKey(position))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsOverlapping(Vector3Int position)
        {
            foreach(Vector3Int pos in cachedPositions)
            {
                if(pos.x == position.x && pos.y == position.y && pos.z == position.z)
                {
                    return true;
                }
            }

            return false;
        }

        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> returnVal = new();
            for (int y = 0; y < objectSize.y; y++)
            {
                for (int x = 0; x < objectSize.x; x++)
                {
                    Vector3Int requiredPosition = gridPosition + new Vector3Int(x, y, 0);
                    returnVal.Add(requiredPosition);
                }
            }
            
            return returnVal;
        }

        public int GetRepresentationIndex(Vector3Int gridPosition)
        {
            if(placedObjects.ContainsKey(gridPosition) == false)
            {
                return -1;
            }

            return placedObjects[gridPosition].PlacedObjectIndex;
        }

        public void RemoveObjectAt(Vector3Int gridPosition)
        {
            foreach (var pos in placedObjects[gridPosition].occupiedPositions)
            {
                placedObjects.Remove(pos);
                cachedPositions.Remove(pos);
            }
        }
    }
}
