using System;

namespace Assets.BlockPuzzle
{
    public interface IComplition
    {
        public bool Completed { get;}
        public event Action OnChange;
    }
}

