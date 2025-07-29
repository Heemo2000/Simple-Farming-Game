using UnityEngine;
using Game.Core;
using Game.CharacterHandling;
using Game.StateMachineHandling;
using Game.CameraHandling;
using Unity.Cinemachine;
using Game.UI;

namespace Game.GameplayHandling
{
    public class NormalState : IState
    {
        private Human playerHuman;
        private CameraManager cameraManager;
        private CinemachineCamera playerCamera;
        private UIManager uiManager;
        private Page mainPanel;
        private GameInput input;
        private GameObject gridGraphic;
        private BuyManager buyManager;


        private Vector2 moveInput;
        public NormalState(GameplayController controller)
        {
            this.playerHuman = controller.PlayerHuman;
            this.cameraManager = controller.CameraManager;
            this.playerCamera = controller.PlayerCamera;
            this.uiManager = controller.UIManager;
            this.mainPanel = controller.MainPanel;
            this.input = controller.GameInput;
            this.gridGraphic = controller.GridGraphic;
            this.buyManager = controller.BuyManager;
        }

        public void OnEnter()
        {
            this.cameraManager.MakeCameraImportant(this.playerCamera);
            this.mainPanel.exitOnNewPagePush = true;
            if(this.uiManager.PageCount > 1)
            {
                this.uiManager.PopPage();
            }
            this.gridGraphic.SetActive(false);
            this.buyManager.LoadData();
        }
        public void OnUpdate()
        {
            Debug.Log("Now in Normal State.");
            moveInput = input.GetMoveInput();
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
            moveInput = Vector2.zero;
            playerHuman.HandleMovement(moveInput);
        }
        
    }
}
