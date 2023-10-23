using Assets.BlockPuzzle.Proggression;
using System.Collections;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{

    public class GameUI: MonoBehaviour
    {
        [SerializeField] private WinScreen _winScreen;
        [SerializeField] private MainMenu _menu;
        [SerializeField] private ProgressionView _progression;

        public void Construct(PuzzleViewDependency puzzleViewDependency, PlayerProgresion playerProgresion)
        {
            _progression.Construct(playerProgresion);
            _winScreen.Construct(playerProgresion);
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

