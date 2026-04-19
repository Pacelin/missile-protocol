using Project.Game.Map;
using UnityEngine;

namespace Project.Game
{
    public class RadarPingView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _sizeMultiplier;
        [SerializeField] private Vector2 _distanceRange;
        
        private void OnEnable()
        {
            G.RadarModel.OnPingPosition += OnRadarPingPosition;
        }

        private void OnDisable()
        {
            G.RadarModel.OnPingPosition -= OnRadarPingPosition;
        }

        private void OnRadarPingPosition(MapLocation zone, Vector2 vector)
        {
            var t = Mathf.InverseLerp(0, G.RadarModel.ScanDistance, vector.magnitude);
            var spawnDistance = Mathf.Lerp(_distanceRange.x, _distanceRange.y, t);
            _particleSystem.Emit(new ParticleSystem.EmitParams()
            {
                position = vector.normalized * spawnDistance,
                startColor = zone.RadarColor,
                startSize = zone.RadarSize * _sizeMultiplier
            }, 1);
        }
    }
}