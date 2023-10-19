using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Assets.BlockPuzzle.HUD
{

    public class StartPuzzleView: GameButton
    {
        [SerializeField] private TMP_Text _name;

        public void Construct(StartPuzzleViewDependency dependency)
        {
            _name.text = $"{dependency.PuzzleName}";
            Button.onClick.AddListener(() => dependency.LoadPuzzle());
        }
    }
}

