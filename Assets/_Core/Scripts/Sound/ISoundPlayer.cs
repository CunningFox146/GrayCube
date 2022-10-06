using UnityEngine;

namespace GrayCube.Sound
{
    internal interface ISoundPlayer
    {
        public AudioSource PlaySound(SoundInfo soundName);
    }
}
