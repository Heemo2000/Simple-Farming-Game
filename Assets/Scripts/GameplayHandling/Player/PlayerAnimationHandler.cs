using UnityEngine;
using Game.Core;

namespace Game.GameplayHandling
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private int moveInputHash = -1;

        public void HandleMoveAnimInput(float moveInput)
        {
            animator.SetFloat(moveInputHash, moveInput, Time.deltaTime, Time.deltaTime);
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            moveInputHash = Animator.StringToHash(Constants.MOVE_INPUT_ANIM_PARAM);
        }
    }
}
