using System.Threading;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapMissleTrigger : MapZoneHandler
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private MapMisslesSpawner _mapMisslesSpawner;
        [SerializeField] private int _maxAliveMissles;

        protected override void OnEnterZone(CancellationToken cancellationToken)
        {
            if (_mapMisslesSpawner.AliveMissles >= _maxAliveMissles)
                return;
            
            if (_spawnPoint)
                _mapMisslesSpawner.Spawn(_spawnPoint.position);
            else
                _mapMisslesSpawner.Spawn();
            
            Destroy(gameObject);
        }

        protected override void OnExitZone()
        {
        }
    }
}