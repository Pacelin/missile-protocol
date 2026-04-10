using Plugins.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Audio
{
    public class AudioSettingsVolumeSlider : MonoBehaviour
    {
        [SerializeField] private int[] _busIndex;
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            var value = AudioSystem.Volumes.GetVolume(_busIndex[0]);
            _slider.SetValueWithoutNotify(value);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        } 

        private void OnSliderValueChanged(float value)
        {   
            foreach (var busIndex in _busIndex)
                AudioSystem.Volumes.SetVolume(busIndex, value);
        }
    }
}