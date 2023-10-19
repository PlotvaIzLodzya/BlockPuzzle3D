using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class PuzzleFactory
    {
        [SerializeField] private Puzzle[] _puzzlePrefabs;

        public IEnumerable<Puzzle> GetPuzzles<TPuzzle>() where TPuzzle : Puzzle
        {
            var puzzles = new List<Puzzle>();

            foreach (var puzzle in _puzzlePrefabs)
            {
                if (puzzle is TPuzzle)
                    puzzles.Add(puzzle);
            }

            return puzzles;
        }

        public Puzzle GetRandomPuzzle<TPuzzle> () where TPuzzle : Puzzle
        {
            var puzzles = GetPuzzles<TPuzzle>();

            var index = UnityEngine.Random.Range(0, puzzles.Count());

            return puzzles.ElementAt(index);
        }

        public Puzzle GetPuzzle(int index)
        {
            return _puzzlePrefabs[index];
        }

        public bool TryGetPuzzle(string guid, out Puzzle puzzle)
        {
            puzzle = GetPuzzle(guid);

            return puzzle != null;
        }

        public Puzzle GetPuzzle(string guid)
        {
            return _puzzlePrefabs.First(puzzle => puzzle.GUID.Equals(guid));
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
        private Puzzle _curentPuzzle;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private void Awake()
        {
            if(CoroutineHandler == null)
                CoroutineHandler = this;

            _grab.Construct(_masks);

            ConstructUI();
        }

        private void ConstructUI()
        {
            var letterDependency = CreateDependency<LetterPuzzle>();
            var squareDependency = CreateDependency<SquarePuzzle>();
            var puzzle49Dependency = CreateDependency<Puzzle49>();

            var puzzleViewDependency = new PuzzleViewDependency(letterDependency, squareDependency, puzzle49Dependency);

            _gameUI.Construct(puzzleViewDependency);
        }

        private IEnumerable<StartPuzzleDependency> CreateDependency<TPuzzle>() where TPuzzle: Puzzle
        {
            var dependencyList = new List<StartPuzzleDependency>();

            var puzzles = _puzzleFactory.GetPuzzles<TPuzzle>();

            foreach (var puzzle in puzzles)
            {
                dependencyList.Add(new StartPuzzleDependency
                    (
                        puzzle,
                        () => CreatePuzzle(puzzle.GUID))
                    );
            }

            return dependencyList;
        }

        private void CreatePuzzle(string guid)
        {
            if (_curentPuzzle != null)
                _curentPuzzle.Destroy();

            var puzzle = _puzzleFactory.GetPuzzle(guid);
            _curentPuzzle = puzzle;

            var createPuzzle = Instantiate(puzzle);

            var puzzleDependency = new PuzzleDependency(_masks);
            _levelComplition = createPuzzle.Construct(puzzleDependency);
            _levelComplition.OnChange += OnLevelProgress;
            _gameUI.HideMainMenu();
        }

        private void OnDestroy()
        {
            if(_levelComplition != null)
                _levelComplition.OnChange -= OnLevelProgress;
        }

        [ContextMenu(nameof(SetScene))]
        public void SetScene()
        {
            _setScene.Set(_puzzleMaterials);
        }

        private void OnLevelProgress()
        {
            if(_levelComplition.IsCompleted)
            {
                _levelComplition.OnChange -= OnLevelProgress;
                _gameUI.OnLevelEnd();
            }
        }
    }
}

