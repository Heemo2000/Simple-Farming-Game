using UnityEngine;
using Game.StateMachineHandling;
using Game.CharacterHandling;
using Game.Core;

namespace Game.GameplayHandling
{
    public class HarvestState : IState
    {
        private Human playerHuman;
        private GameInput gameInput;
        private Vector2 moveInput = Vector2.zero;

        public HarvestState(Human human, GameInput input)
        {
            this.playerHuman = human;
            this.gameInput = input;
        }
        public void OnEnter()
        {

        }

        public void OnUpdate()
        {
            Debug.Log("Now in Harvest state");
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
