using System;
using UnityEngine;

namespace GrayCube.Moveable
{
    public interface IMoveable
    {
        public event Action StartMoving;
        public bool GetIsMoveable();
        public void OnStartMoving();
        public void OnStopMoving();
        public void Move(Vector2 position);
    }
}