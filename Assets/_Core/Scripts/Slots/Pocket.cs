using GrayCube.Infrastructure;
using GrayCube.Save;

namespace GrayCube.Slots
{
    public class Pocket : Slot
    {
        private SaveSystem _saveSystem;
        protected override void Start()
        {
            base.Start();
            _saveSystem = MainSystemsFacade.Instance.SaveSystem;
            LoadPocketItem();
        }

        private void OnApplicationQuit()
        {
            SavePocket();
        }

        private void LoadPocketItem()
        {
            var pocketItem = _saveSystem.GetPocketItem();
            if (pocketItem != null)
            {
                PutItem(Instantiate(pocketItem, transform).GetComponent<ISlotItem>());
            }
        }

        private void SavePocket()
        {
            _saveSystem.SetPocketItem(Item);
        }
    }
}
