using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Puzzles;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Game: MonoBehaviour
    {
        [SerializeField] private Puzzle _puzzle;
        [SerializeField] private GameUI _gameUI;

        public static MonoBehaviour Instance { get; private set; }
        private IComplition _levelComplition;

        private void Awake()
        {
            Instance = this;
            _levelComplition = _puzzle.Construct();
            _levelComplition.OnChange += OnLevelProgress;
            _gameUI.Construct();
        }

        private void OnDestroy()
        {
            _levelComplition.OnChange -= OnLevelProgress;
        }

        private void OnLevelProgress()
        {
            if(_levelComplition.Completed)
            {
                _gameUI.OnLevelEnd();
            }
        }
    }
}

