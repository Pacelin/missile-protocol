using System;
using Project.Game.Missles;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapMissle : MonoBehaviour
    {
        public static event Action<Vector3> OnExplode;
        
        public MissleConfiguration MissleConfiguration => _missleConfiguration;
        public EFilter Filter => _filter;
        
        [SerializeField] private MapLocation _location;
        [SerializeField] private float _speed;
        [SerializeField] private float _explodeDistance;
        
        private EFilter _filter;
        private MissleConfiguration _missleConfiguration;
        private TargetMark _targetMark;
        private MapMisslesSpawner _spawner;
        
        public void Setup(EFilter filter, MissleConfiguration missleConfiguration,
            TargetMark mark, MapMisslesSpawner spawner)
        {
            _filter = filter;
            _missleConfiguration = missleConfiguration;
            _targetMark = mark;
            _spawner = spawner;
            _targetMark.SetTarget(_location);
        }

        public void Explode()
        {
            _spawner.OnMissleExplode();
            OnExplode?.Invoke(transform.position);
            Destroy(gameObject);
            Destroy(_targetMark.gameObject);
        }
        
        private void Update()
        {
            if (G.PauseController.HasAnyPause)
                return;

            var t = transform;
            var position = t.position;
            var targetPosition = G.ShipModel.Position;
            
            var vector = (Vector3)targetPosition - position;
            position += vector.normalized * _speed;
            t.position = position;
            t.up = vector.normalized;

            if (vector.sqrMagnitude <= _explodeDistance * _explodeDistance)
            {
                G.ShipModel.TakeDamage(1);
                Explode();
            }
        }
    }
}