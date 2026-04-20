using System;
using Plugins.Audio;
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
        [SerializeField] private float _explodeDistance;
        
        private float _speed;
        private EFilter _filter;
        private MissleConfiguration _missleConfiguration;
        private TargetMark _targetMark;
        private MapMisslesSpawner _spawner;
        
        public void Setup(EFilter filter, MissleConfiguration missleConfiguration,
            TargetMark mark, float missleSpeed, MapMisslesSpawner spawner)
        {
            _filter = filter;
            _missleConfiguration = missleConfiguration;
            _targetMark = mark;
            _speed = missleSpeed;
            _spawner = spawner;
            _targetMark.SetTarget(_location);
        }

        public void Explode(bool damage)
        {
            _spawner.OnMissleExplode(this, damage);
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
            position += vector.normalized * (_speed * Time.deltaTime);
            t.position = position;
            t.up = vector.normalized;

            if (vector.sqrMagnitude <= _explodeDistance * _explodeDistance)
            {
                Explode(true);
            }
        }
    }
}