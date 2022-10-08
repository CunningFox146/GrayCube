using System.Collections.Generic;
using UnityEngine;

namespace GrayCube.SlotGridSystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Grid Layout")]
    public class SlotGridLayout : ScriptableObject
    {
        [SerializeField, HideInInspector] private List<GameObject> _startSlotItems = new();
        [field: SerializeField] public Vector2Int GridSize { get; private set; }

        public List<GameObject> StartSlotItems => _startSlotItems;
    }
}
