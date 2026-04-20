using DG.Tweening;
using UnityEngine;

namespace Project.Game.Utils
{
    public class TutorialOnboardingHighlight : MonoBehaviour
    {
        [SerializeField] private MachineButton _button;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _fadeIn;
        [SerializeField] private float _sustain;
        [SerializeField] private float _fadeOut;
        [SerializeField] private float _delay;
        [Space]
        [SerializeField] private float _zeroAlpha = 0.1f;
        [SerializeField] private float _oneAlpha = 0.3f;

        private void Awake()
        {
            _button.OnClick += OnClick;
            _spriteRenderer.color = new Color(1, 1, 1, _zeroAlpha);
            DOTween.Sequence(this)
                .Append(_spriteRenderer.DOFade(_oneAlpha, _fadeIn).SetEase(Ease.InQuad))
                .AppendInterval(_sustain)
                .Append(_spriteRenderer.DOFade(_zeroAlpha, _fadeOut).SetEase(Ease.OutQuad))
                .AppendInterval(_delay)
                .SetLoops(-1);
        }


        private void OnDestroy()
        {
            _button.OnClick -= OnClick;
            DOTween.Kill(this);
        }
        
        private void OnClick() => Destroy(gameObject);
    }
}