using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class GameButton : Panel
    {
        [field: SerializeField] public Button Button { get; private set; }
    }

    public struct StartPuzzleViewDependency
    {
        public string PuzzleName { get; private set; }
        public Action LoadPuzzle { get; private set; }

        public StartPuzzleViewDependency(string puzzleName, Action loadPuzzle)
        {
            PuzzleName = puzzleName;
            LoadPuzzle = loadPuzzle;
        }
    }

    public struct PuzzleViewDependency
    {
        public IEnumerable<StartPuzzleViewDependency> LetterPuzzles;

        public PuzzleViewDependency(IEnumerable<StartPuzzleViewDependency> letterPuzzles)
        {
            LetterPuzzles = letterPuzzles;
        }
    }

    public class GameUI: MonoBehaviour
    {
        [SerializeField] private Panel _winScreen;
        [SerializeField] private PuzzleListView _letterPuzzleList;

        public void Construct(PuzzleViewDependency puzzleViewDependency)
        {
            _letterPuzzleList.Construct(puzzleViewDependency.LetterPuzzles);
            _winScreen.Hide();
        }

        public void OnLevelEnd()
        {
            _winScreen.Show();
        }
    }
}

