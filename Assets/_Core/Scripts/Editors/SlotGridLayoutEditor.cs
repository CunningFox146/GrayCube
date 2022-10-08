using GrayCube.SlotGridSystem;
using GrayCube.Slots;
using UnityEditor;
using UnityEngine;

namespace GrayCube.Editors
{
    [CustomEditor(typeof(SlotGridLayout))]
    public class SlotGridLayoutEditor : Editor
    {
        private SlotGridLayout _target;
        private SerializedProperty _startSlotItems;
        private bool _showStartSlots;

        private void Awake()
        {
            _target = target as SlotGridLayout;
        }

        private void OnEnable()
        {
            _startSlotItems = serializedObject.FindProperty("_startSlotItems");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _showStartSlots = EditorGUILayout.Foldout(_showStartSlots, "Start items");

            if (_startSlotItems is null || !_showStartSlots) return;

            _startSlotItems.arraySize = _target.GridSize.x * _target.GridSize.y;
            for (int x = 0; x < _target.GridSize.x; x++)
            {
                GUILayout.BeginHorizontal();
                for (int y = 0; y < _target.GridSize.y; y++)
                {
                    int pos = x + y * _target.GridSize.x;
                    var element = _startSlotItems.GetArrayElementAtIndex(pos);
                    var newElement = EditorGUILayout.ObjectField(element.objectReferenceValue, typeof(GameObject), true) as GameObject;

                    if (newElement is not null && newElement.GetComponent<ISlotItem>() is null)
                    {
                        Debug.LogWarning($"{newElement} does not implement ISlotItem");
                    }
                    else
                    {
                        element.objectReferenceValue = newElement;
                    }
                }
                GUILayout.EndHorizontal();
            }

            if (serializedObject.ApplyModifiedProperties())
            foreach(var item in _target.StartSlotItems)
            {
                Debug.Log(item);
            }
        }
    }
}
