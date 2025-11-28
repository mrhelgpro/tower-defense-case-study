using UnityEngine;
using UnityEngine.InputSystem;

namespace TowerDefence.Gameplay
{
    public class PlayerCharacterController : MonoBehaviour
    {
        private Character _character;
        private PlayerActionsExample _playerActions;
        private bool _isMoving;
        private bool _isConstructed;
        
        public void Construct(Character character)
        {
            _character = character;
            _isConstructed = true;
            
            _playerActions = new PlayerActionsExample();
            _playerActions.Player.Jump.performed += OnAttackPerformed;
            _playerActions.Player.Enable();
        }

        private void OnDestroy()
        {
            if (_playerActions != null)
            {
                _playerActions.Player.Jump.performed -= OnAttackPerformed;
                _playerActions.Disable();
                _playerActions.Dispose();
            }
        }

        private void Update()
        {
            if (_isConstructed && _playerActions != null)
            {
                HandleMovement();
            }
        }

        private void HandleMovement()
        {
            var moveInput = _playerActions.Player.Move.ReadValue<Vector2>();
            var hasInput = moveInput.magnitude > 0.01f;
            if (hasInput)
            {
                var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
                _character.MoveToDirection(moveDirection);
                _isMoving = true;
            }
            else if (_isMoving)
            {
                _character.MoveStop();
                _isMoving = false;
            }
        }

        private void OnAttackPerformed(InputAction.CallbackContext context)
        {
            if (_isConstructed && _character != null)
            {
                _character.DealDamage();
            }
        }
    }
}