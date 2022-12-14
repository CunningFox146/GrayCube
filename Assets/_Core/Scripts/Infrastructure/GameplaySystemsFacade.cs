using GrayCube.Slots;
using GrayCube.Input;
using GrayCube.UI;
using UnityEngine;
using GrayCube.GameState;

namespace GrayCube.Infrastructure
{
    public class GameplaySystemsFacade : Singleton<GameplaySystemsFacade>
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public InputSystem Input { get; private set; }
        [field: SerializeField] public ViewSystem ViewSystem { get; private set; }
        [field: SerializeField] public SlotsSystem SlotsSystem { get; internal set; }
        [field: SerializeField] public GameStateSystem GameState { get; internal set; }
    }
}
