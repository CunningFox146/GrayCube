using UnityEngine;
using UnityEngine.Audio;

namespace GrayCube.Sound
{
    [CreateAssetMenu(menuName = "Scriptable Objects/SoundInfo")]
    public class SoundInfo : ScriptableObject
    {
        [field: SerializeField] public bool IsLoop { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Volume { get; private set; } = 1f;
        [field: SerializeField] public float FixedPitch { get; private set; } = 1f;
        [field: SerializeField] public float[] PitchRange { get; private set; }
        [field: SerializeField] public AudioMixerGroup MixerGroup { get; private set; }
        [field: SerializeField] public AudioClip[] Clips { get; private set; }

        public float Pitch => (PitchRange?.Length == 0) ? FixedPitch : Random.Range(PitchRange[0], PitchRange[1]);
    }
}
