using System;
using UnityEngine;

namespace GrayCube.Moveable
{
    public interface IMoveable
    {
        public void StartMoving();
        public void StopMoving();
        public void Move(Vector2 position);
    }
}