using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Project.Core.Pause
{
    public class PauseWindowLifetimeScope : LifetimeScope
    {
        [SerializeField] private PauseWindow _window;
        [SerializeField] private Button _pauseButton;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PauseWindowController>()
                .WithParameter(_window)
                .WithParameter(_pauseButton);
        }
    }
}