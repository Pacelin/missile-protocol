using System;
using UnityEngine;

namespace Project.Game
{
    public class MechanismRotateBySlider : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _minRotation;
        [SerializeField] private float _maxRotation;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private MachineSliderHandle _sliderHandle;

        private float _currentRotation;
        private float _targetRotation;

        private void OnEnable()
        {
            _sliderHandle.OnValueChanged += OnSliderValueChanged;
            OnSliderValueChanged();
            
            _currentRotation = _targetRotation;
            _transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
        }

        private void OnDisable()
        {
            _sliderHandle.OnValueChanged -= OnSliderValueChanged;
        }

        private void Update()
        {
            if (Math.Abs(_currentRotation - _targetRotation) < 0.001f)
                return;
            
            _currentRotation = Mathf.MoveTowardsAngle(_currentRotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
        }

        private void OnSliderValueChanged()
        {
            var normalizedValue = _sliderHandle.NormalizedValue;
            _targetRotation = Mathf.LerpAngle(_minRotation, _maxRotation, normalizedValue);
        }
    }
}