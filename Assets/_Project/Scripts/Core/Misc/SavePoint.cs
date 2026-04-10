using UnityEngine;

namespace Project.Core.Misc
{
    public class SavePoint<T>
    {
        [System.Serializable]
        private struct ArrayWrapper
        {
            public T Array;
        }
        
        private readonly string _key;
        public SavePoint(string key) => _key = key;

        public bool HasSave() => PlayerPrefs.HasKey(_key);

        public T Load()
        {
            if (typeof(T).IsArray)
                return JsonUtility.FromJson<ArrayWrapper>(PlayerPrefs.GetString(_key)).Array;
            
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(_key));
        } 

        public void Save(T data)
        {
            if (typeof(T).IsArray)
                PlayerPrefs.SetString(_key, JsonUtility.ToJson(new ArrayWrapper() { Array = data }));
            else    
                PlayerPrefs.SetString(_key, JsonUtility.ToJson(data));
            
            PlayerPrefs.Save();
        }
    }
}