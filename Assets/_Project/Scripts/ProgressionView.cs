using Assets.BlockPuzzle.Localization;
using Assets.BlockPuzzle.Proggression;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public static class Ranks
    {
        public static string GetRank(int level)
        {
            var rank = InGameTexts.Rookie;

            rank = level switch
            {
                1 => InGameTexts.Rookie,
                2 => InGameTexts.Beginner,
                3 => InGameTexts.Experienced,
                4 => InGameTexts.Expert,
                5 => InGameTexts.Professional,
                6 => InGameTexts.BigBrain,
                7 => InGameTexts.MegaMind,
                _ => rank
            };

            return rank;
        }
    }

    public class ProgressionView : Panel
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _level;

        private PlayerProgresion _progression;

        public void Construct(PlayerProgresion playerProgresion)
        {
            ConstructPassive(playerProgresion);
            _progression.OnExpChange += UpdateView;
        }

        public void ConstructPassive(PlayerProgresion playerProgresion)
        {
            _progression = playerProgresion;

            UpdateInfo();
        }

        private void OnDestroy()
        {
            _progression.OnExpChange -= UpdateView;
        }

        public void UpdateView()
        {
            StartCoroutine(Animating());
        }

        private IEnumerator Animating()
        {
            float time = 0.75f;
            float elapsedTime = 0;
            float targetValue = _progression.Experience;
            float startValue = _slider.value;

            yield return new WaitForSeconds(0.2f);

            while(elapsedTime< time)
            {
                elapsedTime += Time.deltaTime;

                _slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime/time);

                yield return null;
            }

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            _slider.maxValue = _progression.MaxExperience;
            _slider.value = _progression.Experience;
            Debug.Log(_progression.Level);
            _level.text = $"{Ranks.GetRank(_progression.Level)}";
        }
    }
}

