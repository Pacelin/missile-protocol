using UnityEngine;

namespace Project.Game.Map
{
    public class MapZone : MonoBehaviour
    {
        public float Radius => _radius;

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _radius;
        [SerializeField] private int _segments;

        private void OnValidate()
        {
            UpdateRange();
        }

        private void OnDrawGizmosSelected()
        {
            if (_lineRenderer)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void UpdateRange()
        {
            if (!_lineRenderer) return;
            
            _lineRenderer.positionCount = _segments;
            _lineRenderer.loop = true;

            float angleStep = 2f * Mathf.PI / _segments;

            for (int i = 0; i < _segments; i++)
            {
                float angle = i * angleStep;
                float x = _radius * Mathf.Cos(angle);
                float y = _radius * Mathf.Sin(angle);
                Vector3 point = new Vector3(x, y, 0);
                _lineRenderer.SetPosition(i, point);
            }
        }
    }
}