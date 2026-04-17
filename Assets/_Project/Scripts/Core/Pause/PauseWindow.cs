using Project.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Pause
{
    public class PauseWindow : MonoBehaviour
    {
        public Button ResumeButton => _resumeButton;
        public Button SettingsButton => _settingsButton;
        public Button MainMenuButton => _mainMenuButton;

        public MainMenuAdditionalWindow SettingsWindow => _settingsWindow;
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _mainMenuButton;
        [Space]
        [SerializeField] private MainMenuAdditionalWindow _settingsWindow;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}