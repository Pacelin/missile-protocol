using Project.MainMenu;
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
        [SerializeField] private MainMenuPopup _mainMenuPopup;

        public void ResetWindow() => _mainMenuPopup.ResetState();
        public void Show() => _mainMenuPopup.Show();
        public void Hide() => _mainMenuPopup.Hide();
    }
}