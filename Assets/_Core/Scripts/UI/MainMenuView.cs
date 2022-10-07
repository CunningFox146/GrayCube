using GrayCube.Infrastructure;
using GrayCube.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _startButton;
        private SceneSystem _sceneSystem;

        private void Start()
        {
            _sceneSystem = MainSystemsFacade.Instance.SceneSystem;
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void OnStartClickHandler()
        {
            _sceneSystem.LoadGameplay();
        }

        private void RegisterEventHandlers()
        {
            _startButton.onClick.AddListener(OnStartClickHandler);
        }

        private void UnregisterEventHandlers()
        {
            _startButton.onClick.RemoveListener(OnStartClickHandler);
        }
    }
}
