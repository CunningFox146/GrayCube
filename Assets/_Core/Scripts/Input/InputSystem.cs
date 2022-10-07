using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrayCube.Input
{
    public class InputSystem : MonoBehaviour, IInputSource
    {
        public event Action ClickPerformed;
        public event Action ClickCancelled;

        private GameplayInput _gameplayInput;

        private void Awake()
        {
            InitializeInputSystem();
        }

        private void OnEnable()
        {
            _gameplayInput.Enable();
        }

        private void OnDisable()
        {
            _gameplayInput.Disable();
        }

        private void OnDestroy()
        {
            _gameplayInput.Dispose();
        }

        public Vector2 GetClickPosition() => _gameplayInput.Gameplay.ClickPosition.ReadValue<Vector2>();
        private void OnClickPerformedHandler(InputAction.CallbackContext _) => ClickPerformed?.Invoke();
        private void OnClickCancelledHandler(InputAction.CallbackContext _) => ClickCancelled?.Invoke();

        private void InitializeInputSystem()
        {
            _gameplayInput = new();
            _gameplayInput.Gameplay.Click.performed += OnClickPerformedHandler;
            _gameplayInput.Gameplay.Click.canceled += OnClickCancelledHandler;
        }
    }
}
