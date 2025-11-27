using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefence.Gameplay
{
    public class PlayerCharacterController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private Vector2 _moveInput;
        private bool _isMoving;

        private void Update()
        {
            HandleMovement();
            HandleAttack();
        }

        private void HandleMovement()
        {
            _moveInput = Vector2.zero;
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                _moveInput.y += keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ? 1 : 0;
                _moveInput.y += keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed ? -1 : 0;
                _moveInput.x += keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1 : 0;
                _moveInput.x += keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? -1 : 0;
            }
            
            var hasInput = _moveInput.magnitude > 0.01f;
            if (hasInput)
            {
                var moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
                _character.MoveToDirection(moveDirection);
                _isMoving = true;
            }
            else if (_isMoving)
            {
                _character.MoveStop();
                _isMoving = false;
            }
        }

        private void HandleAttack()
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            
            if ((keyboard != null && keyboard.spaceKey.wasPressedThisFrame) ||
                (mouse != null && mouse.leftButton.wasPressedThisFrame))
            {
                _character.DealDamage();
            }
        }
    }
}