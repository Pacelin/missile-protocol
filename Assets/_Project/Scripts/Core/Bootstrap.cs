using Cysharp.Threading.Tasks;
using Plugins.Audio;
using Project.Core.Misc;
using UnityEngine;
using UnityEngine.Localization.Settings;
using VContainer;

namespace Project.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject] private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            UniTask.Void(async cancellationToken =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                await AudioSystem.Initialize(cancellationToken);
                
                cancellationToken.ThrowIfCancellationRequested();
                if (!LocalizationSettings.InitializationOperation.IsValid())
                {
                    await UniTask.WaitUntil(() => 
                            LocalizationSettings.InitializationOperation.IsValid(),
                        cancellationToken: cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                if (!LocalizationSettings.InitializationOperation.IsDone)
                {
                    await UniTask.WaitUntil(() =>
                            LocalizationSettings.InitializationOperation.IsDone,
                        cancellationToken: cancellationToken);
                }
                
                cancellationToken.ThrowIfCancellationRequested();
                _sceneLoader.Load(1);
            }, this.GetCancellationTokenOnDestroy());
        }
    }
}