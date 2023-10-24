using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Proggression;
using Assets.BlockPuzzle.Puzzles;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Game: MonoBehaviour
    {
        [SerializeField, ReadOnly] private string _levelGUID;
        [SerializeField, ReadOnly] private string _expGuid;

        [SerializeField] private PuzzleFactory _puzzleFactory;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Masks _masks;
        [SerializeField] private PuzzleMaterials _puzzleMaterials;
        [SerializeField] private Grab _grab;
        [SerializeField] private SetScene _setScene;

        private IComplition _levelComplition;
        private Puzzle _curentPuzzle;
        private PlayerProgresion _playerProgression;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private void Awake()
        {
            var progressionDependency = new PlayerProgressionDependency(_expGuid, _levelGUID, 70, 17);
            _playerProgression = new PlayerProgresion(progressionDependency);

            if(CoroutineHandler == null)
                CoroutineHandler = this;

            _grab.Construct(_masks);

            ConstructUI();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                OnLevelComplete();
        }

        [ContextMenu(nameof(Generate))]
        public void Generate()
        {
            _levelGUID = System.Guid.NewGuid().ToString();
            _expGuid = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }

        private void ConstructUI()
        {
            var letterDependency = CreateDependency<LetterPuzzle>();
            var squareDependency = CreateDependency<SquarePuzzle>();
            var puzzle49Dependency = CreateDependency<Puzzle49>();

            var puzzleViewDependency = new PuzzleViewDependency(letterDependency, squareDependency, puzzle49Dependency);

            _gameUI.Construct(puzzleViewDependency, _playerProgression);
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

            CreatePuzzle(puzzle);
        }

        private void CreatePuzzle(Puzzle puzzlePrefab)
        {
            var createdPuzzle = Instantiate(puzzlePrefab);

            var puzzleDependency = new PuzzleDependency(_masks);
            _levelComplition = createdPuzzle.Construct(puzzleDependency);
            _curentPuzzle = createdPuzzle;
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
                OnLevelComplete();
            }
        }

        public void OnLevelComplete()
        {
            _levelComplition.OnChange -= OnLevelProgress;

            if(_curentPuzzle.WasCompletedAlready == false)
                _playerProgression.AddExp(_curentPuzzle.Experience);

            _gameUI.OnLevelEnd();
        }
    }
}

