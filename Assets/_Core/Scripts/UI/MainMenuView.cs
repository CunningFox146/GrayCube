using GrayCube.Infrastructure;
using GrayCube.Scenes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _soundSettingsButton;
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

        private void OnSoundSettingsClicked()
        {
            ViewSystem.ShowView<SoundSettingsView>();
        }

        private void RegisterEventHandlers()
        {
            _startButton.onClick.AddListener(OnStartClickHandler);
            _soundSettingsButton.onClick.AddListener(OnSoundSettingsClicked);
        }

        private void UnregisterEventHandlers()
        {
            _startButton.onClick.RemoveListener(OnStartClickHandler);
            _soundSettingsButton.onClick.RemoveListener(OnSoundSettingsClicked);
        }
    }
}
