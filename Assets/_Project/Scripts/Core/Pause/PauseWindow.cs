using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Pause
{
    public class PauseWindow : MonoBehaviour
    {
        public Button ResumeButton => _resumeButton;
        public Button MainMenuButton => _mainMenuButton;
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}