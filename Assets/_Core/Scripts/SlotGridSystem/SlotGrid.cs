using GrayCube.Infrastructure;
using GrayCube.Save;
using GrayCube.Slots;
using UnityEngine;

namespace GrayCube.SlotGridSystem
{
    public class SlotGrid : MonoBehaviour
    {
        [SerializeField] private SlotGridLayout _layout;
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private float _spacing = 1f;
        private Slot[,] _slots;
        private IGridTracker _tracker;
        private bool _initialized = false;
        private SaveSystem _save;

        private RectTransform Transform => transform as RectTransform;
        public Vector2Int GridSize => _layout.GridSize;

        private void Awake()
        {
            _save = MainSystemsFacade.Instance.SaveSystem;
            InitSlots();
        }

        private void Start()
        {
            _tracker = GameplaySystemsFacade.Instance.GameState;
            LoadItems();
        }

        private void OnDestroy()
        {
            UnregisterAllSlots();
        }

        private void OnApplicationQuit()
        {
            SaveGrid();
        }

        private void InitSlots()
        {
            _slots = new Slot[GridSize.x, GridSize.y];

            var offset = new Vector2((GridSize.x - 1f) * -0.5f * _spacing, (GridSize.x - 1f) * -0.5f * _spacing);
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.x; y++)
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

        private void LoadItems()
        {
            var savedItems = _save.GetGridItems();
            if (savedItems != null)
            {
                LoadItemsFromSave(savedItems);
            }
            else
            {
                LoadItemsFromLayout();
            }

            _initialized = true;
        }

        private void LoadItemsFromSave(GameObject[,] savedItems)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    var slot = _slots[x, y];
                    var item = savedItems[x, y];
                    if (item != null)
                    {
                        slot.PutItem(Instantiate(item, slot.transform).GetComponent<ISlotItem>());
                    }
                }
            }
        }

        private void LoadItemsFromLayout()
        {
            var items = _layout.StartSlotItems;
            if (items is null) return;

            int i = 0;
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    int pos = x + y * GridSize.x;
                    if (pos >= items.Capacity) break;
                    var item = items[i++];
                    if (item == null) continue;

                    var slot = _slots[x, y];
                    slot.PutItem(Instantiate(item, slot.transform).GetComponent<ISlotItem>());
                }
            }
        }

        private Vector2Int GetSlotIndex(Slot slot)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
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

            for (int y = 0; y < GridSize.y; y++)
            {
                if (_slots[index.x, y].IsFull)
                {
                    filledColumns++;
                }
            }

            for (int x = 0; x < GridSize.x; x++)
            {
                if (_slots[x, index.y].IsFull)
                {
                    filledRows++;
                }
            }

            if (filledRows == GridSize.x) PopRow(index.y);
            if (filledColumns == GridSize.y) PopColumn(index.x);
        }

        private void PopRow(int rowY)
        {
            _tracker.OnRowPopped(rowY);
            for (int x = 0; x < GridSize.x; x++)
            {
                _slots[x, rowY].Clear();
            }
        }

        private void PopColumn(int columnX)
        {
            _tracker.OnColumnPopped(columnX);
            for (int y = 0; y < GridSize.y; y++)
            {
                _slots[columnX, y].Clear();
            }
        }

        private void SaveGrid()
        {
            var items = new ISlotItem[GridSize.x, GridSize.y];
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    items[x, y] = _slots[x, y].Item;
                }
            }
            _save.SetGridItems(items);
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
            if (_initialized)
            {
                _tracker.OnItemPut();
            }
        }
    }
}
