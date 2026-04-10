using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.MainMenu
{
    public class MainMenuLifetimeScope : LifetimeScope
    {
        [SerializeField] private MainMenuWindow _window;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_window);
            builder.RegisterEntryPoint<MainMenuController>();
        }
    }
}