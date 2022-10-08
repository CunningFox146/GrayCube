using GrayCube.Infrastructure;
using GrayCube.Sound;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class SoundSettingsView : View
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private PercentageSlider _soundSlider;
        private AudioMixerSystem _audioMixerSystem;


        public override void Show()
        {
            base.Show();
            _soundSlider.SetPercent(_audioMixerSystem.Volume);
        }

        private void OnEnable()
        {
            _audioMixerSystem = MainSystemsFacade.Instance.AudioMixerSystem;
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
            _audioMixerSystem.Volume = percentage;
        }
    }
}
