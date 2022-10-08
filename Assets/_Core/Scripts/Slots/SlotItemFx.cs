using UnityEngine;

namespace GrayCube.Slots
{
    [RequireComponent(typeof(ISlotItem), typeof(Animator))]
    public class SlotItemFx : MonoBehaviour
    {
        private static readonly int FadeAnimation = Animator.StringToHash("Fade");

        private ISlotItem _slotItem;
        private Animator _animator;

        private void Awake()
        {
            _slotItem = GetComponent<ISlotItem>();
            _animator = GetComponent<Animator>();
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
            _slotItem.Cleared += OnItemClearedHandler;
        }

        private void OnItemClearedHandler()
        {
            _animator.CrossFade(FadeAnimation, 0f);
        }

        private void UnregisterEventHandlers()
        {
            _slotItem.Cleared -= OnItemClearedHandler;
        }
    }
}
