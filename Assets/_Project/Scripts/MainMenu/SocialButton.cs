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
            Application.OpenURL(_link);
        }
    }
}