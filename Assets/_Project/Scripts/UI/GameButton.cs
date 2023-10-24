using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class GameButton : Panel
    {
        [field: SerializeField] public Button Button { get; private set; }
    }
}

