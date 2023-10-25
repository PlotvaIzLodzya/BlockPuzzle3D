using Assets.BlockPuzzle.Localization;
using Assets.BlockPuzzle.Proggression;
using TMPro;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class WinScreen : Panel
    {
        [SerializeField] private ProgressionView _progression;
        [SerializeField] private TMP_Text _complete;

        public void Construct(PlayerProgresion playerProgresion)
        {
            _progression.ConstructPassive(playerProgresion);
            _complete.text = InGameTexts.Complete;
        }

        public override void Show()
        {
            base.Show();
            _progression.UpdateView();
        }

    }
}

