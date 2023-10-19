using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Assets.BlockPuzzle.HUD
{

    public class StartPuzzleView: GameButton
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _completedSign;

        public void Construct(StartPuzzleDependency dependency)
        {
            _completedSign.gameObject.SetActive(dependency.IsCompleted);
            _name.text = $"{dependency.PuzzleName}";
            Button.onClick.AddListener(() => dependency.LoadPuzzle());
        }
    }
}

