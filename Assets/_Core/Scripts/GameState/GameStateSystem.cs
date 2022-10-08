using GrayCube.Infrastructure;
using GrayCube.SlotGridSystem;
using GrayCube.UI;
using UnityEngine;

namespace GrayCube.GameState
{
    public class GameStateSystem : MonoBehaviour, IGridTracker
    {
        private ViewSystem _viewSystem;
        public bool IsGamePlay { get; private set; } = true;

        private void Start()
        {
            _viewSystem = GameplaySystemsFacade.Instance.ViewSystem;
        }

        public void OnRowPopped(int row)
        {
            if (IsGamePlay)
            {
                Win();
            }
        }

        public void OnColumnPopped(int column)
        {
            if (IsGamePlay)
            {
                Win();
            }
        }

        public void OnItemPut()
        {
            if (IsGamePlay)
            {
                Loose();
            }
        }

        private void Win()
        {
            IsGamePlay = false;
            var view = _viewSystem.GetView<GameEndView>();
            view.SetupWin();
            _viewSystem.ShowView(view);
        }

        private void Loose()
        {
            IsGamePlay = false;
            var view = _viewSystem.GetView<GameEndView>();
            view.SetupLost();
            _viewSystem.ShowView(view);
        }
    }
}
