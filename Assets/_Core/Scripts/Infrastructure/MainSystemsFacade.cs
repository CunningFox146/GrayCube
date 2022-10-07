using GrayCube.Scenes;
using GrayCube.Sound;
using UnityEngine;

namespace GrayCube.Infrastructure
{
    public class MainSystemsFacade : Singleton<MainSystemsFacade>
    {
        [field: SerializeField] public SceneSystem SceneSystem { get; private set; }
        public ISoundPlayer SoundPlayer { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SoundPlayer = GetComponentInChildren<ISoundPlayer>();
        }
    }
}