namespace Assets.BlockPuzzle.Dependency
{

    public class PuzzleDependency
    {
        public Masks Masks { get; private set; }

        public PuzzleDependency(Masks masks)
        {
            Masks = masks;
        }

    }
}

