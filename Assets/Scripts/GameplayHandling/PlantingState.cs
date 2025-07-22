using UnityEngine;
using Game.Core;
using Game.StateMachineHandling;
using Game.CameraHandling;
using Unity.Cinemachine;
using Game.UI;



namespace Game.GameplayHandling
{
    public class PlantingState : IState
    {
        private CropManager cropManager;
        private CropSelector cropSelector;
        private CameraManager cameraManager;
        private CinemachineCamera topDownCamera;
        private UIManager uiManager;
        private Page mainPanel;
        private Page plantingPanel;
        private GameInput gameInput;

        public PlantingState(CropManager cropManager, CropSelector cropSelector, CameraManager cameraManager, CinemachineCamera topDownCamera, UIManager uiManager, Page mainPanel, Page plantingPanel, GameInput gameInput)
        {
            this.cropManager = cropManager;
            this.cropSelector = cropSelector;
            this.cameraManager = cameraManager;
            this.topDownCamera = topDownCamera;
            this.uiManager = uiManager;
            this.mainPanel = mainPanel;
            this.plantingPanel = plantingPanel;
            this.gameInput = gameInput;
        }

        public void OnEnter()
        {
            this.gameInput.OnPositionClicked += SpawnSelectedCrop;
            this.cameraManager.MakeCameraImportant(this.topDownCamera);
            this.mainPanel.exitOnNewPagePush = true;
            this.uiManager.PushPage(this.plantingPanel);
        }

        public void OnUpdate()
        {
            
        }

        public void OnFixedUpdate()
        {

        }

        public void OnLateUpdate()
        {
            
        }

        public void OnExit()
        {
            this.gameInput.OnPositionClicked -= SpawnSelectedCrop;
        }

        private void SpawnSelectedCrop(Vector3 position)
        {
            cropManager.SpawnCrop(cropSelector.SelectedCropType, position);
        }
    }
}
