using System;
using UnityEngine;

namespace GrayCube.Moveable
{
    public class TransformMoveable : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _moveSpeed;

        private bool _isMoving;
        private Vector2 _targetPos;

        protected virtual void Update()
        {
            UpdateMovement();
        }

        public virtual void Move(Vector2 position)
        {
            _targetPos = position;
        }

        public virtual void StartMoving() => _isMoving = true;
        public virtual void StopMoving() => _isMoving = false;

        private void UpdateMovement()
        {
            if (_isMoving)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * _moveSpeed);
            }
        }
    }
}
