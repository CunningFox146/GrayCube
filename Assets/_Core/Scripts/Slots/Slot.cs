using GrayCube.Infrastructure;
using System;
using UnityEngine;

namespace GrayCube.Slots
{
    public class Slot : MonoBehaviour
    {
        public event Action<Slot> Filled;
        public event Action<Slot> Cleared;

        private SlotsSystem _slotsSystem;

        [field: SerializeField] public bool IsItemDraggable { get; private set; }
        public ISlotItem Item { get; private set; }
        public bool IsFull => Item is not null;

        public virtual void Start()
        {
            _slotsSystem = GameplaySystemsFacade.Instance.SlotsSystem;

            _slotsSystem.RegisterSlot(this);
        }

        private void OnEnable()
        {
            _slotsSystem?.RegisterSlot(this);
        }

        private void OnDisable()
        {
            _slotsSystem.UnregisterSlot(this);
        }

        public virtual void PutItem(ISlotItem item)
        {
            if (item is null || Item is not null) return;
            Item = item;
            Item.OnPutInSlot(this);
            Filled?.Invoke(this);
        }

        public virtual void Clear()
        {
            if (Item is null) return;

            Item.OnCleared();
            Item = null;
            Cleared?.Invoke(this);
        }
    }
}
