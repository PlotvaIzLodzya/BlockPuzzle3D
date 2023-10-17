namespace Assets.BlockPuzzle.Dependency
{

    public class PuzzleDependency
    {
        public Masks Masks { get; private set; }
        public float StepAngle { get; private set; }
        public float RotationTime { get; private set; }
        public float StepTime { get; private set; }

        public PuzzleDependency(Masks masks)
        {
            Masks = masks;
        }

        public PuzzleDependency SetStepAngle(float stepAngle)
        {
            StepAngle = stepAngle;

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

