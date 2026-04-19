using UnityEngine;
using UnityEngine.UI;

namespace Project.MainMenu
{
    public class MeasurementSettingsButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _activeWhenSelected;
        [SerializeField] private EMeasure _measure;

        private void OnEnable()
        {
            Measurement.OnMeasurementChanged += OnMeasurementChanged;
            _button.onClick.AddListener(OnClick);
            OnMeasurementChanged();
        }

        private void OnDisable()
        {
            Measurement.OnMeasurementChanged -= OnMeasurementChanged;
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Measurement.ActiveMeasure = _measure;
        }

        private void OnMeasurementChanged()
        {
            _activeWhenSelected.SetActive(Measurement.ActiveMeasure == _measure);
        }
    }
}