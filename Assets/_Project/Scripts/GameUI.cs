using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class GameUI: MonoBehaviour
    {
        [SerializeField] private Panel _winScreen;


        public void Construct()
        {
            _winScreen.Hide();
        }

        public void OnLevelEnd()
        {
            _winScreen.Show();
        }
    }
}

