using UnityEngine;

namespace Project.Game
{
    public class WaveFilter : Filter
    {
        private Vector2 _amplitudeRange;
        private Vector2 _frequencyRange;
        private int _steps;

        private int _correctAmplitude;
        private int _currentAmplitude;
        private int _correctFrequency;
        private int _currentFrequency;
        
        private WaveFilter() { }

        public float GetFrequencySin() =>
            Mathf.Lerp(_frequencyRange.x, _frequencyRange.y, 1f * _steps / _currentFrequency);
        public float GetFrequencyCorrectSin() =>
            Mathf.Lerp(_frequencyRange.x, _frequencyRange.y, 1f * _steps / _correctFrequency);
        public float GetAmplitudeSin() =>
            Mathf.Lerp(_amplitudeRange.x, _amplitudeRange.y, 1f * _steps / _currentAmplitude);
        public float GetAmplitudeCorrectSin() =>
            Mathf.Lerp(_amplitudeRange.x, _amplitudeRange.y, 1f * _steps / _correctAmplitude);

        public void SetAmplitudeStep(int step)
        {
            _currentAmplitude = step;
            CheckCorrect();
        }

        public void SetFrequencyStep(int step)
        {
            _correctFrequency = step;
            CheckCorrect();
        }

        private void CheckCorrect()
        {
            if (_currentAmplitude == _correctAmplitude &&
                _currentFrequency == _correctFrequency)
                SendComplete();
        }
        
        public static WaveFilter New(Vector2 amplitudeRange, Vector2 frequencyRange,
            int steps, int currentAmplitude, int currentFrequency)
        {
            var filter = new WaveFilter();
            filter._amplitudeRange = amplitudeRange;
            filter._frequencyRange = frequencyRange;
            filter._steps = steps;

            filter._correctAmplitude = Random.Range(0, steps + 1);
            filter._correctFrequency = Random.Range(0, steps + 1);
            filter._currentAmplitude = currentAmplitude;
            filter._currentFrequency = currentFrequency;
            
            if (filter._correctFrequency == currentFrequency)
                filter._correctFrequency = Random.Range(0, steps + 1);
            if (filter._correctAmplitude == currentAmplitude)
                filter._correctAmplitude = Random.Range(0, steps + 1);
            if (filter._correctFrequency == currentFrequency)
                filter._correctFrequency = Random.Range(0, steps + 1);
            if (filter._correctAmplitude == currentAmplitude)
                filter._correctAmplitude = Random.Range(0, steps + 1);

            return filter;
        }
    }
}