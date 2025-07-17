using System;
using UnityEngine;
using Game.Input;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public class GameInput : MonoBehaviour
    {
        [SerializeField] private Camera lookCamera;
        [Min(100.0f)]
        [SerializeField] private float maxCheckDistance = 100.0f;
        [SerializeField] private LayerMask checkLayerMask;

        private GameControls gameControls;
        public Action OnStop;
        public Action<Vector2> OnMove;
        public Action<Vector3> OnPositionClicked;


        public Vector2 GetMoveInput()
        {
            return gameControls.PlayerActionMap.Move.ReadValue<Vector2>();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            OnMove?.Invoke(input);
        }

        private void OnMoveEnd(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(Vector2.zero);
            OnStop?.Invoke();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            Vector2 screenPos = gameControls.PlayerActionMap.PointerPos.ReadValue<Vector2>();
            Ray ray = lookCamera.ScreenPointToRay(screenPos);

            if(Physics.Raycast(ray, out RaycastHit hit, maxCheckDistance, checkLayerMask.value))
            {
                OnPositionClicked?.Invoke(hit.point);
            }
        }


        private void Awake()
        {
            gameControls = new GameControls();
            gameControls.Enable();
            gameControls.PlayerActionMap.Move.performed += OnMovePerformed;
            gameControls.PlayerActionMap.Move.canceled += OnMoveEnd;
            gameControls.PlayerActionMap.Click.started += OnClick;
        }

        private void OnDestroy()
        {
            gameControls.Disable();
            gameControls.PlayerActionMap.Move.performed -= OnMovePerformed;
            gameControls.PlayerActionMap.Move.canceled -= OnMoveEnd;
            gameControls.PlayerActionMap.Click.started -= OnClick;
        }
    }
}
