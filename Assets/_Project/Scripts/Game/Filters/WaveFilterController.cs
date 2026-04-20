using Plugins.Audio;
using UnityEngine;

namespace Project.Game
{
    public class WaveFilterController : MonoBehaviour, IFilterController
    {
        [SerializeField] private Vector2 _amplitudeRange;
        [SerializeField] private Vector2 _frequencyRange;
        [SerializeField] private MachineSliderHandle _amplitudeSlider;
        [SerializeField] private MachineSliderHandle _frequencySlider;
        [SerializeField] private SineWaveGraph _correct;
        [SerializeField] private SineWaveGraph _current;

        private WaveFilter _filter;
        private SoundEventInstance _soundEvent;
        
        public void StartFilter(IFilterHandler handler)
        {
            _filter = WaveFilter.New(
                _amplitudeRange, _frequencyRange,
                _amplitudeSlider.Steps, _frequencySlider.Steps,
                _amplitudeSlider.CurrentStep,
                _frequencySlider.CurrentStep);

            _current.amplitude = _filter.GetAmplitudeSin();
            _current.frequency = _filter.GetFrequencySin();
            _correct.amplitude = _filter.GetAmplitudeCorrectSin();
            _correct.frequency = _filter.GetFrequencyCorrectSin();
            _filter.SetHandler(handler);
            gameObject.SetActive(true);

            _soundEvent = AudioSystem.Game_Machines_Sine.CreateInstance();
            SetAudioParameters();
            _soundEvent.Start();
        }

        public void StopFilter()
        {
            gameObject.SetActive(false);
            if (_soundEvent != null)
            {
                _soundEvent.Stop(false);
                _soundEvent.Release();
                _soundEvent = null;
            }
        }
        
        private void OnEnable()
        {
            _amplitudeSlider.OnValueChanged += OnAmplitudeValueChanged;
            _frequencySlider.OnValueChanged += OnFrequencyValueChanged;
        }

        private void OnDisable()
        {
            _amplitudeSlider.OnValueChanged -= OnAmplitudeValueChanged;
            _frequencySlider.OnValueChanged -= OnFrequencyValueChanged;
            
            if (_soundEvent != null)
            {
                _soundEvent.Stop(false);
                _soundEvent.Release();
                _soundEvent = null;
            }
        }

        private void OnAmplitudeValueChanged()
        {
            _filter.SetAmplitudeStep(_amplitudeSlider.CurrentStep);
            _current.amplitude = _filter.GetAmplitudeSin();
            SetAudioParameters();
        }

        private void OnFrequencyValueChanged()
        {
            _filter.SetFrequencyStep(_frequencySlider.CurrentStep);
            _current.frequency = _filter.GetFrequencySin();
            SetAudioParameters();
        }

        private void SetAudioParameters()
        {
            if (_soundEvent == null)
                return;

            AudioSystem.Global.SetAmplitude(_filter.GetAmplitudeNormalized());
            AudioSystem.Global.SetFrequency(_filter.GetFrequencyNormalized());
        }
    }
}