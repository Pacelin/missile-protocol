using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Plugins.Audio;
using Project.Core.Misc;
using VContainer.Unity;

namespace Project.Core.Pause
{
    [UsedImplicitly]
    public class PauseWindowController : IInitializable, IDisposable
    {
        private readonly PauseWindow _window;
        private readonly PauseController _pauseController;
        private readonly AcceptPopup _acceptPopup;
        private readonly SceneLoader _sceneLoader;

        private bool _showing;
        
        public PauseWindowController(PauseWindow window,
            PauseController pauseController, 
            AcceptPopup acceptPopup,
            SceneLoader sceneLoader)
        {
            _window = window;
            _pauseController = pauseController;
            _acceptPopup = acceptPopup;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _window.MainMenuButton.onClick.AddListener(QuitToMainMenu);
            _window.ResumeButton.onClick.AddListener(Hide);
        }

        public void Dispose()
        {
            _window.MainMenuButton.onClick.RemoveListener(QuitToMainMenu);
            _window.ResumeButton.onClick.RemoveListener(Hide);

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
            UniTask.Void(async () =>
            {
                if (await _acceptPopup.Show())
                {
                    _pauseController.SetPause(EPauseState.PausedByUser, false);
                    _sceneLoader.Load(1);
                }
            });
        }
    }
}