using System;
using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class MachineButton : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler, 
        IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnPress;
        public event Action OnRelease;
        public event Action OnClick;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                    return;
                _interactable = value;

                if (!_interactable && _hover && _down)
                {
                    OnRelease?.Invoke();
                    _down = false;
                    _hover = false;
                }
                
                UpdateButtonState();
            }
        }
        
        [SerializeField] private bool _interactable;
        [Space]
        [SerializeField] private SpriteRenderer _buttonRenderer;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _hoverSprite;
        [SerializeField] private Sprite _downSprite;
        [SerializeField] private Sprite _disabledSprite;
        [Space]
        [SerializeField] private Transform _buttonContent;
        [SerializeField] private Vector3 _defaultContentPosition;
        [SerializeField] private Vector3 _downContentPosition;
        [SerializeField] private float _defaultContentScale = 1f;
        [SerializeField] private float _downContentScale = 0.95f;
        [Space]
        [SerializeField] private SoundEvent _enterSound;
        [SerializeField] private SoundEvent _downSound;
        [SerializeField] private SoundEvent _upSound;
        
        private bool _hover;
        private bool _down;

        private void OnEnable() => UpdateButtonState();

        private void UpdateButtonState()
        {
            var sprite = _defaultSprite;
            var contentPosition = _defaultContentPosition;
            var contentScale = _defaultContentScale;
            
            if (!_interactable)
                sprite = _disabledSprite;
            else if (_down)
            {
                sprite = _downSprite;
                contentPosition = _downContentPosition;
                contentScale = _downContentScale;
            }
            else if (_hover)
                sprite = _hoverSprite;

            _buttonRenderer.sprite = sprite;
            if (_buttonContent)
            {
                _buttonContent.localPosition = contentPosition;
                _buttonContent.localScale = Vector3.one * contentScale;
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            _hover = true;
            if (!_down)
                _enterSound.PlayOneShot();
            UpdateButtonState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            bool isDown = _down;
            _hover = false;
            _down = false;

            if (isDown)
            {
                _upSound.PlayOneShot();
                OnRelease?.Invoke();
            }
            UpdateButtonState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            _down = true;
            _downSound.PlayOneShot();
            
            OnPress?.Invoke();
            
            UpdateButtonState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            bool isDown = _down;
            _down = false;
            
            if (_hover && isDown)
            {
                _upSound.PlayOneShot();
                OnRelease?.Invoke();
                OnClick?.Invoke();
            }
            
            UpdateButtonState();
        }
    }
}