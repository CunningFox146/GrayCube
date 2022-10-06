using GrayCube.Sound;

namespace GrayCube.Infrastructure
{
    public class MainSystemsFacade : Singleton<MainSystemsFacade>
    {
        public ISoundPlayer SoundPlayer { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SoundPlayer = GetComponentInChildren<ISoundPlayer>();
        }
    }
}