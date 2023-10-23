using Assets.BlockPuzzle.Proggression;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class WinScreen : Panel
    {
        [SerializeField] private ProgressionView _progression;

        public void Construct(PlayerProgresion playerProgresion)
        {
            _progression.ConstructPassive(playerProgresion);
        }

        public override void Show()
        {
            base.Show();
            _progression.UpdateView();
        }

    }
}

