using GrayCube.Infrastructure;
using GrayCube.Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class GameEndView : View
    {
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private TMP_Text _header;
        [SerializeField] private string _wonText;
        [SerializeField] private string _lostText;

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

        public void SetupWin()
        {
            _header.text = _wonText;
        }

        public void SetupLost()
        {
            _header.text = _lostText;
        }

        private void OnPlayAgainClicked()
        {
            _sceneSystem.LoadGameplay();
        }
        private void RegisterEventHandlers()
        {
            _playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        }

        private void UnregisterEventHandlers()
        {
            _playAgainButton.onClick.RemoveListener(OnPlayAgainClicked);
        }

    }
}
