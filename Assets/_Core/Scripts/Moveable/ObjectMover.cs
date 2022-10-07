using GrayCube.Input;
using GrayCube.Utils;
using UnityEngine;

namespace GrayCube.Moveable
{
    public class ObjectMover : IUpdateable
    {
        private IInputSource _inputSource;
        private IMoveable _currentObject;
        private Camera _camera;

        private Vector3 WorldPoint => _camera.ScreenToWorldPoint(_inputSource.GetClickPosition());

        public ObjectMover(IInputSource input, Camera camera)
        {
            _inputSource = input;
            _camera = camera;

            _inputSource.ClickPerformed += OnClickPerformedHandler;
            _inputSource.ClickCancelled += OnClickCancelledHandler;
        }

        private IMoveable GetMoveableUnderPoint(Vector3 point)
        {
            var hit = Physics2D.Raycast(point, Vector2.zero, 0f, 1 << (int)Layers.Moveable);
            if (hit && hit.transform.TryGetComponent(out IMoveable moveable))
            {
                return moveable;
            }
            return null;
        }

        public void Update()
        {
            _currentObject?.Move(WorldPoint);
        }

        private void OnClickPerformedHandler()
        {
            _currentObject = GetMoveableUnderPoint(WorldPoint);
            _currentObject?.StartMoving();
        }

        private void OnClickCancelledHandler()
        {
            _currentObject?.StopMoving();
            _currentObject = null;
        }

    }
}
