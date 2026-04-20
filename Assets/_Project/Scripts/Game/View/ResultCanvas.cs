using Plugins.Audio;
using Project.Core.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class ResultCanvas : MonoBehaviour
    {
        [SerializeField] private Button[] _mainMenuButtons;
        [SerializeField] private Button[] _restartButtons;
        [SerializeField] private GameObject _win;
        [SerializeField] private GameObject _lost;
        
        private void OnEnable()
        {
            foreach (var mainMenuButton in _mainMenuButtons)
                mainMenuButton.onClick.AddListener(GoMainMenu);
            foreach (var restartButton in _restartButtons)
                restartButton.onClick.AddListener(Restart);
            G.PauseController.SetPause(EPauseState.PausedByEnding, true);
        }

        private void OnDisable()
        {
            foreach (var mainMenuButton in _mainMenuButtons)
                mainMenuButton.onClick.RemoveListener(GoMainMenu);
            foreach (var restartButton in _restartButtons)
                restartButton.onClick.RemoveListener(Restart);
            G.PauseController.SetPause(EPauseState.PausedByEnding, false);
        }

        public void Show(bool win)
        {
            _win.SetActive(win);
            _lost.SetActive(!win);
            gameObject.SetActive(true);
            if (win)
                AudioSystem.Game_Win.PlayOneShot();
            else
                AudioSystem.Game_Lose.PlayOneShot();
        }

        private void Restart() => G.Loader.Load(2);
        private void GoMainMenu() => G.Loader.Load(1);
    }
}