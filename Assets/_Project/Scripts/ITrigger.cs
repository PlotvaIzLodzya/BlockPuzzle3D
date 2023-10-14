using UnityEngine;

namespace Assets.BlockPuzzle
{
    public interface ITrigger
    {
        public Transform transform { get; }
        public bool IsTriggering { get; }
        public bool IsTriggered { get; }
    }
}