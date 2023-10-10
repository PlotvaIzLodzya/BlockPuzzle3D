using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Game: MonoBehaviour
    {
        public static Game Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}

