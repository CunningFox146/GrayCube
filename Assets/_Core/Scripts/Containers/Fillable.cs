using GrayCube.Infrastructure;
using System;
using UnityEngine;

namespace GrayCube.Containers
{
    public class Fillable : MonoBehaviour
    {
        public event Action Filled;
        public event Action Cleared;

        private FillableSystem _fillableSystem;

        public IFiller Filler { get; private set; }

        public virtual void Start()
        {
            _fillableSystem = GameplaySystemsFacade.Instance.FillableSystem;

            _fillableSystem.RegisterFillable(this);
        }

        private void OnEnable()
        {
            _fillableSystem?.RegisterFillable(this);
        }

        private void OnDisable()
        {
            _fillableSystem.UnregisterFillable(this);
        }

        public virtual void Fill(IFiller filler)
        {
            if (filler is null || Filler is not null) return;

            Filler = filler;
            Filler.OnFilled(this);
            Filled?.Invoke();
        }

        public virtual void Clear()
        {
            if (Filler is null) return;

            Filler.OnCleared(this);
            Filler = null;
            Cleared?.Invoke();
        }
    }
}
