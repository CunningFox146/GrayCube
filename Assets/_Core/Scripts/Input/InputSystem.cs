using GrayCube.Infrastructure;
using GrayCube.Moveable;
using GrayCube.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrayCube.Input
{
    public class InputSystem : MonoBehaviour, IInputSource
    {
        public event Action ClickPerformed;
        public event Action ClickCancelled;

        private ObjectMover _objectMover;
        private GameplayInput _gameplayInput;
        private ViewSystem _viewSystem;
        private bool _isClickDown;

        private void Awake()
        {
            InitializeInputSystem();
        }

        private void Start()
        {
            var systems = GameplaySystemsFacade.Instance;
            _viewSystem = systems.ViewSystem;
            _objectMover = new(this, systems.MainCamera);
        }

        private void Update()
        {
            _objectMover.Update();
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

        private void OnClickCancelledHandler(InputAction.CallbackContext _)
        {
            if (!_isClickDown) return;

            _isClickDown = false;
            ClickCancelled?.Invoke();
        }

        private void OnClickPerformedHandler(InputAction.CallbackContext _)
        {
            if (_viewSystem.IsPointerOnUI(GetClickPosition())) return;

            _isClickDown = true;
            ClickPerformed?.Invoke();
        }

        private void InitializeInputSystem()
        {
            _gameplayInput = new();
            _gameplayInput.Gameplay.Click.performed += OnClickPerformedHandler;
            _gameplayInput.Gameplay.Click.canceled += OnClickCancelledHandler;
        }
    }
}
