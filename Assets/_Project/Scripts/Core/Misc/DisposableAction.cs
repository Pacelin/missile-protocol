using System;

namespace Project.Core.Misc
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _disposeAction;
        
        public DisposableAction(Action disposeAction) => _disposeAction = disposeAction;
        public void Dispose() => _disposeAction.Invoke();
    }
}