using DG.Tweening;
using UnityEngine;

namespace Project.Game
{
    public class DurabilityProgressBarItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private SpriteRenderer _additiveRenderer;
        [SerializeField] private float _dissolveDelay;
        [SerializeField] private float _dissolveFadeInDuration;
        [SerializeField] private float _dissolveFadeOutDuration;
        [SerializeField] private float _blinkDelay;
        [SerializeField] private float _blinkFadeInDuration;
        [SerializeField] private float _blinkSustain;
        [SerializeField] private float _blinkFadeOutDuration;

        private Material _material;
        private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

        public void Setup(bool isActive)
        {
            if (!_material)
            {
                _material = new Material(_renderer.sharedMaterial);
                _renderer.sharedMaterial = _material;
            }
            _material.SetFloat(DissolveAmount, isActive ? 0 : 1);
            _additiveRenderer.color = new Color(1, 1, 1, 0);
        }
        
        private void OnDestroy()
        {
            DOTween.Kill(this);
            if (_material)
                Destroy(_material);
        }
        
        public void Appear(float delay)
        {
            DOTween.Kill(this);

            DOVirtual.DelayedCall(delay, () =>
            {
                _material.DOFloat(0, DissolveAmount, _dissolveFadeOutDuration)
                    .SetEase(Ease.Linear)
                    .SetTarget(this);

                DOTween.Sequence(this)
                    .AppendInterval(_blinkDelay)
                    .Append(_additiveRenderer.DOFade(1, _blinkFadeInDuration).SetEase(Ease.InQuad))
                    .AppendInterval(_blinkSustain)
                    .Append(_additiveRenderer.DOFade(0, _blinkFadeOutDuration).SetEase(Ease.OutQuad));
            }).SetTarget(this);
        }

        public void Disappear(float delay)
        {
            DOTween.Kill(this);
            
            DOVirtual.DelayedCall(delay, () =>
            {
                DOTween.Sequence(this)
                    .Append(_additiveRenderer.DOFade(1, _blinkFadeInDuration).SetEase(Ease.InQuad))
                    .AppendInterval(_blinkSustain)
                    .Append(_additiveRenderer.DOFade(0, _blinkFadeOutDuration).SetEase(Ease.OutQuad));
                
                _material.DOFloat(1, DissolveAmount, _dissolveFadeInDuration)
                    .SetEase(Ease.Linear)
                    .SetDelay(_dissolveDelay)
                    .SetTarget(this);
            }).SetTarget(this);
        }
    }
}