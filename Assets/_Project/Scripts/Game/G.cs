using System;
using Plugins.Audio;
using Project.Core.Audio;
using Project.Core.Misc;
using Project.Core.Pause;
using TMPro;
using UnityEngine;
using VContainer;

namespace Project.Game
{
    public class G : MonoBehaviour
    {
        public static PauseController PauseController => _instance._pauseController;
        public static RadarModel RadarModel => _instance._radarModel;
        public static ShipModel ShipModel => _instance._shipModel;
        public static SceneLoader Loader => _instance._loader;
        public static ResultCanvas ResultCanvas => _instance._resultCanvas;

        [Inject] private PauseController _pauseController;
        [Inject] private RadarModel _radarModel;
        [Inject] private ShipModel _shipModel;
        [Inject] private SceneLoader _loader;

        private static G _instance;

        [SerializeField] private ResultCanvas _resultCanvas;

        [Inject]
        private void Construct() => _instance = this;

        private void Awake()
        {
            MusicController.Set(EMusicTrack.Main, AudioSystem.Game_Music);
        }
    }
}