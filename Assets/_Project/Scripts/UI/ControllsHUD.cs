using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class ControllsHUD : Panel
    {
        [SerializeField] private GameButton _rotateLeft;
        [SerializeField] private GameButton _rotateRight;
        [SerializeField] private GameButton _rotateFlip;
        [SerializeField] private GameButton _rotatePlace;

        public void Construct(ControllsHUDDependency dependency)
        {
            _rotateLeft.Button.onClick.AddListener(() => dependency.RotateToLeft());
            _rotateRight.Button.onClick.AddListener(() => dependency.RotateToRight());
            _rotateFlip.Button.onClick.AddListener(() => dependency.Flip());
            _rotatePlace.Button.onClick.AddListener(() => dependency.Place());
        }
    }
}

