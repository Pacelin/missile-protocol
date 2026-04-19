using Project.Game.Map;
using Project.MainMenu;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Project.Game
{
    public class TargetMark : MonoBehaviour
    {
        [SerializeField] private GameObject _mark;
        [Space]
        [SerializeField] private Rect _rect;
        [SerializeField] private float _nearDistance;
        [SerializeField] private MapLocation _target;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private LocalizedString _kmLocalized;
        [SerializeField] private LocalizedString _milesLocalized;

        public void SetTarget(MapLocation target) => _target = target;
        
        private void Update()
        {
            if (!_target)
                return;
            
            var shipPosition = G.ShipModel.Position;
            var targetPosition = _target.Position;
            var vector = targetPosition - shipPosition;
            if (vector.magnitude < _nearDistance)
            {
                _mark.SetActive(false);
                return;
            }

            _mark.SetActive(true);
            var measure = Measurement.UnitToMeasureDistance(G.ShipModel.LinearVelocity.MaxSpeed,
                vector.magnitude);
            var localized = Measurement.ActiveMeasure == EMeasure.Kilometers ? _kmLocalized : _milesLocalized;
            _text.text = measure.ToString("0.0") + " " + localized.GetLocalizedString();

            var normVector = vector.normalized;
            var showVector = normVector * DistanceFromCenterToBoundary(_rect, normVector);
            transform.position = _rect.center + showVector;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_rect.center, new Vector3(_rect.width, _rect.height, 1));
        }

        private static float DistanceFromCenterToBoundary(Rect rect, Vector2 direction)
        {
            if (direction == Vector2.zero)
                return 0f;

            Vector2 center = rect.center;
            Vector2 halfSize = rect.size * 0.5f;

            float tx = (direction.x != 0) ? (halfSize.x / Mathf.Abs(direction.x)) : float.PositiveInfinity;
            float ty = (direction.y != 0) ? (halfSize.y / Mathf.Abs(direction.y)) : float.PositiveInfinity;

            return Mathf.Min(tx, ty);
        }
    }
}