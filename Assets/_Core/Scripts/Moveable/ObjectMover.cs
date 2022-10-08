using GrayCube.Input;
using GrayCube.UI;
using GrayCube.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GrayCube.Moveable
{
    public class ObjectMover : IUpdateable
    {
        private ViewSystem _viewSystem;
        private IInputSource _inputSource;
        private IMoveable _currentObject;

        public ObjectMover(IInputSource input, ViewSystem viewSystem)
        {
            _inputSource = input;
            _viewSystem = viewSystem;

            _inputSource.ClickPerformed += OnClickPerformedHandler;
            _inputSource.ClickCancelled += OnClickCancelledHandler;
        }

        private IMoveable GetMoveableUnderPoint(Vector3 point)
        {
            var elements = _viewSystem.GetElementsAtPoint(point);
            foreach (RaycastResult element in elements)
            {
                if (element.gameObject.TryGetComponent(out IMoveable moveable) && moveable.GetIsMoveable())
                {
                    return moveable;
                }
            }
            return null;
        }

        public void Update()
        {
            _currentObject?.Move(_viewSystem.ScreenToCanvasPos(_inputSource.GetClickPosition()));
        }

        private void OnClickPerformedHandler()
        {
            _currentObject = GetMoveableUnderPoint(_inputSource.GetClickPosition());
            _currentObject?.StartMoving();
        }

        private void OnClickCancelledHandler()
        {
            _currentObject?.StopMoving();
            _currentObject = null;
        }
    }
}
