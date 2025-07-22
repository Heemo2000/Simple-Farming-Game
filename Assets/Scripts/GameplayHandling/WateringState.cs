using System.Collections.Generic;
using UnityEngine;
using Game.CharacterHandling;
using Game.Core;
using Game.StateMachineHandling;
using Game.UI;
namespace Game.GameplayHandling
{
    public class WateringState : IState
    {
        private Human playerHuman;
        private GameInput gameInput;
        private Vector2 moveInput;
        private GameplayController controller;
        private UIManager uiManager;
        private Page wateringPanel;
        private HashSet<Crop> visitedCrops;
        private float currentTime = 0.0f;
        private Collider[] detectedColliders;
        
        public WateringState(GameplayController controller)
        {
            this.controller = controller;
            this.playerHuman = controller.PlayerHuman;
            this.gameInput = controller.GameInput;
            this.uiManager = controller.UIManager;
            this.wateringPanel = controller.WateringPanel;
            this.visitedCrops = new HashSet<Crop>(new CropComparer());
            this.detectedColliders = new Collider[Constants.MAX_COLLIDER_COUNT];
        }

        public void OnEnter()
        {
            this.uiManager.PushPage(this.wateringPanel);
        }
        public void OnUpdate()
        {
            Debug.Log("Now in Watering State.");
            moveInput = gameInput.GetMoveInput();
            if(currentTime < controller.WateringInterval)
            {
                currentTime += Time.deltaTime;
                return;
            }
            this.visitedCrops.Clear();
            currentTime = 0.0f;
            int count = Physics.OverlapSphereNonAlloc(this.playerHuman.transform.position, this.controller.WateringRadius, detectedColliders, this.controller.CropLayerMask.value);
            //Debug.Log("Crop count: " + count);
            for (int i = 0; i < count; i++)
            {
                Crop crop = detectedColliders[i].GetComponent<Crop>();
                if(visitedCrops.Contains(crop))
                {
                    //Debug.Log("Found same crop");
                    continue;
                }

                visitedCrops.Add(crop);
                crop.Grow();
            }
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
            this.uiManager.PopPage();
        }

    }
}
