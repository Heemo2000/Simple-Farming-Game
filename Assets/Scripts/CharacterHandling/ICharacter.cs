using UnityEngine;

namespace Game.CharacterHandling
{
    public interface ICharacter
    {
        void HandleMovement(Vector2 moveInput);        
    }
}
