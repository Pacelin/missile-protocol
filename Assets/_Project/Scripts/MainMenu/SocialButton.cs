using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class SocialButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _link;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);
        private void OnClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            OpenURLInExternalWindow(_link);
#else
            Application.OpenURL(_link);
#endif
        }
        
#if UNITY_WEBGL && !UNITY_EDITOR
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void OpenURLInExternalWindow(string url);
#endif
    }
}