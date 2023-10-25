using UnityEngine;
using System.Collections.Generic;

namespace Assets.BlockPuzzle.View
{
    public class GlowObject : MonoBehaviour
    {
        public float LerpTime = 0.5f;
        public Color GlowColor = Color.green;

        private IHighlightable _highlightable;
        private float elapsedTime = 0f;
        private bool _hovered;

        public Renderer[] Renderers
        {
            get;
            private set;
        }

        public Color CurrentColor
        {
            get { return _currentColor; }
        }

        private List<Material> _materials = new List<Material>();
        private Color _currentColor;
        private Color _targetColor;

        void Start()
        {
            Construct(GetComponent<IHighlightable>());
            Renderers = GetComponentsInChildren<Renderer>();

            foreach (var renderer in Renderers)
            {
                _materials.AddRange(renderer.materials);
            }
        }

        public void Construct(IHighlightable highlightable)
        {
            _highlightable = highlightable;
        }

        private void OnMouseEnter()
        {
            _hovered = true;
        }

        public void OnMouseExit()
        {
            _hovered = false;
        }

        /// <summary>
        /// Loop over all cached materials and update their color, disable self if we reach our target color.
        /// </summary>
        private void Update()
        {
            float lerp = elapsedTime / LerpTime;

            if ((_hovered || _highlightable.IsRequireHighlight) && _highlightable.CanBeHighlighted)
            {
                elapsedTime += Time.deltaTime;
                _targetColor = GlowColor;
            }
            else
            {
                _currentColor = Color.black;
                elapsedTime = 0f;
            }

            _currentColor = Color.Lerp(_currentColor, _targetColor, lerp);

            if (lerp >= 1)
            {
                return;
            }


            for (int i = 0; i < _materials.Count; i++)
            {
                _materials[i].SetColor("_GlowColor", _currentColor);
            }
        }
    }
}
