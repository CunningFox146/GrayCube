using System;

namespace GrayCube.Containers
{
    public interface ISlotItem
    {
        public event Action ItemPutInSlot;
        public event Action Cleared;

        public void OnPutInSlot(Slot slot);
        public void OnCleared();
    }
}