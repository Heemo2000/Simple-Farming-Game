using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPlacer : MonoBehaviour
    {
        private List<GameObject> placedGameObjects = new();

        public int PlaceObject(GameObject prefab, Vector3 position)
        {
            GameObject placeable = Instantiate(prefab, position, Quaternion.identity);
            placedGameObjects.Add(placeable);

            return placedGameObjects.Count - 1;
        }

        public void RemoveObjectAt(int gameObjectIndex)
        {
            if(placedGameObjects.Count <= gameObjectIndex)
            {
                return;
            }
            Destroy(placedGameObjects[gameObjectIndex]);
            placedGameObjects.RemoveAt(gameObjectIndex);
        }
    }
}
