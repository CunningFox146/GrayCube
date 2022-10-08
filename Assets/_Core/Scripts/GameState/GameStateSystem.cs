using GrayCube.Infrastructure;
using GrayCube.Save;
using GrayCube.SlotGridSystem;
using GrayCube.Sound;
using GrayCube.UI;
using GrayCube.Utils;
using System;
using UnityEngine;

namespace GrayCube.GameState
{
    public class GameStateSystem : MonoBehaviour, IGridTracker, IGameState
    {
        public event Action OnGameWon;
        public event Action OnGameLost;

        [SerializeField] private SoundInfo _winSound;
        [SerializeField] private SoundInfo _looseSound;

        private ViewSystem _viewSystem;
        private SaveSystem _saveSystem;
        private ISoundPlayer _soundPlayer;

        public bool IsGamePlay { get; private set; } = true;

        private void Start()
        {
            var mainSystems = MainSystemsFacade.Instance;
            _saveSystem = mainSystems.SaveSystem;
            _soundPlayer = mainSystems.SoundPlayer;
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
            OnGameWon?.Invoke();
            _saveSystem.ClearPockets();

            this.DelayAction(1f, () =>
            {
                _soundPlayer.PlaySound(_winSound);
                var view = _viewSystem.GetView<GameEndView>();
                view.SetupWin();
                _viewSystem.ShowView(view);
            });
        }

        private void Loose()
        {
            OnGameLost?.Invoke();
            IsGamePlay = false;
            var view = _viewSystem.GetView<GameEndView>();
            view.SetupLost();
            _viewSystem.ShowView(view);
            _soundPlayer.PlaySound(_looseSound);
            _saveSystem.ClearPockets();
        }
    }
}
