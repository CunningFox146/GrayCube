using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class HUDView : View
    {

        [SerializeField] private Button _soundSettingsButton;

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void OnSoundSettingsClicked()
        {
            ViewSystem.ShowView<SoundSettingsView>();
        }

        private void RegisterEventHandlers()
        {
            _soundSettingsButton.onClick.AddListener(OnSoundSettingsClicked);
        }

        private void UnregisterEventHandlers()
        {
            _soundSettingsButton.onClick.RemoveListener(OnSoundSettingsClicked);
        }
    }
}
