using UnityEngine;

namespace Project.Game
{
    public class RadarLine : MonoBehaviour
    {
        [SerializeField] private Transform _line;
        
        
    }
    
    public class MechanismChangeSpriteByPressButton : MonoBehaviour
    {
        [SerializeField] private MachineButton _button;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _holdSprite;
        
        private void OnEnable()
        {
            _button.OnPress += OnPress;
            _button.OnRelease += OnRelease;
            _spriteRenderer.sprite = _button.Pressed ? _holdSprite : _defaultSprite;
        }

        private void OnDisable()
        {
            _button.OnPress -= OnPress;
            _button.OnRelease -= OnRelease;
        }

        private void OnPress() => _spriteRenderer.sprite = _holdSprite;
        private void OnRelease() => _spriteRenderer.sprite = _defaultSprite;
    }
}