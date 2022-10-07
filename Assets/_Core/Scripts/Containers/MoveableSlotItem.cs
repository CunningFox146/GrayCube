using GrayCube.Infrastructure;
using GrayCube.Moveable;
using System;
using UnityEngine;

namespace GrayCube.Containers
{
    public class MoveableSlotItem : TransformMoveable, ISlotItem
    {
        public event Action ItemPutInSlot;
        public event Action Cleared;

        private SlotsSystem _slotsSystem;

        private void Start()
        {
            _slotsSystem = GameplaySystemsFacade.Instance.SlotsSystem;
        }

        public void OnCleared()
        {
            Destroy(gameObject);
            Cleared?.Invoke();
        }

        public void OnPutInSlot(Slot slot)
        {
            _isMoveable = false;
            transform.SetParent(slot.transform);
            transform.localPosition = Vector3.zero;
            ItemPutInSlot?.Invoke();
        }

        public override void StopMoving()
        {
            base.StopMoving();

            var slot = _slotsSystem.GetSlotAtPoint(transform.position);

            if (slot is not null && !slot.IsFull)
            {
                slot.PutItem(this);
            }
            else
            {
                ReturnToStartPos();
            }
        }
    }
}
