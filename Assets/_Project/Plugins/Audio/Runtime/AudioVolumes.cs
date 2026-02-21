using Plugins.UnityEditorHelpers;
using UnityEngine;

namespace Plugins.Audio
{
    [CreateResourceAsset("SO_AudioVolumes")]
    public class AudioVolumes : ScriptableObject
    {
        public string MasterBusPath => _masterBusPath;
        public string[] BusesPaths => _busesPaths;
        
        public float DefaultMasterVolume => _defaultMasterVolume;
        public float DefaultVolume => _defaultVolume;
        
        [SerializeField] private string _masterBusPath = "bus:/";
        [SerializeField] private string[] _busesPaths = { "bus:/Music", "bus:/Sounds" };
        [Range(0, 1)]
        [SerializeField] private float _defaultMasterVolume = 0.8f;
        [Range(0, 1)]
        [SerializeField] private float _defaultVolume = 0.8f;
    }
}