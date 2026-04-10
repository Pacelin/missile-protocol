using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class MainMenuWindow : MonoBehaviour
    {
        public Button PlayButton => _playButton;
        public Button QuitButton => _quitButton;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
    }
}
