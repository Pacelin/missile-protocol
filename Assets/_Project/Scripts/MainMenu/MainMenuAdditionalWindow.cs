using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.MainMenu
{
    public class MainMenuAdditionalWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _mask;

        protected virtual void OnDestroy() => DOTween.Kill(this);

        public void ResetWindow()
        {
            _mask.anchorMax = new Vector2(0, 1);
        }
        
        public UniTask Show(CancellationToken cancellationToken)
        {
            DOTween.Kill(this);
            return DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    gameObject.SetActive(true);
                })
                .Append(_canvasGroup.DOFade(1, 0.2f))
                .Join(_mask.DOAnchorMax(new Vector2(1, 1), 0.3f))
                .AppendCallback(() =>
                {
                    _canvasGroup.interactable = true;
                })
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public UniTask Hide(CancellationToken cancellationToken)
        {
            DOTween.Kill(this);
            return DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    _canvasGroup.interactable = false;
                })
                .Append(_canvasGroup.DOFade(0, 0.2f))
                .Join(_mask.DOAnchorMax(new Vector2(0, 1), 0.3f))
                .AppendCallback(() =>
                {
                    gameObject.SetActive(false);
                })
                .ToUniTask(cancellationToken: cancellationToken);
        }
    }
}