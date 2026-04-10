using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Core.Misc
{
    public class SceneTransitionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeIn = 0.5f;
        [SerializeField] private float _fadeOut = 0.4f;

        private void OnDestroy()
        {
            _canvasGroup.DOKill();
        }

        public UniTask FadeIn(CancellationToken cancellationToken)
        {
            return DOTween.Sequence(_canvasGroup)
                .AppendCallback(() => gameObject.SetActive(true))
                .Append(_canvasGroup.DOFade(1, _fadeIn).From(0))
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public UniTask FadeOut(CancellationToken cancellationToken)
        {
            return DOTween.Sequence(_canvasGroup)
                .Append(_canvasGroup.DOFade(0, _fadeOut).From(1))
                .AppendCallback(() => gameObject.SetActive(false))
                .ToUniTask(cancellationToken: cancellationToken);
        }
    }
}