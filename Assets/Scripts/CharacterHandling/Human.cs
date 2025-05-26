using UnityEngine;

namespace Game.CharacterHandling
{
    public class Human : MonoBehaviour, ICharacter
    {
        
        [Header("Movement Settings: ")]
        [SerializeField] private float originalSpeed = 10.0f;
        
        [Range(1.2f, 5.0f)]
        [SerializeField] private float maxSpeedMultiplier = 2.0f;
        
        [Min(5.0f)]
        [SerializeField] private float moveTransitioningSpeed = 20.0f;

        [Min(5.0f)]
        [SerializeField] private float rotateTransitioningSpeed = 20.0f;

        [Header("Gravity Settings: ")]
        [Min(1.0f)]
        [SerializeField] private float gravity = 10.0f;
        
        private float velocityY = 0.0f;
        private float currentVelocityY = 0.0f;
        private float nextVelocityY = 0.0f;
        private float newVelocityY = 0.0f;

        private bool isMoving = false;
        private float currentSpeed = 0.0f;
        private float targetSpeed = 0.0f;

        private CharacterController controller;
        public void HandleMovement(Vector2 moveInput)
        {
            if (controller.isGrounded)
            {
                Debug.Log("On Ground");
                velocityY = 0.0f;
            }
            else
            {
                currentVelocityY = velocityY;
                nextVelocityY = velocityY - gravity * Time.fixedDeltaTime;
                newVelocityY = (currentVelocityY + nextVelocityY) / 2.0f;
                velocityY = newVelocityY;
            }

            isMoving = moveInput.x != 0.0f || moveInput.y != 0.0f;
            targetSpeed = (isMoving) ? originalSpeed * maxSpeedMultiplier : 0.0f;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, moveTransitioningSpeed * Time.fixedDeltaTime);

            Vector3 horizontalVector = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized * currentSpeed;
            Vector3 upVector = Vector3.up * velocityY;

            controller.Move((horizontalVector + upVector) * Time.fixedDeltaTime);

            Quaternion requiredRotation = (isMoving) ? Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(horizontalVector), rotateTransitioningSpeed * Time.fixedDeltaTime) : transform.rotation;
            transform.rotation = requiredRotation;
        }

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
    }    
}
