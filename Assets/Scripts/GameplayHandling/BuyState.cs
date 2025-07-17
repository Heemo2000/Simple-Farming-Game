using UnityEngine;
using Game.StateMachineHandling;
namespace Game.GameplayHandling
{
    public class BuyState : IState
    {
        private CropSelector cropSelector;
        public BuyState(CropSelector cropSelector)
        {
            this.cropSelector = cropSelector;
        }
        public void OnEnter()
        {
            
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

        }
    }
}
