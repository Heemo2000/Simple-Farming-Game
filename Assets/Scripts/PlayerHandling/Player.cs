using UnityEngine;
using Game.Input;
using Game.CharacterHandling;

namespace Game.PlayerHandling
{
    public class Player : MonoBehaviour
    {
        private GameControls controls;
        private Human character;

        private void Awake()
        {
            controls = new GameControls();
            character = GetComponent<Human>();
        }

        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            controls.Enable();
        }

        private void FixedUpdate()
        {
            character.HandleMovement(controls.PlayerActionMap.Move.ReadValue<Vector2>());
        }

        private void OnDestroy()
        {
            controls.Disable();
        }
    }
}
