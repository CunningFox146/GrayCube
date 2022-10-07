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

        private void Awake()
        {
            InitializeInputSystem();
        }

        private void Start()
        {
            var systems = GameplaySystemsFacade.Instance;
            _viewSystem = systems.ViewSystem;
            _objectMover = new(this, systems.MainCamera);
            RegisterEventHandlers();
        }

        private void Update()
        {
            _objectMover.Update();
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
            _gameplayInput.Enable();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
            _gameplayInput.Disable();
        }

        private void OnDestroy()
        {
            _gameplayInput.Dispose();
        }

        public Vector2 GetClickPosition() => _gameplayInput.Gameplay.ClickPosition.ReadValue<Vector2>();

        private void OnClickCancelledHandler(InputAction.CallbackContext _) => ClickCancelled?.Invoke();
        private void OnClickPerformedHandler(InputAction.CallbackContext _) => ClickPerformed?.Invoke();

        private void InitializeInputSystem()
        {
            _gameplayInput = new();
            _gameplayInput.Gameplay.Click.performed += OnClickPerformedHandler;
            _gameplayInput.Gameplay.Click.canceled += OnClickCancelledHandler;
        }

        private void UpdateIsInputEnabled()
        {
            var visibleViews = _viewSystem.GetVisibleViews();
            if (visibleViews.Count == 1 && visibleViews[0] is HUDView && enabled)
            {
                _gameplayInput.Enable();
            }
            else
            {
                _gameplayInput.Disable();
            }
        }

        private void RegisterEventHandlers()
        {
            if (_viewSystem is not null)
            {
                _viewSystem.OnViewShown += OnViewShownHandler;
                _viewSystem.OnViewHidden += OnViewHiddenHandler;
            }
        }

        private void UnregisterEventHandlers()
        {
            _viewSystem.OnViewShown -= OnViewShownHandler;
            _viewSystem.OnViewHidden -= OnViewHiddenHandler;
        }

        private void OnViewHiddenHandler(View _)
        {
            UpdateIsInputEnabled();
        }

        private void OnViewShownHandler(View _)
        {
            UpdateIsInputEnabled();
        }
    }
}
