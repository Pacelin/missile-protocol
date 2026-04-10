using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Project.Core.Misc
{
    public class SceneLoader
    {
        private readonly SceneTransitionView _transitionView;
        
        public SceneLoader(SceneTransitionView transitionView)
        {
            _transitionView = transitionView;
        }

        public void Load(int buildIndex, Action<IContainerBuilder> extra, float delay = 0f)
        {
            UniTask.Void(async cancellationToken =>
            {
                using (LifetimeScope.Enqueue(extra))
                {
                    await _transitionView.FadeIn(cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();

                    await SceneManager.LoadSceneAsync(buildIndex);
                    cancellationToken.ThrowIfCancellationRequested();

                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();

                    await _transitionView.FadeOut(cancellationToken);
                }
            }, Application.exitCancellationToken);
        }

        public void Load(int buildIndex, float delay = 0f)
        {
            UniTask.Void(async cancellationToken =>
            {
                await _transitionView.FadeIn(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                await SceneManager.LoadSceneAsync(buildIndex);
                cancellationToken.ThrowIfCancellationRequested();

                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                
                await _transitionView.FadeOut(cancellationToken);
            }, Application.exitCancellationToken);
        }
    }
}