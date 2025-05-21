using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] public float airborneSpeedMultiplier = .5f;

        private Character _character;
        private Coroutine _jumpCoroutine;
        private IPlayerState _currentState;

        private void Awake()
        {
            _character = GetComponent<Character>();
            SetState(new GroundedState());
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }

            if (jumpInput?.action != null)
            {
                jumpInput.action.performed += HandleJumpInput;
            }
        }

        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started -= HandleMoveInput;
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }

            if (jumpInput?.action != null)
            {
                jumpInput.action.performed -= HandleJumpInput;
            }
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            var input = ctx.ReadValue<Vector2>();
            _currentState?.HandleMove(input);
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _currentState?.HandleJump();
        }

        private void OnCollisionEnter(Collision other)
        {
            _currentState?.HandleCollision(other);
        }

        public void StartJump()
        {
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);

            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        public void SetState(IPlayerState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter(this);
        }

        public Character Character => _character;
      
    }
}
