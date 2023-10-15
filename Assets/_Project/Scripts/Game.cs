using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Puzzles;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Game: MonoBehaviour
    {
        [SerializeField] private Puzzle _puzzle;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Masks _masks;
        [SerializeField] private Grab _grab;

        private IComplition _levelComplition;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private void Awake()
        {
            if(CoroutineHandler == null)
                CoroutineHandler = this;

            _grab.Construct(_masks);
            var puzzleDependency = new PuzzleDependency(_masks); 
            _levelComplition = _puzzle.Construct(puzzleDependency);
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

