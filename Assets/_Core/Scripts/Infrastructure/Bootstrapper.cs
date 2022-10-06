using UnityEngine;

namespace GrayCube.Infrastructure
{
    public static class Bootstrapper
    {
        private const string MainSystemsPath = "Infrastructure/MainSystemsFacade";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            Application.targetFrameRate = 120;
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load(MainSystemsPath)));
        }
    }
}