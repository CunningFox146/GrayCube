using UnityEditor;
using UnityEditor.UI;

namespace GrayCube.UI
{
    [CustomEditor(typeof(DefaultButton))]
    public class DefaultButtonEditor : ButtonEditor
    {
        private SerializedProperty _soundProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            _soundProperty = serializedObject.FindProperty("_clickSound");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.ObjectField(_soundProperty);

            serializedObject.ApplyModifiedProperties();
        }
    }
}