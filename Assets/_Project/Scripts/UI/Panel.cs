using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class Panel : MonoBehaviour
    {
        public bool IsActive => gameObject.activeInHierarchy;
        public RectTransform RectTransform { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

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

