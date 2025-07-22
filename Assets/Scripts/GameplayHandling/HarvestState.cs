using Game.CharacterHandling;
using Game.Core;
using Game.StateMachineHandling;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameplayHandling
{
    public class HarvestState : IState
    {
        private Human playerHuman;
        private GameInput gameInput;
        private Vector2 moveInput = Vector2.zero;
        private GameplayController controller;
        private UIManager uiManager;
        private Page harvestingPanel;

        private HashSet<Crop> visitedCrops;
        private float currentTime = 0.0f;
        private Collider[] detectedColliders;

        public HarvestState(GameplayController controller)
        {
            this.controller = controller;
            this.playerHuman = controller.PlayerHuman;
            this.gameInput = controller.GameInput;
            this.uiManager = controller.UIManager;
            this.harvestingPanel = controller.HarvestingPanel;
            this.visitedCrops = new HashSet<Crop>(new CropComparer());
            this.detectedColliders = new Collider[Constants.MAX_COLLIDER_COUNT];
        }
        public void OnEnter()
        {
            this.uiManager.PushPage(this.harvestingPanel);
        }

        public void OnUpdate()
        {
            Debug.Log("Now in Harvest state");
            moveInput = gameInput.GetMoveInput();
            if (currentTime < controller.HarvestingInterval)
            {
                currentTime += Time.deltaTime;
                return;
            }

            this.visitedCrops.Clear();
            currentTime = 0.0f;
            int count = Physics.OverlapSphereNonAlloc(this.playerHuman.transform.position, this.controller.HarvestingRadius, detectedColliders, this.controller.CropLayerMask.value);
            //Debug.Log("Crop count: " + count);
            for (int i = 0; i < count; i++)
            {
                Crop crop = detectedColliders[i].GetComponent<Crop>();
                if (visitedCrops.Contains(crop))
                {
                    //Debug.Log("Found same crop");
                    continue;
                }

                visitedCrops.Add(crop);
                crop.Harvest();
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
            uiManager.PopPage();
        }
    }
}
