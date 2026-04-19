using System;
using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class MachineSwitchButton : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler, 
        IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler
    {
        public bool IsOn => _on;
        public event Action<MachineSwitchButton> OnValueChanged;
        
        [SerializeField] private SpriteRenderer _buttonRenderer;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _hoverSprite;
        [SerializeField] private Sprite _downSprite;
        [SerializeField] private Sprite _defaultOnSprite;
        [SerializeField] private Sprite _hoverOnSprite;
        [SerializeField] private Sprite _downOnSprite;
        [Space]
        [SerializeField] private SoundEvent _enterSound;
        [SerializeField] private SoundEvent _downSound;
        [SerializeField] private SoundEvent _upSound;
        [SerializeField] private SoundEvent _onSound;
        [SerializeField] private SoundEvent _offSound;
        [SerializeField] private bool _on;
        
        private bool _hover;
        private bool _down;

        private void OnEnable() => UpdateButtonState();

        public void SetOn(bool isOn)
        {
            _on = isOn;
            UpdateButtonState();
            OnValueChanged?.Invoke(this);
        }
        
        private void UpdateButtonState()
        {
            Sprite sprite;
            
            if (_on)
            { 
                sprite = _defaultOnSprite;
                if (_down)
                    sprite = _downOnSprite;
                else if (_hover)
                    sprite = _hoverOnSprite;
            }
            else
            {
                sprite = _defaultSprite;
                if (_down)
                    sprite = _downSprite;
                else if (_hover)
                    sprite = _hoverSprite;
            }

            _buttonRenderer.sprite = sprite;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            if (!_down)
                _enterSound.PlayOneShot();
            UpdateButtonState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            UpdateButtonState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            _downSound.PlayOneShot();
            UpdateButtonState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            _upSound.PlayOneShot();
            UpdateButtonState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _on = !_on;
            if (_on)
                _onSound.PlayOneShot();
            else
                _offSound.PlayOneShot();
            UpdateButtonState();
            OnValueChanged?.Invoke(this);
        }
    }
}