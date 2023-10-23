namespace Assets.BlockPuzzle.Proggression
{
    public interface ICount
    {
        public float Value { get; }
        public float MaxValue { get; }
        public void Increase(float value);
    }
}

