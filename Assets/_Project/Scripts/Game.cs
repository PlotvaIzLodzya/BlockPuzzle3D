using Agava.YandexGames;
using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Proggression;
using Assets.BlockPuzzle.Puzzles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Game: MonoBehaviour
    {
#if(UNITY_EDITOR)
        [ReadOnly]
#endif
        [SerializeField] private string _levelGUID;
#if (UNITY_EDITOR)
        [ReadOnly]
#endif
        [SerializeField] private string _expGuid;

        [SerializeField] private PuzzleFactory _puzzleFactory;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Masks _masks;
        [SerializeField] private PuzzleMaterials _puzzleMaterials;
        [SerializeField] private Grab _grab;
        [SerializeField] private SetScene _setScene;
        [SerializeField] private float _complitionDelay;
        [SerializeField] private ParticleSystem _endParticles;

        private IComplition _levelComplition;
        private Puzzle _curentPuzzle;
        private PlayerProgresion _playerProgression;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private IEnumerator Start()
        {
#if (!UNITY_EDITOR)
            yield return YandexGamesSdk.Initialize();
#endif

            var progressionDependency = new PlayerProgressionDependency(_expGuid, _levelGUID, 70, 17);
            _playerProgression = new PlayerProgresion(progressionDependency);

            if(CoroutineHandler == null)
                CoroutineHandler = this;

            _grab.Construct(_masks);
            ConstructUI();

            yield return null;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                OnLevelComplete();
        }

        [ContextMenu(nameof(GenerateGUID))]
        public void GenerateGUID()
        {
#if(UNITY_EDITOR)
            _levelGUID = System.Guid.NewGuid().ToString();
            _expGuid = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        [ContextMenu(nameof(SetScene))]
        public void SetScene()
        {
            _setScene.Set(_puzzleMaterials);
        }

        private void ConstructUI()
        {
            var letterDependency = CreateDependency<LetterPuzzle>(Difficulty.Easy);
            var squareDependency = CreateDependency<SquarePuzzle>(Difficulty.Medium);
            var puzzle49Dependency = CreateDependency<Puzzle49>(Difficulty.Hard);

            var puzzleViewDependency = new PuzzleViewDependency(letterDependency, squareDependency, puzzle49Dependency);

            _gameUI.Construct(puzzleViewDependency, _playerProgression);
        }

        private IEnumerable<StartPuzzleDependency> CreateDependency<TPuzzle>(Difficulty difficulty) where TPuzzle: Puzzle
        {
            var dependencyList = new List<StartPuzzleDependency>();

            var puzzles = _puzzleFactory.GetPuzzles<TPuzzle>();

            foreach (var puzzle in puzzles)
            {
                var dependency = new StartPuzzleDependency
                    (
                        puzzle,
                        () => CreatePuzzle(puzzle.GUID),
                        difficulty
                    );

                dependencyList.Add(dependency);
            }

            return dependencyList;
        }

        private void CreatePuzzle(string guid)
        {
            if (_curentPuzzle != null)
                _curentPuzzle.Destroy();

            var puzzle = _puzzleFactory.GetPuzzle(guid);

            StartGame(puzzle);
        }

        private void StartGame(Puzzle puzzlePrefab)
        {

            CreatePuzzle(puzzlePrefab);

            _gameUI.HideMainMenu();

#if(!UNITY_EDITOR)
            InterstitialAd.Show();
#endif
        }

        public void CreatePuzzle(Puzzle puzzlePrefab)
        {
            var createdPuzzle = Instantiate(puzzlePrefab);

            var puzzleDependency = new PuzzleDependency(_masks, _puzzleMaterials.ShapeMaterial.color, _complitionDelay);
            _levelComplition = createdPuzzle.Construct(puzzleDependency);
            _curentPuzzle = createdPuzzle;
            _levelComplition.OnChange += OnLevelProgress;
        }

        private void OnDestroy()
        {
            if(_levelComplition != null)
                _levelComplition.OnChange -= OnLevelProgress;
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

            _endParticles.Play();

            StartCoroutine(Wait(_complitionDelay, () =>
            {
                _gameUI.OnLevelEnd();
            }));
        }

        public IEnumerator Wait(float duration, Action action)
        {
            yield return new WaitForSeconds(duration);
            action();
        }
    }
}

