using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class Masks
    {
        [field: SerializeField] public LayerMask Ground { get; private set; }
        [field: SerializeField] public LayerMask LevelComplition { get; private set; }
        [field: SerializeField] public LayerMask Grab { get; private set; }
        [field: SerializeField] public LayerMask GroundProjection { get; private set; }
    }
}

