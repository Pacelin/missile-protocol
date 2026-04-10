using Plugins.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Audio
{
    public class AudioSettingsMasterVolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            var value = AudioSystem.Volumes.MasterVolume;        
            _slider.SetValueWithoutNotify(value);
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        } 

        private void OnSliderValueChanged(float value)
        {
            AudioSystem.Volumes.MasterVolume = value;
        }
    }
}