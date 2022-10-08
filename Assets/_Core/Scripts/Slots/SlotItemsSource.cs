using GrayCube.GameState;
using GrayCube.Infrastructure;
using GrayCube.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace GrayCube.Slots
{
    public class SlotItemsSource : MonoBehaviour
    {
        [SerializeField] private GameObject _slotItemPrefab;

        private IObjectPool<ISlotItem> _pool;
        private ISlotItem _currentItem;
        private IGameState _gameState;
        private RectTransform Transform => transform as RectTransform;

        private void Awake()
        {
            InitObjectPool();
            ReleaseItem();
        }

        private void Start()
        {
            _gameState = GameplaySystemsFacade.Instance.GameState;
            RegisterEventHandlers();
        }

        private void OnDestroy()
        {
            UnregisterEventHandlers();
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

            void OnItemClearedHandler()
            {
                item.Cleared -= OnItemClearedHandler;
                this.DelayAction(0.5f, () => _pool.Release(item));
            }

            item.ItemPutInSlot += OnItemPutInSlotHandler;
            item.Cleared += OnItemClearedHandler;

            _currentItem = item;
        }

        private void RegisterEventHandlers()
        {
            _gameState.OnGameWon += OnGameEndHandler;
            _gameState.OnGameLost += OnGameEndHandler;
        }

        private void UnregisterEventHandlers()
        {
            _gameState.OnGameWon -= OnGameEndHandler;
            _gameState.OnGameLost -= OnGameEndHandler;
        }

        private void OnGameEndHandler()
        {
            ((MonoBehaviour)_currentItem).gameObject.SetActive(false);
        }

        private void OnItemPutInSlotHandler()
        {
            _currentItem.ItemPutInSlot -= OnItemPutInSlotHandler;
            ReleaseItem();
        }
    }
}
