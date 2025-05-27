using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField] private PreviewSystem previewSystem;
        [SerializeField] private Grid grid;
        [SerializeField] private ObjectsDataSO database;
        [SerializeField] private GameObject gridVisualization;
        [SerializeField] private ObjectPlacer objectPlacer;
        
        /*
        private IBuildingState buildingState;
        private GameInput gameInput;
        private bool isPointerOverUI = false;
        private GridData floorData;
        private GridData furnitureData;
        
        private Vector3Int lastGridPosition = Vector3Int.zero;
        public void StartPlacement(int id)
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new PlacementState(id,
                                                grid, 
                                                database, 
                                                previewSystem, 
                                                floorData, 
                                                furnitureData, 
                                                objectPlacer, 
                                                gridVisualization);
            gameInput.OnClicked += PlaceStructure;
            gameInput.OnExit += StopPlacement;
        }

        public void StartRemoving()
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new RemovingState(grid,
                                              previewSystem,
                                              floorData,
                                              furnitureData,
                                              objectPlacer,
                                              gridVisualization);
            gameInput.OnClicked += RemoveStructure;
            gameInput.OnExit += StopPlacement;
        }

        private void RemoveStructure()
        {
            if (isPointerOverUI)
            {
                return;
            }
            Vector3 mousePosition = gameInput.GetSelectedMousePosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            buildingState.OnAction(gridPosition);
        }

        private void PlaceStructure()
        {
            if(isPointerOverUI)
            {
                return;
            }

            Vector3 mousePosition = gameInput.GetSelectedMousePosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            buildingState.OnAction(gridPosition);
        }

        
        private void StopPlacement()
        {
            gridVisualization.SetActive(false);
            if(buildingState != null)
            {
                buildingState.EndState();
                gameInput.OnClicked -= PlaceStructure;
                gameInput.OnClicked -= RemoveStructure;
                gameInput.OnExit -= StopPlacement;
            }
            buildingState = null;
        }

        private void ShowClicked()
        {
            Debug.Log("Clicked");
        }

        private void Awake()
        {
            gameInput = GetComponent<GameInput>();
            floorData = new();
            furnitureData = new();
        }

        private void Start()
        {
            gameInput.OnClicked += ShowClicked;
            StopPlacement();
        }



        // Update is called once per frame
        void Update()
        {
            isPointerOverUI = gameInput.IsPointerOverUI();
            if(buildingState == null)
            {
                return;
            }
            Vector3 mousePosition = gameInput.GetSelectedMousePosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            if(lastGridPosition == gridPosition)
            {
                return;
            }

            buildingState.UpdateState(gridPosition);
            lastGridPosition = gridPosition;
        }

        private void OnDestroy()
        {
            gameInput.OnClicked -= ShowClicked;
        }
        */
    }
}
