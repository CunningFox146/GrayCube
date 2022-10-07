using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class SoundSettingsView : View
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private PercentageSlider _soundSlider;

        private void Start()
        {
            // Register
        }

        public override void Show()
        {
            base.Show();
            // load percent
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _closeButton.onClick.AddListener(OnCloseClickedHandler);
            _soundSlider.PercentageChanged += PercentageChangedHandler;
        }

        private void UnregisterEventHandlers()
        {
            _closeButton.onClick.RemoveListener(OnCloseClickedHandler);
            _soundSlider.PercentageChanged -= PercentageChangedHandler;
        }

        private void OnCloseClickedHandler()
        {
            ViewSystem.HideView(this);
        }

        private void PercentageChangedHandler(float percentage)
        {
            Debug.Log(percentage);
        }
    }
}
