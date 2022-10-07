using System;

namespace GrayCube.Containers
{
    public interface IFiller
    {
        public event Action Filled;
        public event Action Cleared;

        public void OnFilled(Fillable fillable);
        public void OnCleared(Fillable fillable);
    }
}