using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Plugins.Audio
{
    internal class AudioFocusHandler : MonoBehaviour
    {
        public IReadOnlyAsyncReactiveProperty<bool> IsFocused => _isFocused;
        
        private readonly AsyncReactiveProperty<bool> _isFocused = new(true);

        private void Awake() => _isFocused.Value = Application.isFocused;
        private void OnApplicationFocus(bool hasFocus) => _isFocused.Value = hasFocus;
    }
}