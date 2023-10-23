using System;
using TMPro;

namespace Assets.BlockPuzzle.Proggression
{
    public class PlayerProgresion
    {
        private ValueHandler _playerExperience;
        private ValueHandler _playerLevel;

        public int Level => (int)_playerLevel.Value;
        public float Experience => _playerExperience.Value;
        public float MaxExperience => _playerExperience.MaxValue;

        public event Action OnExpChange;
        public event Action OnLevelChange;

        public PlayerProgresion(PlayerProgressionDependency dependency)
        {
            _playerExperience = new ValueHandler(0, dependency.MaxExp, dependency.ExpGUID);
            _playerLevel = new ValueHandler(0, dependency.MaxLevel, dependency.LevelGUID);
        }

        public void AddExp(float exp)
        {
            _playerExperience.Increase(exp);

            if(_playerExperience.Value >= _playerExperience.MaxValue)
            {
                var additionalExp = _playerExperience.Value - _playerExperience.MaxValue;
                LevelUp(additionalExp);
            }

            OnExpChange?.Invoke();
        }

        private void LevelUp(float additionalExp)
        {
            _playerExperience.SetDefaultValue();
            _playerLevel.Increase(1);
            OnLevelChange?.Invoke();
            AddExp(additionalExp);
        }
    }
}

