using GrayCube.Util;
using GrayCube.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace GrayCube.Sound
{
    public class SoundPlayer2D : MonoBehaviour, ISoundPlayer
    {
        private IObjectPool<AudioSource> _audioPool;

        private void Awake()
        {
            InitAudioPool();
        }

        private void OnDestroy()
        {
            _audioPool.Clear();
        }

        public AudioSource PlaySound(SoundInfo info)
        {
            var sound = _audioPool.Get();

            sound.pitch = info.Pitch;
            sound.volume = info.Volume;
            sound.outputAudioMixerGroup = info.MixerGroup;
            sound.loop = info.IsLoop;
            sound.clip = ArrayUtil.GetRandomItem(info.Clips);

            sound.Play();

            if (!info.IsLoop)
            {
                this.DelayAction(sound.clip.length, () => StopSound(sound));
            }

            return sound;
        }

        private void InitAudioPool()
        {
            _audioPool = new ObjectPool<AudioSource>(
                            CreateAudioSource,
                            (obj) => obj.gameObject.SetActive(true),
                            (obj) => obj.gameObject.SetActive(false)
                        );
        }

        private AudioSource CreateAudioSource()
        {
            var sound = new GameObject("Sound");
            sound.transform.SetParent(transform);
            return sound.AddComponent<AudioSource>();
        }

        private void StopSound(AudioSource sound)
        {
            _audioPool.Release(sound);
        }
    }
}
