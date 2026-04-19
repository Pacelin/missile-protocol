using UnityEngine;

namespace Project.Game
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private TargetMark[] _datas;
        [SerializeField] private TargetMark _destination;
        
        private void Awake()
        {
            foreach (var data in _datas)
                data.gameObject.SetActive(true);
            _destination.gameObject.SetActive(false);
        }
    }
}