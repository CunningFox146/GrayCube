using UnityEngine;

namespace GrayCube.Moveable
{
    internal class TransformMoveable : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _moveSpeed;

        private bool _isMoving;
        private Vector2 _targetPos;

        private void Update()
        {
            UpdateMovement();
        }

        public void Move(Vector2 position)
        {
            _targetPos = position;
        }

        public void StartMoving() => _isMoving = true;
        public void StopMoving() => _isMoving = false;

        private void UpdateMovement()
        {
            if (_isMoving)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * _moveSpeed);
            }
        }
    }
}
