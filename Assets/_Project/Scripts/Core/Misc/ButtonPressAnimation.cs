using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Core.Misc
{
    public class ButtonPressAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Top")]
        [SerializeField] private RectTransform _topRectTransform;
        [SerializeField] private Vector2 _defaultTopAnchoredPosition;
        [SerializeField] private Vector2 _downTopAnchoredPosition;
        [SerializeField] private Image _topImage;
        [SerializeField] private Sprite _defaultTopSprite;
        [SerializeField] private Sprite _hoverTopSprite;
        [SerializeField] private Sprite _downTopSprite;
        [Header("Bottom")]
        [SerializeField] private Image _bottomImage;
        [SerializeField] private Sprite _defaultBottomSprite;
        [SerializeField] private Sprite _hoverBottomSprite;
        [SerializeField] private Sprite _downBottomSprite;
        [Header("Graphics")]
        [SerializeField] private CanvasRenderer _graphics;
        [SerializeField] private Color _defaultTint = Color.white;
        [SerializeField] private Color _hoverTint = Color.white;
        [SerializeField] private Color _downTint = Color.white;
        [Header("Animations")]
        [SerializeField] private float _transitionDuration = 0.1f;
        
        private bool _hover;
        private bool _down;

        private void OnEnable()
        {
            _hover = _down = false;
            UpdateState(true);
        } 

        private void OnDisable() => DOTween.Kill(this);

        private void UpdateState(bool immediate)
        {
            DOTween.Kill(this);
            
            var topSprite = _defaultTopSprite;
            var bottomSprite = _defaultBottomSprite;
            var topAnchoredPosition = _defaultTopAnchoredPosition;
            var tint = _defaultTint;

            if (_down)
            {
                topSprite = _downTopSprite;
                bottomSprite = _downBottomSprite;
                topAnchoredPosition = _downTopAnchoredPosition;
                tint = _downTint;
            }
            else if (_hover)
            {
                topSprite = _hoverTopSprite;
                bottomSprite = _hoverBottomSprite;
                tint = _hoverTint;
            }

            _topImage.sprite = topSprite;
            _bottomImage.sprite = bottomSprite;

            if (immediate)
            {
                _topRectTransform.anchoredPosition = topAnchoredPosition;
                _graphics.SetColor(tint);
            }
            else
            {
                _topRectTransform.DOAnchorPos(topAnchoredPosition, _transitionDuration)
                    .SetTarget(this);
                DOVirtual.Color(_graphics.GetColor(), tint, _transitionDuration,
                    c => _graphics.SetColor(c))
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