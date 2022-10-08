using GrayCube.Infrastructure;
using GrayCube.Moveable;
using GrayCube.Save;
using System;
using UnityEngine;

namespace GrayCube.Slots
{
    public class MoveableSlotItem : RectTransformMoveable, ISlotItem
    {
        public event Action ItemPutInSlot;
        public event Action Cleared;

        [SerializeField] private SlotItemId _id;
        private SlotsSystem _slotsSystem;

        private RectTransform Transform => transform as RectTransform;
        
        private void Start()
        {
            _slotsSystem = GameplaySystemsFacade.Instance.SlotsSystem;
        }

        public SlotItemId GetId() => _id;

        public void OnCleared()
        {
            Cleared?.Invoke();
        }

        public void OnPutInSlot(Slot slot)
        {
            var slotRect = slot.transform as RectTransform;
            _isMoveable = slot.IsItemDraggable;
            Transform.SetParent(slotRect);
            Transform.anchoredPosition = Vector2.zero;
            ItemPutInSlot?.Invoke();
        }

        public override void OnStopMoving()
        {
            base.OnStopMoving();

            var slot = _slotsSystem.GetSlotAtPoint(Transform.position);

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
