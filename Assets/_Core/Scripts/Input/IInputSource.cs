using System;
using UnityEngine;

namespace GrayCube.Input
{
    public interface IInputSource
    {
        public Vector2 GetClickPosition();
        public event Action ClickPerformed;
        public event Action ClickCancelled;
    }
}
