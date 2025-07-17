using Game.CharacterHandling;
using Game.Core;
using Game.StateMachineHandling;
using UnityEngine;

namespace Game
{
    public class WateringState : IState
    {
        private Human playerHuman;
        private GameInput gameInput;

        private Vector2 moveInput;
        public WateringState(Human playerHuman, GameInput input)
        {
            this.playerHuman = playerHuman;
            this.gameInput = input;
        }

        public void OnEnter()
        {

        }
        public void OnUpdate()
        {
            Debug.Log("Now in Watering State.");
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

        }

    }
}
