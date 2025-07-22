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


        private Vector2 moveInput;
        public NormalState(Human playerHuman, CameraManager cameraManager, CinemachineCamera playerCamera, UIManager uiManager, Page mainPanel, GameInput input)
        {
            this.playerHuman = playerHuman;
            this.cameraManager = cameraManager;
            this.playerCamera = playerCamera;
            this.uiManager = uiManager;
            this.mainPanel = mainPanel;
            this.input = input;
        }

        public void OnEnter()
        {
            this.cameraManager.MakeCameraImportant(this.playerCamera);
            this.mainPanel.exitOnNewPagePush = true;
            if(this.uiManager.PageCount > 1)
            {
                this.uiManager.PopPage();
            }
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
