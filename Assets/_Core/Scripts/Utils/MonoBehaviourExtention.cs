using System;
using System.Collections;
using UnityEngine;

namespace GrayCube.Utils
{
    public static class MonoBehaviourExtension
    {
        public static Coroutine DelayAction(this MonoBehaviour monoBehaviour, float delay, Action action)
        {
            IEnumerator DelayCoroutine()
            {
                yield return new WaitForSeconds(delay);
                action.Invoke();
            }
            return monoBehaviour.StartCoroutine(DelayCoroutine());
        }
    }
}
