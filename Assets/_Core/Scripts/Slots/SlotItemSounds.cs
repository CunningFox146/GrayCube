using GrayCube.Infrastructure;
using GrayCube.Moveable;
using GrayCube.Sound;
using UnityEngine;

namespace GrayCube.Slots
{
    [RequireComponent(typeof(ISlotItem), typeof(IMoveable))]
    public class SlotItemSounds : MonoBehaviour
    {
        [SerializeField] private SoundInfo _startMovingSound;
        [SerializeField] private SoundInfo _putInSlotSound;

        private ISlotItem _slotItem;
        private IMoveable _moveable;
        private ISoundPlayer _soundPlayer;

        private void Awake()
        {
            _slotItem = GetComponent<ISlotItem>();
            _moveable = GetComponent<IMoveable>();
        }

        private void Start()
        {
            _soundPlayer = MainSystemsFacade.Instance.SoundPlayer;
        }

        private void OnEnable()
        {
            RegisterEventHandlers();
        }

        private void OnDisable()
        {
            UnregisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _slotItem.ItemPutInSlot += OnItemPutInSlotHandler;
            _moveable.StartMoving += OnStartMovingHander;
        }

        private void UnregisterEventHandlers()
        {
            _slotItem.ItemPutInSlot -= OnItemPutInSlotHandler;
            _moveable.StartMoving -= OnStartMovingHander;
        }

        private void OnStartMovingHander()
        {
            _soundPlayer?.PlaySound(_startMovingSound);
        }

        private void OnItemPutInSlotHandler()
        {
            _soundPlayer?.PlaySound(_putInSlotSound);
        }
    }
}
