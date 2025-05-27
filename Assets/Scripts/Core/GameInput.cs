using System;
using UnityEngine;
using Game.Input;

namespace Game.Core
{
    public class GameInput : MonoBehaviour
    {
        private GameControls gameControls;

        public Vector2 GetMoveInput()
        {
            return gameControls.PlayerActionMap.Move.ReadValue<Vector2>();
        }

        private void Awake()
        {
            gameControls = new GameControls();
            gameControls.Enable();
        }

        private void OnDestroy()
        {
            gameControls.Disable();
        }
    }
}
