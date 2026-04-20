using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public class RandomTextEveryEnable : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private LocalizedString[] _localizedStrings;

        private LocalizedString _activeLocalize;
        
        private void OnEnable()
        {
            _activeLocalize = _localizedStrings[Random.Range(0, _localizedStrings.Length)];
            _text.text = _activeLocalize.GetLocalizedString();
            
            _activeLocalize.StringChanged += OnStringChanged;
        }

        private void OnDisable()
        {
            if (_activeLocalize != null)
                _activeLocalize.StringChanged -= OnStringChanged;
        }

        private void OnStringChanged(string value)
        {
            _text.text = _activeLocalize.GetLocalizedString();
        }
    }
}