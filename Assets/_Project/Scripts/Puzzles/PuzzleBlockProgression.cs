using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class PuzzleBlockProgression : Panel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _progress;

        public void UpdateInfo(int total, int completed)
        {
            _progress.text = $"{completed}/{total}";
            _slider.maxValue = total;
            _slider.value = completed;
        }
    }
}

