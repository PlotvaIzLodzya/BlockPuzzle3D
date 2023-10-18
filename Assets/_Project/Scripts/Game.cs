using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.HUD;
using Assets.BlockPuzzle.Puzzles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Puzzle GetPuzzle(int index)
        {
            index = Mathf.Clamp(index, 0, _puzzlePrefabs.Length-1);

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
        [SerializeField] private int _id;

        private IComplition _levelComplition;

        public static MonoBehaviour CoroutineHandler { get; private set; }

        private void Awake()
        {
            if(CoroutineHandler == null)
                CoroutineHandler = this;

            var puzzle = _puzzleFactory.GetPuzzle(_id);
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

