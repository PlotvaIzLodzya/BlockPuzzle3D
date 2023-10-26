using Assets.BlockPuzzle.Proggression;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{

    public class GameUI: MonoBehaviour
    {
        [SerializeField] private WinScreen _winScreen;
        [SerializeField] private MainMenu _menu;
        [SerializeField] private ProgressionView _progression;
        [SerializeField] private ControllsHUD _controllsHUD;

        public void Construct(PuzzleViewDependency puzzleViewDependency,ControllsHUDDependency controllsHUDDependency, PlayerProgresion playerProgresion)
        {
            _progression.Construct(playerProgresion);
            _winScreen.Construct(playerProgresion);
            _menu.Construct(puzzleViewDependency);
            _controllsHUD.Construct(controllsHUDDependency);
            _winScreen.Hide();
            _controllsHUD.Hide();
        }

        public void HideMainMenu()
        {
            _menu.Hide();
            _controllsHUD.Show();
        }

        public void ShowMainMenu()
        {
            _menu.Show();
            _controllsHUD.Hide();
        }

        public void OnLevelEnd()
        {
            _winScreen.Show();
        }
    }
}

