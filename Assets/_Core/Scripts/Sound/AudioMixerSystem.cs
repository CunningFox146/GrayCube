using GrayCube.Infrastructure;
using GrayCube.Save;
using UnityEngine;
using UnityEngine.Audio;

namespace GrayCube.Sound
{
    public class AudioMixerSystem : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        private SaveSystem _saveSystem;

        public float Volume
        {
            get => _saveSystem.Volume;
            set
            {
                _saveSystem.Volume = value;
                SetMixerVolume(value);
            }
        }

        private void Start()
        {
            _saveSystem = MainSystemsFacade.Instance.SaveSystem;
            SetMixerVolume(Volume);
        }
        private void SetMixerVolume(float value)
        {
            _audioMixer.SetFloat("Volume", (-80 + value * 80));
        }

    }
}
