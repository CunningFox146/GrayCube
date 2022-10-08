using System;

namespace GrayCube.GameState
{
    public interface IGameState
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
    }
}