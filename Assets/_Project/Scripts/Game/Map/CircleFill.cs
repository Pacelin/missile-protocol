using UnityEngine;

namespace Project.Game.Map
{
    public class CircleFill : MonoBehaviour
    {
        public float Radius => _radius;
    
        public float FillAmount
        {
            get => _fillAmount;
            set
            {
                _fillAmount = Mathf.Clamp01(value);
                UpdateRange();
            }
        }

        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _segments = 36;
        [Range(0, 1)] [SerializeField] private float _fillAmount = 1f;
        [SerializeField] private float _startAngle = 0f;

        private void OnValidate()
        {
            UpdateRange();
        }

        private void UpdateRange()
        {
            if (_lineRenderer == null) return;

            if (_fillAmount <= 0f)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            if (_fillAmount >= 1f)
            {
                _lineRenderer.positionCount = _segments;
                _lineRenderer.loop = true;
                float angleStep = 360f / _segments;
                for (int i = 0; i < _segments; i++)
                {
                    float angle = _startAngle + i * angleStep;
                    Vector3 point = PointOnCircle(angle);
                    _lineRenderer.SetPosition(i, point);
                }
            }
            else
            {
                float totalAngle = 360f * _fillAmount;
                int pointCount = Mathf.Max(2, Mathf.CeilToInt(_segments * _fillAmount) + 1);
                _lineRenderer.positionCount = pointCount;
                _lineRenderer.loop = false;

                float angleStep = -totalAngle / (pointCount - 1);
                for (int i = 0; i < pointCount; i++)
                {
                    float angle = _startAngle + i * angleStep;
                    Vector3 point = PointOnCircle(angle);
                    _lineRenderer.SetPosition(i, point);
                }
            }
        }

        private Vector3 PointOnCircle(float angleDegrees)
        {
            float rad = angleDegrees * Mathf.Deg2Rad;
            float x = _radius * Mathf.Cos(rad);
            float y = _radius * Mathf.Sin(rad);
            return new Vector3(x, y, 0f);
        }
    }
}