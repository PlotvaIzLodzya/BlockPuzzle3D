using Assets.BlockPuzzle.Complition;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.Localization;
using Assets.BlockPuzzle.SaveLoad;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class Puzzle : GUIDObject
    {
        [field: SerializeField] public float Experience { get; private set; }
        [field: SerializeField] public InGameText Name { get; private set; }

        [SerializeField] private float _angleStep = 45;
        [SerializeField] private float _stepTime = 0.2f;
        [SerializeField] private float _rotationTime = 0.2f;
        [SerializeField] private float _stepSize = 0.2f;

        private IShape[] _shapes;
        private LevelComplition _levelComplition;

        public int ShapesCount => _shapes.Length;
        public bool IsCompleted => SaveService.HasSave(GUID);
        public bool WasCompletedAlready { get; private set; }

        public void GetFromDependencieFromChildren()
        {
            _shapes= GetComponentsInChildren<IShape>();
            _levelComplition = GetComponentInChildren<LevelComplition>();
        }

        public IComplition Construct(PuzzleDependency puzzleDependency)
        {
            GetFromDependencieFromChildren();
            WasCompletedAlready = IsCompleted;

            foreach (IShape shape in _shapes)
            {
                puzzleDependency.SetStepAngle(_angleStep)
                                .SetStepTime(_stepTime)
                                .SetStepSize(_stepSize)
                                .SetRotaionTime(_rotationTime);

                shape.Construct(puzzleDependency);
            }
            
            _levelComplition.Construct(_shapes.Length, GUID);
            _levelComplition.OnComplete += OnComplition;
            return _levelComplition;
        }

        private void OnDestroy()
        {
            _levelComplition.OnComplete -= OnComplition;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnComplition()
        {
            foreach (var shape in _shapes)
            {
                shape.OnComplete();
            }
        }
    }
}