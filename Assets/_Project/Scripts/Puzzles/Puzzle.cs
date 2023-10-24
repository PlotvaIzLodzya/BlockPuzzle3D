using Assets.BlockPuzzle.Complition;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.Localization;
using Assets.BlockPuzzle.SaveLoad;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public class Puzzle : GUIDObject
    {
        [field: SerializeField] public float Experience { get; private set; }
        [field: SerializeField] public InGameText Name { get; private set; }

        [SerializeField] private float _angleStep = 45;
        [SerializeField] private float _stepTime = 0.2f;
        [SerializeField] private float _rotationTime = 0.2f;
        [SerializeField] private float _stepSize = 0.2f;

        private IShape[] _shape;
        private LevelComplition _levelComplition;

        public int ShapesCount => _shape.Length;
        public bool IsCompleted => SaveService.HasSave(GUID);
        public bool WasCompletedAlready { get; private set; }

        public void GetFromDependencieFromChildren()
        {
            _shape= GetComponentsInChildren<IShape>();
            _levelComplition = GetComponentInChildren<LevelComplition>();
        }

        public IComplition Construct(PuzzleDependency puzzleDependency)
        {
            GetFromDependencieFromChildren();
            WasCompletedAlready = IsCompleted;

            foreach (IShape shape in _shape)
            {
                puzzleDependency.SetStepAngle(_angleStep)
                                .SetStepTime(_stepTime)
                                .SetStepSize(_stepSize)
                                .SetRotaionTime(_rotationTime);

                shape.Construct(puzzleDependency);
            }
            
            _levelComplition.Construct(_shape.Length, GUID);

            return _levelComplition;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}