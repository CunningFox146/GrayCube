using UnityEngine;

namespace GrayCube.Moveable
{
    public class RectTransformMoveable : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _moveSpeed;

        protected bool _isMoveable = true;
        private bool _isMoving;
        private Vector2 _startPos;
        private Vector2 _targetPos;

        private RectTransform Transform => transform as RectTransform;

        public bool GetIsMoveable() => _isMoveable;

        protected virtual void Update()
        {
            UpdateMovement();
        }

        public virtual void Move(Vector2 position)
        {
            _targetPos = position;
        }

        public virtual void StartMoving()
        {
            _startPos = Transform.anchoredPosition;
            _targetPos = transform.position;
            _isMoving = true;
        }

        public virtual void StopMoving() => _isMoving = false;

        public void ReturnToStartPos() => Transform.anchoredPosition = _startPos;

        private void UpdateMovement()
        {
            if (_isMoving)
            {
                var targetPos = new Vector3(_targetPos.x, _targetPos.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _moveSpeed);
            }
        }
    }
}
