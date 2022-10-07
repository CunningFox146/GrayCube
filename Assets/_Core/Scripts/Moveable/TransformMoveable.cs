using UnityEngine;

namespace GrayCube.Moveable
{
    public class TransformMoveable : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _moveSpeed;

        protected bool _isMoveable = true;
        private bool _isMoving;
        private Vector2 _startPos;
        private Vector2 _targetPos;

        protected virtual void Update()
        {
            UpdateMovement();
        }

        public bool GetIsMoveable() => _isMoveable;

        public virtual void Move(Vector2 position)
        {
            _targetPos = position;
        }

        public virtual void StartMoving()
        {
            _startPos = transform.position;
            _isMoving = true;
        }

        public virtual void StopMoving() => _isMoving = false;

        public void ReturnToStartPos()
        {
            transform.position = _startPos;
            _targetPos = _startPos;
        }

        private void UpdateMovement()
        {
            if (_isMoving)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * _moveSpeed);
            }
        }
    }
}
