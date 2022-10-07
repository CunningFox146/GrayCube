using UnityEngine;

namespace GrayCube.Slots
{
    public class SlotsGrid : MonoBehaviour
    {
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private float _spacing = 1f;

        private Slot[,] _slots;

        private void Awake()
        {
            _slots = new Slot[_gridSize.x, _gridSize.y];

            var offset = new Vector2((_gridSize.x - 1f) * -0.5f, (_gridSize.x - 1f) * -0.5f);
            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.x; y++)
                {
                    var slot = Instantiate(_slotPrefab);
                    slot.name = $"Slot [{x}, {y}]";
                    slot.transform.SetParent(transform);
                    slot.transform.localPosition = offset + new Vector2(x * _spacing, y * _spacing);
                    _slots[x, y] = slot;
                }
            }
        }
    }
}
