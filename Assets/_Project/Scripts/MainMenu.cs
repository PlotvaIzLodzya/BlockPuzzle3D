using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.BlockPuzzle.HUD
{
    public class MainMenu : Panel
    {
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private PuzzleListView _letterPuzzleList;
        [SerializeField] private PuzzleListView _squarePuzzleList;
        [SerializeField] private PuzzleListView _49PuzzleList;

        public void Construct(PuzzleViewDependency puzzleViewDependency)
        {
            Show();
            _letterPuzzleList.Construct(puzzleViewDependency.LetterPuzzles);
            _squarePuzzleList.Construct(puzzleViewDependency.SquarePuzzles);
            _49PuzzleList.Construct(puzzleViewDependency.Puzzles49);

            _scrollView.verticalNormalizedPosition = 1;
        }
    }
}

