using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class MainMenu : Panel
    {
        [SerializeField] private ScrollRect _scrollView;
        [SerializeField] private PuzzleListView _letterPuzzleList;
        [SerializeField] private PuzzleListView _squarePuzzleList;
        [SerializeField] private PuzzleListView _49PuzzleList;
        [SerializeField] private GameButton _menuButton;
        [SerializeField] private GameButton _closeButton;

        public void Construct(PuzzleViewDependency puzzleViewDependency)
        {
            base.Show();
            _menuButton.Hide();
            _closeButton.Hide();

            _letterPuzzleList.Construct(puzzleViewDependency.LetterPuzzles);
            _squarePuzzleList.Construct(puzzleViewDependency.SquarePuzzles);
            _49PuzzleList.Construct(puzzleViewDependency.Puzzles49);
            _menuButton.Button.onClick.AddListener(Show);
            _closeButton.Button.onClick.AddListener(Hide);

            _scrollView.verticalNormalizedPosition = 1;
        }

        public override void Show()
        {
            base.Show();
            _closeButton.Show();
            _menuButton.Hide();
        }

        public override void Hide()
        {
            base.Hide();
            _menuButton.Show();
            _closeButton.Hide();
        }
    }
}

