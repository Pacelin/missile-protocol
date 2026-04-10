using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core.Pause
{
    public class PauseWindowLifetimeScope : LifetimeScope
    {
        [SerializeField] private PauseWindow _window;
        [SerializeField] private AcceptPopup _acceptExitPopup;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PauseWindowController>()
                .WithParameter(_window)
                .WithParameter(_acceptExitPopup);
        }
    }
}