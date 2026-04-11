using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class MainMenuWindow : MonoBehaviour
    {
        public Button PlayButton => _playButton;
        public Button SettingsButton => _settingsButton;
        public Button AboutUsButton => _aboutUsButton;
        public Button QuitButton => _quitButton;

        public MainMenuAdditionalWindow SettingsWindow => _settingsWindow;
        public MainMenuAdditionalWindow AboutUsWindow => _aboutUsWindow;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _aboutUsButton;
        [SerializeField] private Button _quitButton;
        [Space]
        [SerializeField] private MainMenuAdditionalWindow _settingsWindow;
        [SerializeField] private MainMenuAdditionalWindow _aboutUsWindow;
    }
}
