using GrayCube.Infrastructure;
using GrayCube.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace GrayCube.UI
{
    public class DefaultButton : Button
    {
        [SerializeField] protected SoundInfo _clickSound;
        protected ISoundPlayer _soundPlayer;

        protected override void Start()
        {
            base.Start();
            if (Application.isPlaying)
            {
                _soundPlayer = MainSystemsFacade.Instance.SoundPlayer;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            RegisterEventHandlers();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnregisterEventHandlers();
        }

        private void PlayClickSound()
        {
            _soundPlayer?.PlaySound(_clickSound);
        }

        private void RegisterEventHandlers()
        {
            if (_clickSound is not null)
            {
                onClick.AddListener(PlayClickSound);
            }
        }

        private void UnregisterEventHandlers()
        {
            onClick.RemoveListener(PlayClickSound);
        }
    }
}
