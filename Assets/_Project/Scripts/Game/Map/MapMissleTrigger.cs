using System.Threading;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapMissleTrigger : MapZoneHandler
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private MapMisslesSpawner _mapMisslesSpawner;
        [SerializeField] private int _maxAliveMissles;
        [SerializeField] private float[] _chances;
        [SerializeField] private int _countToSpawn;
        [SerializeField] private float[] _countChances;
        [SerializeField] private float _garantChance = 0.1f;

        [ContextMenu("RandomisAliveMissles")]
        private void Randomize()
        {
            var acc = 0f;
            var r = Random.Range(0, 1f);
            for (int i = 0; i < _chances.Length; i++)
            {
                acc += _chances[i];
                if (acc >= r)
                {
                    _maxAliveMissles = i;
                    break;
                }
            }
            
            
            var acc2 = 0f;
            var r2 = Random.Range(0, 1f);
            for (int i = 0; i < _countChances.Length; i++)
            {
                acc2 += _countChances[i];
                if (acc2 >= r2)
                {
                    _countToSpawn = i + 1;
                    break;
                }
            }

            var r3 = Random.Range(0, 1f);
            if (_garantChance >= r3)
            {
                _maxAliveMissles = int.MaxValue;
                _countToSpawn = 1;
            }
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!_mapMisslesSpawner)
                _mapMisslesSpawner = FindFirstObjectByType<MapMisslesSpawner>();
        }

        protected override void OnEnterZone(CancellationToken cancellationToken)
        {
            if (_mapMisslesSpawner.AliveMissles > _maxAliveMissles)
                return;

            for (int i = 0; i < _countToSpawn; i++)
            {
                if (_spawnPoint)
                    _mapMisslesSpawner.Spawn(_spawnPoint.position);
                else
                    _mapMisslesSpawner.Spawn();
            }
            
            Destroy(gameObject);
        }

        protected override void OnExitZone()
        {
        }
    }
}