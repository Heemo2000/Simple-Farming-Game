using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RemovingState
    {
        private int gameObjectIndex = -1;
        Grid grid;
        private PreviewSystem previewSystem;
        private GridData floorData, furnitureData;
        private ObjectPlacer objectPlacer;
        private GameObject gridVisualization;

        public RemovingState(Grid grid,
                             PreviewSystem previewSystem,
                             GridData floorData,
                             GridData furnitureData,
                             ObjectPlacer objectPlacer,
                             GameObject gridVisualization)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.floorData = floorData;
            this.furnitureData = furnitureData;
            this.objectPlacer = objectPlacer;
            this.gridVisualization = gridVisualization;

            previewSystem.gameObject.SetActive(false);
        }


        public void EndState()
        {
            
        }

        public void OnAction(Vector3Int gridPosition)
        {
            GridData selectedData = null;
            if(furnitureData.CanPlaceObjectAt(gridPosition, Vector2Int.one))
            {
                selectedData = furnitureData;
            }
            else if(floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one))
            {
                selectedData = floorData;
            }

            if(selectedData == null)
            {
                Debug.Log("Cannot find selected data");
            }
            else
            {
                Debug.Log("Selected data: " + selectedData.ToString());

                gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
                if(gameObjectIndex == -1)
                {
                    return;
                }

                selectedData.RemoveObjectAt(gridPosition);
                objectPlacer.RemoveObjectAt(gameObjectIndex);
            }
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            
        }
    }
}
