using System;
using UnityEngine;
using Game.Input;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public class GameInput : MonoBehaviour
    {
        private GameControls gameControls;
        public Action OnEnterPlantMode;
        public Action OnExitPlantMode;
        public Action<Vector2> OnMove;
        
        public Vector2 GetMoveInput()
        {
            return gameControls.PlayerActionMap.Move.ReadValue<Vector2>();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnExitStarted(InputAction.CallbackContext context)
        {
            OnExitPlantMode?.Invoke();
        }

        private void OnPlantingModeStarted(InputAction.CallbackContext context)
        {
            OnEnterPlantMode?.Invoke();
        }

        private void Awake()
        {
            gameControls = new GameControls();
            gameControls.Enable();
            gameControls.PlayerActionMap.PlantingMode.started += OnPlantingModeStarted;
            gameControls.PlayerActionMap.Exit.started += OnExitStarted;
            gameControls.PlayerActionMap.Move.performed += OnMovePerformed;
        }
        

        private void OnDestroy()
        {
            gameControls.Disable();
            gameControls.PlayerActionMap.PlantingMode.started -= OnPlantingModeStarted;
            gameControls.PlayerActionMap.Exit.started -= OnExitStarted;
            gameControls.PlayerActionMap.Move.performed -= OnMovePerformed;
        }
    }
}
