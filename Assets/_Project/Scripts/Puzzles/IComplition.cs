using System;

namespace Assets.BlockPuzzle
{
    public interface IComplition
    {
        public bool IsCompleted { get;}
        public event Action OnChange;
    }
}

