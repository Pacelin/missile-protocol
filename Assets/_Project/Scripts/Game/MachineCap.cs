using UnityEngine;
using UnityEngine.Rendering;

namespace Project.Game
{
    public class MachineCap : MonoBehaviour
    {
        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private int _sortingOrderWhenRelease;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _destroyHeight;
        [SerializeField] private Vector2 _releaseForceRotationRange;
        [SerializeField] private Vector2 _releaseForceRange;
        [SerializeField] private Vector2 _angularForceRange;
        
        private bool _released;
        
        private void Awake()
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Release()
        {
            _released = true;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;

            var forceRotation = Random.Range(_releaseForceRotationRange.x, _releaseForceRotationRange.y);
            var force = Random.Range(_releaseForceRange.x, _releaseForceRange.y);
            var angularForce = Random.Range(_angularForceRange.x, _angularForceRange.y);

            var direction = Quaternion.Euler(0, 0, forceRotation) * Vector3.up;

            _rigidbody.linearVelocity = direction * force;
            _rigidbody.angularVelocity = angularForce;
            _sortingGroup.sortingOrder = _sortingOrderWhenRelease;
        }

        private void Update()
        {
            if (!_released)
                return;
            
            if (_rigidbody.position.y <= _destroyHeight)
                Destroy(gameObject);
        }
    }
}