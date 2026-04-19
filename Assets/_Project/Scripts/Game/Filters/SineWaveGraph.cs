using UnityEngine;

namespace Project.Game
{
    [RequireComponent(typeof(LineRenderer))]
    public class SineWaveGraph : MonoBehaviour
    {
        public float amplitude = 1f;
        public float frequency = 1f;
        public float speed = 1f;
        public float width = 10f;
        public int resolution = 100;
        public LineRenderer lineRenderer;

        private float phaseOffset;

        void Start()
        {

            lineRenderer.positionCount = resolution;
            lineRenderer.useWorldSpace = false;
            phaseOffset = 0f;

            UpdateGraph();
        }

        void Update()
        {
            phaseOffset += speed * Time.deltaTime;
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            if (lineRenderer == null) return;

            float stepX = width / (resolution - 1);
            float startX = -width * 0.5f;

            Vector3[] positions = new Vector3[resolution];

            for (int i = 0; i < resolution; i++)
            {
                float x = startX + i * stepX;
                float input = frequency * (x + phaseOffset);
                float y = amplitude * Mathf.Sin(input);

                positions[i] = new Vector3(x, y, 0f);
            }

            lineRenderer.SetPositions(positions);
        }

        private void OnValidate()
        {
            if (lineRenderer == null)
                lineRenderer = GetComponent<LineRenderer>();
        
            amplitude = Mathf.Max(0, amplitude);
            frequency = Mathf.Max(0, frequency);
            resolution = Mathf.Max(2, resolution);
            width = Mathf.Max(0.1f, width);

            if (lineRenderer != null)
            {
                lineRenderer.positionCount = resolution;
                UpdateGraph();
            }
        }
    }
}