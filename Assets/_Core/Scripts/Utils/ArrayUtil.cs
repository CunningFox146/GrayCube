using UnityEngine;

namespace GrayCube.Util
{
    public static class ArrayUtil
    {
        public static T GetRandomItem<T>(T[] array)
        {
            int length = array.Length;
            if (length == 1) return array[0];

            int idx = Random.Range(0, length);
            return array[idx];
        }
    }
}