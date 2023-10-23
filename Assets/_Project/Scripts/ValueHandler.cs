using System;
using UnityEngine;

namespace Assets.BlockPuzzle.Proggression
{
    public class ValueHandler : ICount
    {
        private float _value;
        private float _maxValue;
        private float _minValue;
        private string _saveName;

        public float Value => _value;
        public float MaxValue => _maxValue;

        public event Action ValueChanged;

        public ValueHandler(float minValue, float maxValue, string saveName)
        {
            _saveName = saveName;
            _minValue = minValue;
            _maxValue = maxValue;
            _value = _minValue;
            LoadAmount();
        }

        public void SetDefaultValue()
        {
            _value = 0;
            ChangeAmount(0);
        }

        public void Increase(float value)
        {
            ChangeAmount(value);
        }

        public bool TryDecrease(float value)
        {
            if (IsEnough(value))
            {
                Decrease(value);
                return true;
            }

            return false;
        }

        public bool IsEnough(float value)
        {
            return _value >= value;
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(_saveName);
        }

        public float LoadAmount()
        {
            _value = _minValue;

            if (PlayerPrefs.HasKey(_saveName))
                _value = PlayerPrefs.GetFloat(_saveName);

            return _value;
        }

        private void Decrease(float value)
        {
            ChangeAmount(-value);
        }

        private void ChangeAmount(float value)
        {
            _value += value;

            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            ValueChanged?.Invoke();

            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(_saveName, _value);
        }
    }
}

