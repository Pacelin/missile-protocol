using UnityEngine;

namespace Project.Game
{
    public class MechanismRotateByRadar : MonoBehaviour
    {
        [SerializeField] private Transform _rotateTransform;
        
        private void OnEnable()
        {
            G.RadarModel.OnAngleChanged += OnRadarAngleChanged;
            _rotateTransform.rotation = Quaternion.Euler(0, 0, G.RadarModel.Angle);
        }

        private void OnDisable()
        {
            G.RadarModel.OnAngleChanged -= OnRadarAngleChanged;
        }

        private void OnRadarAngleChanged(float oldAngle, float newAngle)
        {
            _rotateTransform.rotation = Quaternion.Euler(0, 0, newAngle);
        }
    }
}