using Plugins.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Project.Core.Misc.Localization
{
    public class LocalizeButton : MonoBehaviour, 
        IPointerEnterHandler, IPointerExitHandler, 
        IPointerDownHandler, IPointerUpHandler,
        IPointerClickHandler
    {
        [SerializeField] private LocaleIdentifier _locale;
        [SerializeField] private GameObject _hoverState;
        [SerializeField] private GameObject _downState;
        [SerializeField] private GameObject _selectedState;

        private bool _hover;
        private bool _down;
        private bool _selected;

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            OnSelectedLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        } 

        private void OnSelectedLocaleChanged(Locale locale)
        {
            _selected = locale.Identifier == _locale;
            UpdateState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hover = true;
            UpdateState();

            if (_selected)
                return;
            AudioSystem.UI_Hover.PlayOneShot();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hover = false;
            UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
            UpdateState();

            if (_selected)
                return;
            
            AudioSystem.UI_Down.PlayOneShot();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selected)
                return;
            
            AudioSystem.UI_Select.PlayOneShot();
            LocalizationSettings.SelectedLocale = Locale.CreateLocale(_locale);
        }

        private void UpdateState()
        {
            _selectedState.SetActive(_selected);
            _downState.SetActive(!_selected && _down);
            _hoverState.SetActive(!_selected && !_down && _hover);
        }
    }
}