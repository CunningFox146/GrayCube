using GrayCube.Infrastructure;
using GrayCube.Moveable;
using System;
using UnityEngine;

namespace GrayCube.Slots
{
    public class MoveableSlotItem : RectTransformMoveable, ISlotItem
    {
        public event Action ItemPutInSlot;
        public event Action Cleared;

        private SlotsSystem _slotsSystem;
        private RectTransform Transform => transform as RectTransform;

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
            var slotRect = slot.transform as RectTransform;
            _isMoveable = false;
            Transform.SetParent(slotRect);
            Transform.anchoredPosition = Vector2.zero;
            ItemPutInSlot?.Invoke();
        }

        public override void StopMoving()
        {
            base.StopMoving();

            var slot = _slotsSystem.GetSlotAtPoint(Transform.anchoredPosition);

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
