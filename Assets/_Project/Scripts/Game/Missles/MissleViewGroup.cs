using UnityEngine;

namespace Project.Game.Missles
{
    public class MissleViewGroup : MonoBehaviour
    {
        [SerializeField] private MissleView[] _missles;

        public void Setup(MissleConfiguration missleConfiguration)
        {
            for (int i = 0; i < _missles.Length; i++)
                _missles[i].gameObject.SetActive(false);
            
            var missle = _missles[missleConfiguration.MissleIndex];
            missle.Set(missleConfiguration);
            missle.gameObject.SetActive(true);
        }
    }
}