using System;
using Project.Game.Missles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Game.Map
{
    public class MapMisslesSpawner : MonoBehaviour
    {
        public int AliveMissles => _aliveMissles;
        
        [SerializeField] private MapMissle _prefab;
        [SerializeField] private TargetMark _markPrefab;
        [SerializeField] private Vector2 _spawnDistanceRange;
        [SerializeField] private MissleConfiguration[] _missleConfigurations;

        private int _aliveMissles;

        public void Spawn(Vector3 position)
        {
            var missleConfiguration = _missleConfigurations[Random.Range(0, _missleConfigurations.Length)];
            var filters = Enum.GetValues(typeof(EFilter));
            var filter = (EFilter)filters.GetValue(Random.Range(0, filters.Length));

            var mark = Instantiate(_markPrefab);
            var missle = Instantiate(_prefab, position, Quaternion.identity);
            missle.Setup(filter, missleConfiguration, mark, this);
            
            _aliveMissles++;
        }
        
        public void Spawn()
        {
            var direction = Random.insideUnitCircle.normalized;
            var distance = Random.Range(_spawnDistanceRange.x, _spawnDistanceRange.y);
            var spawnPoint = G.ShipModel.Position + direction * distance;
            Spawn(spawnPoint);
        }

        public void OnMissleExplode()
        {
            _aliveMissles--;
        }
    }
}