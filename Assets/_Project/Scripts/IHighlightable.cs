﻿namespace Assets.BlockPuzzle.View
{
    public interface IHighlightable
    {
        public bool IsRequireHighlight { get; }
        public bool CanBeHighlighted { get; }
    }
}