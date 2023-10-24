using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class Panel : MonoBehaviour
    {
        public bool IsActive => gameObject.activeInHierarchy;

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

