using UnityEngine;
using Game.Core;
using Game.StateMachineHandling;

namespace Game.PlacementHandling
{
    [System.Serializable]
    public class PlacementState : IState
    {
        private int selectedObjectIndex = -1;
        int id = -1;
        Grid grid;
        private PreviewSystem previewSystem;
        private ObjectsDataSO database;
        private GridData floorData, furnitureData;
        private ObjectPlacer objectPlacer;
        private GameObject gridVisualization;
        public PlacementState(int id,
                              Grid grid,
                              ObjectsDataSO database,
                              PreviewSystem previewSystem,
                              GridData floorData,
                              GridData furnitureData,
                              ObjectPlacer objectPlacer,
                              GameObject gridVisualization)
        {
            this.id = id;
            this.grid = grid;
            this.database = database;
            this.previewSystem = previewSystem;
            this.floorData = floorData;
            this.furnitureData = furnitureData;
            this.objectPlacer = objectPlacer;
            this.gridVisualization = gridVisualization;

            selectedObjectIndex = database.objectsData.FindIndex((data) => data.ID == id);
            if (selectedObjectIndex > -1)
            {
                gridVisualization.SetActive(true);
                previewSystem.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError($"No ID Found: {id}");
            }
        }



        public void EndState()
        {
            if(previewSystem != null)
            {
                previewSystem.gameObject.SetActive(false);
            }
        }

        public void OnAction(Vector3Int gridPosition)
        {
            if(selectedObjectIndex < 0)
            {
                return;
            }
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if (placementValidity == false)
            {
                return;
            }

            Vector3 worldPosition = grid.CellToWorld(gridPosition);
            int objectIndex = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab, worldPosition);

            var objectData = database.objectsData[selectedObjectIndex];
            GridData selectedData = objectData.ID == 0 ?
                                    floorData :
                                    furnitureData;

            selectedData.AddObjectAt(gridPosition, objectData.Size, objectData.ID, objectIndex);
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            Vector3 worldPosition = grid.CellToWorld(gridPosition);

            previewSystem.SetPreview(worldPosition, 
                                     database.objectsData[selectedObjectIndex].ID, 
                                     placementValidity);
        }

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        {
            GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
                                                                             floorData :
                                                                             furnitureData;

            bool result = selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
            Debug.Log($"Can place :" + result);
            return result;
        }

        
    }
}
