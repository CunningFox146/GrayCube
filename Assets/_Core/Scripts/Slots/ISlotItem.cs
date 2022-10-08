using System;

namespace GrayCube.Slots
{
    public interface ISlotItem
    {
        public event Action ItemPutInSlot;
        public event Action Cleared;

        public void OnPutInSlot(Slot slot);
        public void OnCleared();
    }
}