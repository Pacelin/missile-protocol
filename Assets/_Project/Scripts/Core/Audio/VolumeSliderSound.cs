using Plugins.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Audio
{
    public class VolumeSliderSound : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void OnEnable() => _slider.onValueChanged.AddListener(OnScroll);
        private void OnDisable() => _slider.onValueChanged.RemoveListener(OnScroll);
        private void OnScroll(float arg0) => AudioSystem.UI_Slider.PlayOneShot();
    }
}