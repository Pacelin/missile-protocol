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
        [Space]
        [SerializeField] private MachineButton _unlockButton;

        private WaveFilter _filter;
        
        public void StartFilter(IFilterHandler handler)
        {
            if (_unlockButton)
                _unlockButton.Interactable = true;

            _filter = WaveFilter.New(
                _amplitudeRange, _frequencyRange,
                _amplitudeSlider.Steps,
                _amplitudeSlider.CurrentStep,
                _frequencySlider.CurrentStep);

            _current.amplitude = _filter.GetAmplitudeSin();
            _current.frequency = _filter.GetFrequencySin();
            _correct.amplitude = _filter.GetAmplitudeCorrectSin();
            _correct.frequency = _filter.GetFrequencyCorrectSin();
            _filter.SetHandler(handler);
            gameObject.SetActive(true);
        }

        public void StopFilter()
        {
            gameObject.SetActive(false);
            _filter = null;
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
        }

        private void OnFrequencyValueChanged()
        {
            _filter.SetAmplitudeStep(_amplitudeSlider.CurrentStep);
            _current.amplitude = _filter.GetAmplitudeSin();
        }

        private void OnAmplitudeValueChanged()
        {
            _filter.SetFrequencyStep(_frequencySlider.CurrentStep);
            _current.frequency = _filter.GetFrequencySin();
        }
    }
}