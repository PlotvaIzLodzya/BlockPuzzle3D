using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.BlockPuzzle.HUD
{
    public class OpenMenuButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("click");
        }
    }
}

