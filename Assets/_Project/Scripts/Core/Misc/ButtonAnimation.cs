using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Core.Misc
{
    public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private CanvasRenderer _renderer;
        [SerializeField] private Color _defaultColor = new Color(1, 1, 1, 0);
        [SerializeField] private Color _hoverColor = new Color(1, 1, 1, 0.025f);
        [SerializeField] private Color _downColor = new Color(1, 1, 1, 0.05f);

        private bool _hover;
        private bool _down;

        private void OnValidate()
        {
            if (!_renderer)
                _renderer = GetComponent<CanvasRenderer>();
            if (_renderer)
                _renderer.SetColor(_defaultColor);
        }

        private void OnEnable()
        {
            _hover = _down = false;
            UpdateState(true);
        } 

        private void OnDisable() => DOTween.Kill(this);

        private void UpdateState(bool immediate)
        {
            DOTween.Kill(this);
            var color = _defaultColor;
            if (_hover)
                color = _down ? _downColor : _hoverColor;

            if (immediate)
            {
                _renderer.SetColor(color);
            }
            else
            {
                DOVirtual.Color(_renderer.GetColor(), color, 
                        0.1f, c => _renderer.SetColor(c))
                    .SetTarget(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            UpdateState(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            _down = false;
            UpdateState(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            UpdateState(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            UpdateState(false);
        }
    }
}