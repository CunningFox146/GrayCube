using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrayCube.Scenes
{
    public class SceneSystem : MonoBehaviour
    {
        public event Action OnSceneChangeStart;
        public event Action OnSceneChanged;

        private Coroutine _sceneLoadCoroutine;

        public SceneIndex CurrentScene { get; private set; }
        public float LoadProgress { get; private set; }
        public bool IsLoading => _sceneLoadCoroutine is not null;

        public void LoadMainMenu() => LoadScene(SceneIndex.MainMenu);
        public void LoadGameplay() => LoadScene(SceneIndex.Gameplay);

        public void LoadScene(SceneIndex index)
        {
            if (IsLoading) return;

            int buildIndex = (int)index;
            var loadOperation = SceneManager.LoadSceneAsync(buildIndex);
            _sceneLoadCoroutine = StartCoroutine(LoadCoroutine(loadOperation));
            OnSceneChangeStart?.Invoke();
        }

        private IEnumerator LoadCoroutine(AsyncOperation operation)
        {
            while (!operation.isDone)
            {
                LoadProgress = operation.progress;
                yield return null;
            }
            OnSceneChanged?.Invoke();
            _sceneLoadCoroutine = null;
        }
    }
}
