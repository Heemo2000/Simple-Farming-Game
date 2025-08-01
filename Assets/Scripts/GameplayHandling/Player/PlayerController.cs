using UnityEngine;
using Game.CharacterHandling;
using System;

namespace Game.GameplayHandling
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem footParticleSystem;

        private Human human;
        private bool isMoving = false;

        public void HandleMovement(Vector2 moveInput)
        {
            human.HandleMovement(moveInput);
        }

        private void HandleFootEffects(Vector2 moveInput)
        {
            isMoving = moveInput.x != 0 || moveInput.y != 0;
            footParticleSystem.gameObject.SetActive(isMoving);
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
