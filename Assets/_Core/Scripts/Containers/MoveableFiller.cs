using GrayCube.Moveable;
using System;

namespace GrayCube.Containers
{
    public class MoveableFiller : TransformMoveable, IFiller
    {
        public event Action Filled;
        public event Action Cleared;

        public void OnCleared(Fillable fillable)
        {
            Destroy(gameObject);
            Cleared?.Invoke();
        }

        public void OnFilled(Fillable fillable)
        {
            transform.SetParent(fillable.transform);
            Filled?.Invoke();
        }

        public override void StopMoving()
        {
            base.StopMoving();
            // Snap to closest Fillable
        }
    }
}
