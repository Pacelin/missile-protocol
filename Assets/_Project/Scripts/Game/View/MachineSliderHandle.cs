using System;
using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class MachineSliderHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action OnValueChanged;
        
        public int Steps => _steps;
        public int CurrentStep => _currentStep;
        public float NormalizedValue => 1f * _currentStep / _steps;

        [SerializeField] private SpriteRenderer _handleSpriteRenderer;
        [SerializeField] private Sprite _defaultHandleSprite;
        [SerializeField] private Sprite _hoverHandleSprite;
        [SerializeField] private Sprite _downHandleSprite;
        [Space]
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _minimumLocalPosition;
        [SerializeField] private Vector3 _maximumLocalPosition;
        [SerializeField] private int _steps;
        [Space]
        [SerializeField] private SoundEvent _enterSound;
        [SerializeField] private SoundEvent _downSound;
        [SerializeField] private SoundEvent _upSound;
        [SerializeField] private SoundEvent _positionChangedSound;
        
        private int _currentStep;
        private bool _down;
        private bool _hover;
        private bool _isDragging;

        private void OnValidate()
        {
            if (!_camera)
                _camera = FindFirstObjectByType<Camera>();
        }

        private void OnEnable()
        {
            UpdateSpriteState();
            UpdateHandlePosition();
        }

        public void SetHandlePosition(int step)
        {
            _currentStep = Math.Clamp(step, 0, _steps);
            OnValueChanged?.Invoke();
            UpdateHandlePosition();
        }
        
        public void SetHandlePosition(float normalized)
        {
            var step = Mathf.RoundToInt(Mathf.Lerp(0, _steps, normalized));
            SetHandlePosition(step);
        }
        
        private void UpdateHandlePosition()
        {
            float newT = (float)_currentStep / _steps;
            Vector3 newLocalPos = Vector3.Lerp(_minimumLocalPosition, _maximumLocalPosition, newT);
            _transform.localPosition = newLocalPos;
        }
        
        private void UpdateSpriteState()
        {
            var sprite = _defaultHandleSprite;
            if (_down)
                sprite = _downHandleSprite;
            else if (_hover)
                sprite = _hoverHandleSprite;

            _handleSpriteRenderer.sprite = sprite;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            if (!_down)
                _enterSound.PlayOneShot();
            UpdateSpriteState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            UpdateSpriteState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            _downSound.PlayOneShot();
            UpdateSpriteState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            _upSound.PlayOneShot();
            UpdateSpriteState();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
                return;

            Vector3 mousePos = new Vector3(
                eventData.position.x, eventData.position.y,
                Mathf.Abs(_camera.transform.position.z - transform.position.z));
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(mousePos);
            Vector3 localMousePos = _transform.parent.InverseTransformPoint(mouseWorldPos);
    
            Vector3 direction = _maximumLocalPosition - _minimumLocalPosition;
            float length = direction.magnitude;
            if (length < 0.001f) return;
            Vector3 dirNormalized = direction / length;
    
            Vector3 toPoint = localMousePos - _minimumLocalPosition;
            float t = Vector3.Dot(toPoint, dirNormalized) / length;
            t = Mathf.Clamp01(t);
    
            int newStep = Mathf.RoundToInt(t * _steps);
            newStep = Mathf.Clamp(newStep, 0, _steps);
    
            if (newStep != _currentStep)
            {
                _currentStep = newStep;
                OnValueChanged?.Invoke();
                _positionChangedSound.PlayOneShot();
                UpdateHandlePosition();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDragging)
                return;
            
            _isDragging = false;
        }
    }
}