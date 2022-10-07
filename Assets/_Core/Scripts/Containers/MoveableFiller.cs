using GrayCube.Infrastructure;
using GrayCube.Moveable;
using System;
using UnityEngine;

namespace GrayCube.Containers
{
    public class MoveableFiller : TransformMoveable, IFiller
    {
        public event Action Filled;
        public event Action Cleared;

        private FillableSystem _fillableSystem;

        private void Start()
        {
            _fillableSystem = GameplaySystemsFacade.Instance.FillableSystem;
        }

        public void OnCleared(Fillable fillable)
        {
            Destroy(gameObject);
            Cleared?.Invoke();
        }

        public void OnFilled(Fillable fillable)
        {
            _isMoveable = false;
            transform.SetParent(fillable.transform);
            transform.localPosition = Vector3.zero;
            Filled?.Invoke();
        }

        public override void StopMoving()
        {
            base.StopMoving();

            var fillable = _fillableSystem.GetFillableAtPoint(transform.position);

            if (fillable is not null)
            {
                fillable.Fill(this);
            }
            else
            {
                ReturnToStartPos();
            }
        }
    }
}
