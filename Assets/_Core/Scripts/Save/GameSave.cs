using GrayCube.Slots;
using System;
using UnityEngine;

namespace GrayCube.Save
{
    [Serializable]
    public class GameSave
    {
        public SlotItemId[,] _savedItems { get; set; }
        public SlotItemId _pocketItem { get; set; }
        public float Volume { get; set; } = 1f;

        public GameObject[,] GetGridItems(SlotItemData itemData)
        {
            if (_savedItems == null) return null;

            int sizeX = _savedItems.GetLength(0);
            int sizeY = _savedItems.GetLength(1);

            var slots = new GameObject[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeX; y++)
                {
                    slots[x, y] = GetSlotPrefabById(_savedItems[x, y], itemData);
                }
            }
            return slots;
        }

        public void SetGridItems(ISlotItem[,] items)
        {
            if (items == null)
            {
                _savedItems = null;
                return;
            }

            int sizeX = items.GetLength(0);
            int sizeY = items.GetLength(1);

            _savedItems = new SlotItemId[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeX; y++)
                {
                    var item = items[x, y];
                    if (item == null) continue;
                    _savedItems[x, y] = item.GetId();
                }
            }
        }

        public void SetPocketItem(ISlotItem item)
        {
            _pocketItem = item != null ? item.GetId() : SlotItemId.None;
        }

        public GameObject GetPocketItem(SlotItemData itemData)
        {
            return GetSlotPrefabById(_pocketItem, itemData);
        }

        private GameObject GetSlotPrefabById(SlotItemId id, SlotItemData itemData)
        {
            return itemData.ItemRecords.Find(e => e.Id == id).SlotItemPrefab;
        }
    }
}