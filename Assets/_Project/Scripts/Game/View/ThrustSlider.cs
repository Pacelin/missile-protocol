using UnityEngine;

namespace Project.Game
{
    public class ThrustSlider : MonoBehaviour
    {
        [SerializeField] private MachineSliderHandle _sliderHandle;

        private void OnEnable()
        {
            _sliderHandle.SetHandlePosition(G.ShipModel.EnginePower);
            _sliderHandle.OnValueChanged += OnSliderValueChanged;
        }

        private void OnDisable()
        {
            _sliderHandle.OnValueChanged -= OnSliderValueChanged;
        }

        private void OnSliderValueChanged()
        {
            G.ShipModel.SetEnginePower(_sliderHandle.NormalizedValue);
        }
    }
}