using UnityEngine;

namespace GrayCube.Sound
{
    public interface ISoundPlayer
    {
        public AudioSource PlaySound(SoundInfo soundName);
    }
}
