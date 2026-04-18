using System;
using UnityEngine;

namespace Project.Game
{
    public class FuelModel
    {
        public event Action<float, float> OnValueChanged;
        public float Value => _value;
        
        private float _value;
        private readonly float _consumption;
        
        public FuelModel(float startFuel, float consumption)
        {
            _value = startFuel;
            _consumption = consumption;
        }

        public void Fill(float amount)
        {
            var oldValue = _value;
            _value = Mathf.Clamp01(_value + amount);
            OnValueChanged?.Invoke(oldValue, _value);
        }
        
        public void Consume(float power, float deltaTime)
        {
            if (_value == 0f)
                return;
            
            var oldValue = _value;
            _value = Mathf.Clamp01(_value - power * _consumption * deltaTime);
            OnValueChanged?.Invoke(oldValue, _value);
        }
    }
}