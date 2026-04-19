using UnityEngine;

namespace Project.Game
{
    public class MachineSwitchButtonGroup : MonoBehaviour
    {
        public MachineSwitchButton ActiveButton
        {
            get
            {
                if (_activeButton && _activeButton.IsOn)
                    return _activeButton;
                return null;
            }
        }
        
        [SerializeField] private MachineSwitchButton[] _buttons;

        private MachineSwitchButton _activeButton;
        
        private void OnEnable()
        {
            foreach (var button in _buttons)
            {
                button.OnValueChanged += OnValueChanged;
                if (button.IsOn)
                    _activeButton = button;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
                button.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(MachineSwitchButton btn)
        {
            if (btn == _activeButton)
                return;
            
            if (btn.IsOn)
            {
                if (_activeButton)
                    _activeButton.SetOn(false);
                _activeButton = btn;
            }
        }
    }
}