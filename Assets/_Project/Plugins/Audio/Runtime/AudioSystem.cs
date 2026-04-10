using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.Audio
{
    public static partial class AudioSystem
    {
        public static class Volumes
        {
            public static float MasterVolume
            {
                get
                {
                    _masterBus.getVolume(out var volume);
                    return volume;
                }
                set
                {
                    _masterBus.setVolume(value);
                    PlayerPrefs.SetFloat("master_volume", value);
                }
            }

            public static float GetVolume(int index)
            {
                _buses[index].getVolume(out var volume);
                return volume;
            }

            public static void SetVolume(int index, float volume)
            {
                _buses[index].setVolume(volume);
                _buses[index].getID(out var id);
                PlayerPrefs.SetFloat("volume_of_" + id, volume);
            }
        }
        
        private static FMOD.Studio.Bus _masterBus;
        private static FMOD.Studio.Bus[] _buses;
        private static IDisposable _focusDisposable;
        private static AudioFocusHandler _focusHandler;
        private static bool _isDisposed;

        public static async UniTask Initialize(CancellationToken cancellationToken)
        {
            _isDisposed = false;

            var focusHandlerGO = new GameObject("[AUDIO_SYSTEM]");
            Object.DontDestroyOnLoad(focusHandlerGO);
            _focusHandler = focusHandlerGO.AddComponent<AudioFocusHandler>();
            
            RuntimeManager.LoadBank("Master.strings", true);
            RuntimeManager.LoadBank("Master", true);

            await UniTask.WaitUntil(() => RuntimeManager.HaveAllBanksLoaded, cancellationToken: cancellationToken)
                .SuppressCancellationThrow();
            if (cancellationToken.IsCancellationRequested) 
                return;
            await UniTask.WaitWhile(RuntimeManager.AnySampleDataLoading, cancellationToken: cancellationToken)
                .SuppressCancellationThrow();
            if (cancellationToken.IsCancellationRequested) 
                return;

            var volumes = Resources.Load<AudioVolumes>("SO_AudioVolumes");
            
            _masterBus = RuntimeManager.GetBus(volumes.MasterBusPath);
            _masterBus.setVolume(PlayerPrefs.GetFloat("master_volume", volumes.DefaultMasterVolume));

            _buses = new FMOD.Studio.Bus[volumes.BusesPaths.Length];
            for (int i = 0; i < _buses.Length; i++)
            {
                _buses[i] = RuntimeManager.GetBus(volumes.BusesPaths[i]);
                _buses[i].getID(out var busId);
                _buses[i].setVolume(PlayerPrefs.GetFloat("volume_of_" + busId, volumes.DefaultVolume));
            }
            
            _focusDisposable = _focusHandler.IsFocused.Skip(1).DistinctUntilChanged()
                .Subscribe(hasFocus =>
                {
                    if (_isDisposed) return;
                    
                    RuntimeManager.PauseAllEvents(!hasFocus);
                    
                    if (hasFocus)
                        RuntimeManager.CoreSystem.mixerResume();
                    else
                        RuntimeManager.CoreSystem.mixerSuspend();
                });

            cancellationToken.Register(Dispose);
        }

        private static void Dispose()
        {
            _isDisposed = true;
            _focusDisposable?.Dispose();
            _focusDisposable = null;
            
            RuntimeManager.PauseAllEvents(false);
            RuntimeManager.CoreSystem.mixerResume();
        }
    }
}