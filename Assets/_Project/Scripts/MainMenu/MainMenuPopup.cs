using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class MainMenuPopup : MonoBehaviour
    {
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private RectTransform _content;
        [SerializeField] private CanvasGroup _backCanvasGroup;
        [SerializeField] private CanvasGroup _contentCanvasGroup;
        [SerializeField] private Vector2 _contentClosedAnchoredPosition;
        [SerializeField] private Vector2 _contentOpenedAnchoredPosition;
        [SerializeField] private float _openDuration = 0.4f;
        [SerializeField] private float _closeDuration = 0.4f;

        private void OnEnable()
        {
            foreach (var button in _closeButtons)
                button.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            foreach (var button in _closeButtons)
                button.onClick.RemoveListener(Hide);
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public void ResetState()
        {
            gameObject.SetActive(false);
            _content.anchoredPosition = _contentClosedAnchoredPosition;
            _contentCanvasGroup.interactable = false;
            _backCanvasGroup.interactable = false;
            _backCanvasGroup.alpha = 0;
        }

        public void Show()
        {
            DOTween.Kill(this);
            DOTween.Sequence(this)
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_content.DOAnchorPos(_contentOpenedAnchoredPosition, _openDuration).SetEase(Ease.OutElastic))
                .Join(_backCanvasGroup.DOFade(1, _openDuration).SetEase(Ease.OutCubic))
                .AppendCallback(() =>
                {
                    _contentCanvasGroup.interactable = true;
                    _backCanvasGroup.interactable = true;
                });
        }

        public void Hide()
        {
            DOTween.Kill(this);
            DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    _contentCanvasGroup.interactable = false;
                    _backCanvasGroup.interactable = false;
                })
                .Append(_content.DOAnchorPos(_contentClosedAnchoredPosition, _closeDuration).SetEase(Ease.InElastic))
                .Join(_backCanvasGroup.DOFade(0, _closeDuration).SetEase(Ease.Linear))
                .AppendCallback(() => gameObject.SetActive(false));
        }
    }
}