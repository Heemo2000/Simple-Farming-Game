using UnityEngine;
using Game.Core;
using Game.CharacterHandling;
using Game.StateMachineHandling;

namespace Game.GameplayHandling
{
    public class PlayerState : IState
    {
        private Human playerHuman;
        private GameInput input;
        private Vector2 moveInput;
        public PlayerState(Human playerHuman, GameInput input)
        {
            this.playerHuman = playerHuman;
            this.input = input;
        }

        public void OnEnter()
        {
            
        }
        public void OnUpdate()
        {
            moveInput = input.GetMoveInput();
            Debug.Log("Now in Player State.");
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
