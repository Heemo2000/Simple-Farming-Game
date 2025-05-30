using Game.Core;
using Game.StateMachineHandling;
using UnityEngine;

namespace Game.GameplayHandling
{
    public class PlantingState : IState
    {
        private GameInput gameInput;

        public PlantingState(GameInput gameInput)
        {
            this.gameInput = gameInput;
        }

        public void OnEnter()
        {
            
        }

        public void OnUpdate()
        {
            Debug.Log("Now, in planting mode.");
        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {
            
        }


        public void OnExit()
        {
            
        }

        
    }
}
