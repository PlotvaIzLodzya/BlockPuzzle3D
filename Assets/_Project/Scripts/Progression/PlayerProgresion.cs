using System;
using System.Diagnostics;

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
            _playerLevel = new ValueHandler(1, dependency.MaxLevel, dependency.LevelGUID);
        }

        public void AddExp(float exp)
        {
            var totalExp = _playerExperience.Value + exp;
            _playerExperience.Increase(exp);

            if(_playerExperience.Value >= _playerExperience.MaxValue)
            {
                var additionalExp = totalExp - _playerExperience.MaxValue;

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

