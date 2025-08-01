using UnityEngine;
using Game.CharacterHandling;
using System;

namespace Game.GameplayHandling
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
        [SerializeField] private ParticleSystem footParticleSystem;

        private Human human;
        private bool isMoving = false;

        public void HandleMovement(Vector2 moveInput)
        {
            isMoving = moveInput.x != 0 || moveInput.y != 0;
            human.HandleMovement(moveInput);
            playerAnimationHandler.HandleMoveAnimInput(isMoving ? 1.0f : 0.0f);
        }

        private void HandleFootEffects(Vector2 moveInput)
        {
            var emission = footParticleSystem.emission;
            emission.enabled = isMoving;
        }

        private void Awake()
        {
            human = GetComponent<Human>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            human.OnMove.AddListener(HandleFootEffects);
        }
    }
}
