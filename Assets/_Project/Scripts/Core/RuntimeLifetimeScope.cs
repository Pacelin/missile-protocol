using Project.Core.Misc;
using Project.Core.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Core
{
    public class RuntimeLifetimeScope : LifetimeScope
    {
        [Header("Scene Loading")] 
        [SerializeField] private SceneTransitionView _transition;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PauseController>().AsSelf();
            builder.RegisterComponent(_transition);
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }
    }
}