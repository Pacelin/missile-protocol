using UnityEngine;

namespace Project.Core.Misc
{
    public class SaveBool
    {
        public bool Value
        {
            get => _value;
            set
            {
                PlayerPrefs.SetInt(_key, value ? 1 : 0);
                PlayerPrefs.Save();
                _value = value;
            }
        }
        
        private bool _value;
        private readonly string _key;

        public SaveBool(string key, bool defaultValue)
        {
            _key = key;
            var savedValue = PlayerPrefs.GetInt(_key, -1);
            if (savedValue == -1)
                _value = defaultValue;
            else
                _value = PlayerPrefs.GetInt(_key) == 1;
        }
    }
}