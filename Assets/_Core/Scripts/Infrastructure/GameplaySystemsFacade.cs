using GrayCube.Input;
using UnityEngine;

namespace GrayCube.Infrastructure
{
    public class GameplaySystemsFacade : Singleton<GameplaySystemsFacade>
    {
        [field: SerializeField] public InputSystem Input { get; private set; }
    }
}
