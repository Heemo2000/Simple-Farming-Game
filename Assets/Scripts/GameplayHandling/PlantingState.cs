using Game.Core;
using Game.StateMachineHandling;
using Game.CharacterHandling;
using UnityEngine;


namespace Game.GameplayHandling
{
    public class PlantingState : IState
    {
        private Human playerHuman;
        private CropManager cropManager;
        private CropSelector cropSelector;
        private GameInput gameInput;
        private Vector2 moveInput = Vector2.zero;

        public PlantingState(Human playerHuman, CropManager cropManager, CropSelector cropSelector, GameInput gameInput)
        {
            this.playerHuman = playerHuman;
            this.cropManager = cropManager;
            this.cropSelector = cropSelector;
            this.gameInput = gameInput;
        }

        public void OnEnter()
        {
            playerHuman.HandleMovement(Vector2.zero);
            this.gameInput.OnPositionClicked += SpawnSelectedCrop;
        }

        public void OnUpdate()
        {
            Debug.Log("Now, in Planting State.");
            moveInput = gameInput.GetMoveInput();
        }

        public void OnFixedUpdate()
        {
            playerHuman.HandleMovement(moveInput);
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
