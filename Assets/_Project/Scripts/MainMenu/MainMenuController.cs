using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Plugins.Audio;
using Project.Core.Audio;
using Project.Core.Misc;
using UnityEngine;
using VContainer.Unity;

namespace Project.MainMenu
{
    [UsedImplicitly]
    public class MainMenuController : IInitializable, IDisposable
    {
        private readonly MainMenuWindow _mainMenuWindow;
        private readonly SceneLoader _sceneLoader;

        private CancellationTokenSource _cts;
        private MainMenuAdditionalWindow _activeAdditionalWindow;
        
        public MainMenuController(MainMenuWindow window, SceneLoader sceneLoader)
        {
            _mainMenuWindow = window;
            _sceneLoader = sceneLoader;
        }
        
        public void Initialize()
        {
            _mainMenuWindow.PlayButton.onClick.AddListener(OnPlayClicked);
            _mainMenuWindow.QuitButton.onClick.AddListener(OnQuitClicked);
            _mainMenuWindow.SettingsButton.onClick.AddListener(OnSettingsClicked);
            _mainMenuWindow.AboutUsButton.onClick.AddListener(OnAboutUsClicked);
            
            _mainMenuWindow.SettingsWindow.ResetWindow();
            _mainMenuWindow.AboutUsWindow.ResetWindow();
            
            MusicController.Set(EMusicTrack.Main, AudioSystem.MainMenu_Music);
            
            _mainMenuWindow.QuitButton.gameObject.SetActive(
                Application.platform != RuntimePlatform.WebGLPlayer);
        }

        public void Dispose()
        {
            _mainMenuWindow.PlayButton.onClick.RemoveListener(OnPlayClicked);
            _mainMenuWindow.QuitButton.onClick.RemoveListener(OnQuitClicked);
            _mainMenuWindow.SettingsButton.onClick.RemoveListener(OnSettingsClicked);
            _mainMenuWindow.AboutUsButton.onClick.RemoveListener(OnAboutUsClicked);
        }

        private void OnQuitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            UnityEngine.Application.Quit();
#endif
        }

        private void OnPlayClicked() => _sceneLoader.Load(2);
        private void OnAboutUsClicked() => ShowAdditionalWindow(_mainMenuWindow.AboutUsWindow);
        private void OnSettingsClicked() => ShowAdditionalWindow(_mainMenuWindow.SettingsWindow);

        private void ShowAdditionalWindow(MainMenuAdditionalWindow window)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            _cts = CancellationTokenSource.CreateLinkedTokenSource(
                _mainMenuWindow.GetCancellationTokenOnDestroy());
            
            UniTask.Void(async cancellationToken =>
            {
                var oldWindow = _activeAdditionalWindow;
                bool isSame = window == oldWindow;
                _activeAdditionalWindow = isSame ? null : window;
                
                cancellationToken.ThrowIfCancellationRequested();
                if (oldWindow)
                    await oldWindow.Hide(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (!isSame)
                    await _activeAdditionalWindow.Show(cancellationToken);
            }, _cts.Token);
        }
    }
}