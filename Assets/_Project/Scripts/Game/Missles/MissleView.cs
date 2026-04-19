using TMPro;
using UnityEngine;

namespace Project.Game.Missles
{
    public class MissleView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lengthText;
        [SerializeField] private TMP_Text _seriesText;
        [SerializeField] private GameObject[] _marks;

        public void Set(MissleConfiguration configuration)
        {
            _lengthText.text = configuration.Length;
            _seriesText.text = configuration.Series;
            for (int i = 0; i < _marks.Length; i++)
                _marks[i].SetActive(configuration.MarkIndex == i);
        }
    }
}