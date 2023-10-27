using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverColorize: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _defaultColor;

    private void Awake()
    {
        foreach (var image in _images)
        {
            image.color = _defaultColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var image in _images)
        {
            image.DOColor(_hoverColor, duration: 0.2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (var image in _images)
        {
            image.DOColor(_defaultColor, duration: 0.2f);
        }
    }
}
