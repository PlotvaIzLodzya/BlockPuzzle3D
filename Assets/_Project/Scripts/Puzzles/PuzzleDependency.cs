using UnityEngine;

namespace Assets.BlockPuzzle.Dependency
{

    public struct PuzzleDependency
    {
        public Masks Masks { get; private set; }
        public Color DefaultColor { get; private set; }
        public float CompitionTime { get; private set; }
        public float StepAngle { get; private set; }
        public float RotationTime { get; private set; }
        public float StepTime { get; private set; }
        public float StepSize { get; private set; }

        public PuzzleDependency(Masks masks, Color color, float complitionTime)
        {
            Masks = masks;
            CompitionTime = complitionTime;
            DefaultColor = color;
            StepAngle = 45;
            RotationTime = 0.2f;
            StepTime = 0.2f;
            StepSize = 0.2f;
        }

        public PuzzleDependency SetStepAngle(float stepAngle)
        {
            StepAngle = stepAngle;

            return this;
        }

        public PuzzleDependency SetStepSize(float stepSize)
        {
            StepSize = stepSize;

            return this;
        }

        public PuzzleDependency SetRotaionTime(float time)
        {
            RotationTime = time;

            return this;
        }

        public PuzzleDependency SetStepTime(float time)
        {
            StepTime = time;

            return this;
        }
    }
}

