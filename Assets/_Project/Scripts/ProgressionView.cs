using Assets.BlockPuzzle.Proggression;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class ProgressionView : Panel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _level;

        private PlayerProgresion _progression;

        public void Construct(PlayerProgresion playerProgresion)
        {
            _progression = playerProgresion;
            _progression.OnExpChange += UpdateView;
            UpdateView();
        }

        private void OnDestroy()
        {
            _progression.OnExpChange -= UpdateView;
        }

        public void UpdateView()
        {
            _slider.maxValue = _progression.MaxExperience;
            _slider.value = _progression.Experience;
            _level.text = $"{_progression.Level}";
        }
    }
}

