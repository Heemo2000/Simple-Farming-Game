using UnityEngine;
using UnityEngine.UI;
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
        private Inventory inventory;
        private GameObject gridGraphic;
        private PlayerController playerController;
        
        public PlantingState(GameplayController controller)
        {
            this.cropManager = controller.CropManager;
            this.cropSelector = controller.PlantingCropSelector;
            this.cameraManager = controller.CameraManager;
            this.topDownCamera = controller.TopViewCamera;
            this.uiManager = controller.UIManager;
            this.mainPanel = controller.MainPanel;
            this.plantingPanel = controller.PlantingPanel;
            this.gameInput = controller.GameInput;
            this.inventory = controller.Inventory;
            this.gridGraphic = controller.GridGraphic;
            this.playerController = controller.PlayerHuman;
        }

        public void OnEnter()
        {
            this.gameInput.OnPositionClicked += SpawnSelectedCrop;
            this.cameraManager.MakeCameraImportant(this.topDownCamera);
            this.mainPanel.exitOnNewPagePush = true;
            this.uiManager.PushPage(this.plantingPanel);
            this.inventory.LoadData();
            this.gridGraphic.SetActive(true);
        }

        public void OnUpdate()
        {
            this.playerController.HandleMovement(Vector2.zero);
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
            this.gridGraphic.SetActive(false);
            this.uiManager.PopPage();
        }

        private void SpawnSelectedCrop(Vector3 position)
        {
            if(cropSelector.IsInsideButton)
            {
                return;
            }

            CropType selectedCropType = cropSelector.SelectedCropType;
            int count = this.inventory.GetCropCount(selectedCropType);
            
            if(count > 0)
            {
                cropManager.SpawnCrop(cropSelector.SelectedCropType, position);
                this.inventory.DecreaseCropCount(selectedCropType);
            }
        }
    }
}
