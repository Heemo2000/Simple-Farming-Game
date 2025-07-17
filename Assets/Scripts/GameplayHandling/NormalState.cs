using UnityEngine;
using Game.Core;
using Game.CharacterHandling;
using Game.StateMachineHandling;

namespace Game.GameplayHandling
{
    public class NormalState : IState
    {
        private Human playerHuman;
        private GameInput input;

        private Vector2 moveInput;
        public NormalState(Human playerHuman, GameInput input)
        {
            this.playerHuman = playerHuman;
            this.input = input;
        }

        public void OnEnter()
        {
            
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
            
        }
        
    }
}
