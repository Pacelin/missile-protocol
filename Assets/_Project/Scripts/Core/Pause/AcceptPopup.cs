using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Pause
{
    public class AcceptPopup : MonoBehaviour
    {
        [SerializeField] private Button _accept;
        [SerializeField] private Button _decline;

        private UniTaskCompletionSource<bool> _completionSource;

        private void OnEnable()
        {
            _accept.onClick.AddListener(OnAccept);
            _decline.onClick.AddListener(OnDecline);
        }

        private void OnDisable()
        {
            _accept.onClick.RemoveListener(OnAccept);
            _decline.onClick.RemoveListener(OnDecline);
        }

        public UniTask<bool> Show()
        {
            _completionSource = new UniTaskCompletionSource<bool>();
            gameObject.SetActive(true);
            return _completionSource.Task;
        }
        
        private void OnAccept()
        {
            _completionSource.TrySetResult(true);
            _completionSource = null;
            gameObject.SetActive(false);
        }

        private void OnDecline()
        {
            _completionSource.TrySetResult(false);
            _completionSource = null;
            gameObject.SetActive(false);
        }
    }
}