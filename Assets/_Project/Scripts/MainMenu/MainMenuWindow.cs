using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class MainMenuWindow : MonoBehaviour
    {
        public Button PlayButton => _playButton;
        public Button SettingsButton => _settingsButton;
        public Button QuitButton => _quitButton;

        public MainMenuPopup SettingsPopup => _settingsPopup;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [Space]
        [SerializeField] private MainMenuPopup _settingsPopup;
    }
}
