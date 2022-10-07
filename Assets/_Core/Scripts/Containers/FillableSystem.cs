using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace GrayCube.Containers
{
    public class FillableSystem : MonoBehaviour
    {
        [SerializeField] private float _fillableSize;
        private List<Fillable> _fillables = new();

        public void RegisterFillable(Fillable fillable)
        {
            _fillables.Add(fillable);
        }

        public void UnregisterFillable(Fillable fillable)
        {
            _fillables.Remove(fillable);
        }

        public Fillable GetFillableAtPoint(Vector3 pos)
        {
            foreach (Fillable fillable in _fillables)
            {
                var distance = fillable.transform.position - pos;

                if (Mathf.Abs(distance.x) <= _fillableSize
                    && Mathf.Abs(distance.y) <= _fillableSize)
                {
                    return fillable;
                }
            }
            return null;
        }
    }
}
