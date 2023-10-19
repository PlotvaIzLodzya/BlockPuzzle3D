using System.Collections;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class GameUI: MonoBehaviour
    {
        [SerializeField] private Panel _winScreen;
        [SerializeField] private MainMenu _menu;

        public void Construct(PuzzleViewDependency puzzleViewDependency)
        {
            _menu.Construct(puzzleViewDependency);
            _winScreen.Hide();
        }

        public void HideMainMenu()
        {
            _menu.Hide();
        }

        public void ShowMainMenu()
        {
            _menu.Show();
        }

        public void OnLevelEnd()
        {
            _winScreen.Show();
        }
    }
}

