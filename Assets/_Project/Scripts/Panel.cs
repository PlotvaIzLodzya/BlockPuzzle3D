using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class Panel : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

