using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Puzzles;
using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class PuzzleFactory
    {
        [SerializeField] private Puzzle[] _puzzlePrefabs;

        public Puzzle GetPuzzle(int index)
        {
            index = Mathf.Clamp(index, 0, _puzzlePrefabs.Length);

            return _puzzlePrefabs[index];
        }
    }

    public class Game: MonoBehaviour
    {
        [SerializeField] private PuzzleFactory _puzzleFactory;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Masks _masks;
        [SerializeField] private PuzzleMaterials _puzzleMaterials;
        [SerializeField] private Grab _grab;
        [SerializeField] private SetScene _setScene;

        private IComplition _levelComplition;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private void Awake()
        {
            if(CoroutineHandler == null)
                CoroutineHandler = this;

            var puzzle = _puzzleFactory.GetPuzzle(0);
            var createPuzzle = Instantiate(puzzle);

            _grab.Construct(_masks);
            var puzzleDependency = new PuzzleDependency(_masks); 
            _levelComplition = createPuzzle.Construct(puzzleDependency);
            _levelComplition.OnChange += OnLevelProgress;
            _gameUI.Construct();
        }

        private void OnDestroy()
        {
            _levelComplition.OnChange -= OnLevelProgress;
        }

        [ContextMenu(nameof(SetScene))]
        public void SetScene()
        {
            _setScene.Set(_puzzleMaterials);
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

