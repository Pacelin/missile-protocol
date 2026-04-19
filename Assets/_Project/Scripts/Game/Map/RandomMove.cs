using UnityEngine;

namespace Project.Game.Map
{
    public class RandomMove : MonoBehaviour
    {
        [SerializeField] private Vector2 _randomLinearRange;
        [SerializeField] private Vector2 _randomAngularRange;

        private Vector2 _linear;
        private float _angular;
        
        private void Awake()
        {
            _linear = Random.insideUnitCircle.normalized * Random.Range(_randomLinearRange.x, _randomLinearRange.y);
            _angular = Random.Range(_randomAngularRange.x, _randomAngularRange.y);
        }

        private void Update()
        {
            if (G.PauseController.HasAnyPause)
                return;

            transform.position += (Vector3) _linear * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, 0, _angular * Time.deltaTime);
        }
    }
}