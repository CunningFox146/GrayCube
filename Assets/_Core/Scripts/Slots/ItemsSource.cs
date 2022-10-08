using UnityEngine;
using UnityEngine.Pool;

namespace GrayCube.Slots
{
    public class ItemsSource : MonoBehaviour
    {
        [SerializeField] private GameObject _slotItemPrefab;

        private IObjectPool<ISlotItem> _pool;
        private ISlotItem _currentItem;

        private RectTransform Transform => transform as RectTransform;

        private void Awake()
        {
            InitObjectPool();
            ReleaseItem();
        }

        private void InitObjectPool()
        {
            _pool = new ObjectPool<ISlotItem>(
                CreateItem,
                (item) => ((MonoBehaviour)item).gameObject.SetActive(true),
                (item) => ((MonoBehaviour)item).gameObject.SetActive(false)
            );
        }

        private ISlotItem CreateItem()
        {
            var item = Instantiate(_slotItemPrefab, Transform);
            return item.GetComponent<ISlotItem>();
        }

        private void ReleaseItem()
        {
            var item = _pool.Get();
            item.ItemPutInSlot += OnItemPutInSlotHandler;

            _currentItem = item;
        }

        private void OnItemPutInSlotHandler()
        {
            _currentItem.ItemPutInSlot -= OnItemPutInSlotHandler;
            ReleaseItem();
        }
    }
}
