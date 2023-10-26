using System;

namespace Assets.BlockPuzzle.HUD
{
    public struct ControllsHUDDependency
    {
        public Action RotateToLeft;
        public Action RotateToRight;
        public Action Flip;
        public Action Place;

        public ControllsHUDDependency(Action rotateToLeft, Action rotateToRight, Action flip, Action place)
        {
            RotateToLeft = rotateToLeft;
            RotateToRight = rotateToRight;
            Flip = flip;
            Place = place;
        }
    }
}

