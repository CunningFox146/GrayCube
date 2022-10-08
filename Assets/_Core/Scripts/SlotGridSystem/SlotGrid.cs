using GrayCube.Slots;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GrayCube.SlotGridSystem
{
    public class SlotGrid : MonoBehaviour
    {
        public event Action<int> RowPopped;
        public event Action<int> ColumnPopped;

        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _spacing = 1f;

        [SerializeField, HideInInspector] private List<GameObject> _startSlotItems = new();
        private Slot[,] _slots;

        private RectTransform Transform => transform as RectTransform;
        public Vector2Int GridSize => _gridSize;
    
        private void Awake()
        {
            InitSlots();
            //LoadItems();
        }

        private void OnDestroy()
        {
            UnregisterAllSlots();
        }

        private void InitSlots()
        {
            _slots = new Slot[_gridSize.x, _gridSize.y];

            var offset = new Vector2((_gridSize.x - 1f) * -0.5f * _spacing, (_gridSize.x - 1f) * -0.5f * _spacing);
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.x; y++)
                {
                    var slot = Instantiate(_slotPrefab, Transform);
                    var rectTransform = slot.transform as RectTransform;

                    slot.name = $"Slot [{x}, {y}]";
                    rectTransform.anchoredPosition = offset + new Vector2(x * _spacing, y * _spacing);
                    _slots[x, y] = slot;

                    RegisterSlot(slot);
                }
            }
        }

        //private void LoadItems()
        //{
        //    if (StartItems is null) return;
        //    for (int x = 0; x < _gridSize.x; x++)
        //    {
        //        for (int y = 0; y < _gridSize.y; y++)
        //        {
        //            var item = StartItems[x, y];
        //        }
        //    }
        //}

        private Vector2Int GetSlotIndex(Slot slot)
        {
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    var currentSlot = _slots[x, y];
                    if (currentSlot == slot)
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }
            return Vector2Int.zero;
        }

        private void CheckShouldPop(Vector2Int index)
        {
            int filledRows = 0;
            int filledColumns = 0;

            for (int y = 0; y < _gridSize.y; y++)
            {
                if (_slots[index.x, y].IsFull)
                {
                    filledColumns++;
                }
            }

            for (int x = 0; x < _gridSize.x; x++)
            {
                if (_slots[x, index.y].IsFull)
                {
                    filledRows++;
                }
            }

            if (filledRows == _gridSize.x) PopRow(index.y);
            if (filledColumns == _gridSize.y) PopColumn(index.x);
        }

        private void PopRow(int rowY)
        {
            RowPopped?.Invoke(rowY);
            for (int x = 0; x < _gridSize.x; x++)
            {
                _slots[x, rowY].Clear();
            }
        }

        private void PopColumn(int columnX)
        {
            ColumnPopped?.Invoke(columnX);
            for (int y = 0; y < _gridSize.y; y++)
            {
                _slots[columnX, y].Clear();
            }
        }

        private void RegisterSlot(Slot slot)
        {
            slot.Filled += OnSlotFilledHandler;
        }

        private void UnregisterSlot(Slot slot)
        {
            slot.Filled -= OnSlotFilledHandler;
        }

        private void UnregisterAllSlots()
        {
            foreach (Slot slot in _slots)
            {
                UnregisterSlot(slot);
            }
        }

        private void OnSlotFilledHandler(Slot slot)
        {
            CheckShouldPop(GetSlotIndex(slot));
        }
    }
}
