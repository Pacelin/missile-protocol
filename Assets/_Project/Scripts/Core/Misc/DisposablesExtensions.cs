using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Project.Core.Misc
{
    public static class DisposablesExtensions
    {
        public static IDisposable SubscribeOnClick(this Button button, UnityAction action)
        {
            button.onClick.AddListener(action);
            return new DisposableAction(() => button.onClick.RemoveListener(action));
        }
    }
}