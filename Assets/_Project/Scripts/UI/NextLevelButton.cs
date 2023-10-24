using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class NextLevelButton : Panel
    {
        [field: SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

