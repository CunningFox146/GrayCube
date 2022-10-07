using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GrayCube.UI
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class ViewSystem : MonoBehaviour
    {
        public event Action<View> OnViewShown;

        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private Canvas _canvas;

        public List<View> Views { get; private set; }
        public Camera UICamera => _canvas.worldCamera;

        private void Awake()
        {
            RegisterViews();
        }

        public bool IsPointerOnUI(Vector3 pos) => GetElementsAtPoint(pos).Count > 0;

        public List<RaycastResult> GetElementsAtPoint(Vector3 pos)
        {
            var eventData = new PointerEventData(null);
            var results = new List<RaycastResult>();

            eventData.position = pos;
            _raycaster.Raycast(eventData, results);

            return results;
        }

        public T GetView<T>() where T : View
        {
            var view = Views.Where(v => v is T).FirstOrDefault();
            OnViewShown?.Invoke(view);
            return view as T;
        }

        public T ShowView<T>() where T : View
        {
            return ShowView(GetView<T>()) as T;
        }

        public View ShowView(View view)
        {
            view.Show();
            return view;
        }

        public T HideView<T>() where T : View
        {
            return HideView(GetView<T>()) as T;
        }

        public View HideView(View view)
        {
            view.Hide();
            return view;
        }

        public void HideAllViews()
        {
            Views.ForEach((view) => HideView(view));
        }

        public bool IsViewVisible<T>() where T : View
        {
            var view = GetView<T>();
            return view is not null && view.GetIsActive();
        }
        private void RegisterViews()
        {
            Views = new();
            GetComponentsInChildren(true, Views);
            foreach (View view in Views)
            {
                view.ViewSystem = this;
            }
        }
    }
}