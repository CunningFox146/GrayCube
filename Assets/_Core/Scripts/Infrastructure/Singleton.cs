using UnityEngine;

namespace GrayCube.Infrastructure
{
    public class Singleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance is not null)
            {
                Debug.Log($"Destroying {gameObject.name}");
                Destroy(gameObject);
                return;
            }
            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this as T)
            {
                Instance = null;
            }
        }
    }
}
