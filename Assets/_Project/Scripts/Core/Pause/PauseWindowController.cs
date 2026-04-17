using System;
using JetBrains.Annotations;
using Plugins.Audio;
using Project.Core.Misc;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Project.Core.Pause
{
    [UsedImplicitly]
    public class PauseWindowController : IInitializable, IDisposable
    {
        private readonly PauseWindow _window;
        private readonly Button _pauseButton;
        private readonly PauseController _pauseController;
        private readonly SceneLoader _sceneLoader;

        private bool _showing;
        private bool _settingsActive;
        
        [Inject]
        public PauseWindowController(PauseWindow window, Button pauseButton,
            PauseController pauseController,
            SceneLoader sceneLoader)
        {
            _window = window;
            _pauseButton = pauseButton;
            _pauseController = pauseController;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _window.MainMenuButton.onClick.AddListener(QuitToMainMenu);
            _window.ResumeButton.onClick.AddListener(Hide);
            _pauseButton.onClick.AddListener(Show);
            _window.ResetWindow();
        }

        public void Dispose()
        {
            _window.MainMenuButton.onClick.RemoveListener(QuitToMainMenu);
            _window.ResumeButton.onClick.RemoveListener(Hide);
            _pauseButton.onClick.RemoveListener(Show);
            
            if (_showing)
            {
                _pauseController.SetPause(EPauseState.PausedByUser, false);
                AudioSystem.Global.SetPauseState(AudioSystem.Global.ELabel_PauseState.NotOnPause);
            }
        }

        public void Show()
        {
            _pauseController.SetPause(EPauseState.PausedByUser, true);
            AudioSystem.Global.SetPauseState(AudioSystem.Global.ELabel_PauseState.OnPause);
            _window.Show();
            _showing = true;
        }

        public void Hide()
        {
            _pauseController.SetPause(EPauseState.PausedByUser, false);
            AudioSystem.Global.SetPauseState(AudioSystem.Global.ELabel_PauseState.NotOnPause);
            _window.Hide();
            _showing = false;
        }

        private void QuitToMainMenu()
        {
            _pauseController.SetPause(EPauseState.PausedByUser, false);
            _sceneLoader.Load(1);
        }
    }
}
