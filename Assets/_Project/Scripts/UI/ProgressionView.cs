using Assets.BlockPuzzle.Proggression;
using System.Collections;
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
            float time = 2f;
            float speed = _progression.MaxExperience / time;
            float targetValue = _progression.Experience;

            yield return new WaitForSeconds(0.2f);

            if(targetValue < _slider.value)
            {
                while (_slider.value < _progression.MaxExperience)
                {
                    _slider.value = Mathf.MoveTowards(_slider.value, _progression.MaxExperience, speed*Time.deltaTime);

                    yield return null;
                }

                _slider.value = 0;

                UpdateRank();
            }


            while(_slider.value < targetValue)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, targetValue, speed * Time.deltaTime);

                yield return null;
            }

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            _slider.maxValue = _progression.MaxExperience;
            _slider.value = _progression.Experience;

            UpdateRank();
        }

        private void UpdateRank()
        {
            _level.text = $"{Ranks.GetRank(_progression.Level)}";
        }
    }
}

