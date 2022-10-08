using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrayCube.Save
{
    [CreateAssetMenu(menuName = "Scriptable Objects/SlotItem Data")]
    public class SlotItemData : ScriptableObject
    {
        [field: SerializeField] public List<SlotItemRecord> ItemRecords;
    }

    [Serializable]
    public struct SlotItemRecord
    {
        [field: SerializeField] public GameObject SlotItemPrefab { get; private set; }
        [field: SerializeField] public SlotItemId Id { get; private set; }
    }

    [Serializable]
    public enum SlotItemId
    {
        None = 0,
        Default = 1,
    }
}
