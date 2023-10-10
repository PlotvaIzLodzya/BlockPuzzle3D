using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.BlockPuzzle
{
    public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CellView _cellView;

        [ContextMenu(nameof(Construct))]
        public void Construct()
        {
            _cellView.Construct(GetComponentInChildren<SpriteRenderer>());
            EditorUtility.SetDirty(gameObject);
        }

        public void DestoyInEditor()
        {
            DestroyImmediate(gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _cellView.Hover();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cellView.UnHover();
        }
    }

    [Serializable]
    public class CellView
    {
        [SerializeField] private Color _hoveredColor;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _defaulColor;

        public void Construct(SpriteRenderer meshRenderer)
        {
            _renderer = meshRenderer;
            _defaulColor = _renderer.sharedMaterial.color;
        }

        public void Hover()
        {
            _renderer.material.color = _hoveredColor;
        }

        public void UnHover()
        {
            _renderer.material.color = _defaulColor;
        }
    }
}

