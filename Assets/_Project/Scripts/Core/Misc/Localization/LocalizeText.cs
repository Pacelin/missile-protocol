using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Project.Core.Misc.Localization
{
    
    public class LocalizeText : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private TMP_Text _text;
        [SerializeField] private LocalizedString _localizedString;

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (!_text)
                _text = GetComponent<TMP_Text>();
            
            if (_text && !_localizedString.IsEmpty)
                _text.text = LocalizationPreviewEditorWindow.GetLocalizedString(_localizedString);
        }
#endif

        private void OnEnable() => _localizedString.StringChanged += OnStringChanged;
        private void OnDisable() => _localizedString.StringChanged -= OnStringChanged;
        private void OnStringChanged(string text) => _text.text = text;
    }
}