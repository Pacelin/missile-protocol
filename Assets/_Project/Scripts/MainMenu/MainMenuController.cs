using System;
using JetBrains.Annotations;
using Plugins.Audio;
using Project.Core.Audio;
using Project.Core.Misc;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.MainMenu
{
    [UsedImplicitly]
    public class MainMenuController : IInitializable, IDisposable
    {
        private readonly MainMenuWindow _mainMenuWindow;
        private readonly SceneLoader _sceneLoader;

        private SoundEventInstance _music;
        
        [Inject]
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
            
            _mainMenuWindow.SettingsPopup.ResetState();
            
            _mainMenuWindow.QuitButton.gameObject.SetActive(
                Application.platform != RuntimePlatform.WebGLPlayer);
        
            MusicController.Set(EMusicTrack.Main, AudioSystem.MainMenu_Music);
        }

        public void Dispose()
        {
            _mainMenuWindow.PlayButton.onClick.RemoveListener(OnPlayClicked);
            _mainMenuWindow.QuitButton.onClick.RemoveListener(OnQuitClicked);
            _mainMenuWindow.SettingsButton.onClick.RemoveListener(OnSettingsClicked);
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
        private void OnSettingsClicked() => _mainMenuWindow.SettingsPopup.Show();
    }
}