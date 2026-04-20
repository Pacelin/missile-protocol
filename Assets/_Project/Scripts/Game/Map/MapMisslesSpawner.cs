using System;
using System.Collections.Generic;
using Plugins.Audio;
using Project.Game.Missles;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Game.Map
{
    public class MapMisslesSpawner : MonoBehaviour
    {
        public int AliveMissles => _missles.Count;
        
        [SerializeField] private MapMissle _prefab;
        [SerializeField] private TargetMark _markPrefab;
        [SerializeField] private Vector2 _spawnDistanceRange;
        [SerializeField] private MissleConfiguration[] _missleConfigurations;
        [Space]
        [SerializeField] private Vector2 _misslePingDistances = new Vector2(1, 200);
        [SerializeField] private Vector2 _misslePingDelays = new Vector2(0.05f, 2);
        [Space] 
        [SerializeField] private Vector2 _speedDistanceRange;
        [SerializeField] private Vector2 _misslesSpeedByDistance = new Vector2(3, 12);
        [SerializeField] private Transform _gameFinish;
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        
        private readonly List<MapMissle> _missles = new List<MapMissle>();
        private float _lastPingTime;
        
        public void Spawn(Vector3 position)
        {
            var missleConfiguration = _missleConfigurations[Random.Range(0, _missleConfigurations.Length)];
            var filters = Enum.GetValues(typeof(EFilter));
            var filter = (EFilter)filters.GetValue(Random.Range(0, filters.Length));

            var distance = Vector2.Distance(G.ShipModel.Position, (Vector2) _gameFinish.position);
            var t = Mathf.Clamp01(Mathf.InverseLerp(_speedDistanceRange.x, _speedDistanceRange.y, distance));
            var missleSpeed = Mathf.Lerp(_misslesSpeedByDistance.x, _misslesSpeedByDistance.y, 1 - t);
            
            var mark = Instantiate(_markPrefab);
            var missle = Instantiate(_prefab, position, Quaternion.identity);
            missle.Setup(filter, missleConfiguration, mark, missleSpeed, this);
            
            _missles.Add(missle);
        }
        
        public void Spawn()
        {
            var direction = Random.insideUnitCircle.normalized;
            var distance = Random.Range(_spawnDistanceRange.x, _spawnDistanceRange.y);
            var spawnPoint = G.ShipModel.Position + direction * distance;
            Spawn(spawnPoint);
        }

        public void OnMissleExplode(MapMissle missle, bool damage)
        {
            if (damage)
            {
                G.ShipModel.TakeDamage(1);
                AudioSystem.Game_Map_MissileExplosion.PlayOneShot();
                _impulseSource.GenerateImpulse();
            }
            _missles.Remove(missle);
        }

        private void Update()
        {
            if (G.PauseController.HasAnyPause)
                return;

            if (_missles.Count == 0)
                return;

            var nearest = Vector2.Distance(_missles[0].transform.position, G.ShipModel.Position);
            for (int i = 1; i < _missles.Count; i++)
            {
                var distance = Vector2.Distance(_missles[i].transform.position, G.ShipModel.Position);
                if (distance < nearest)
                    nearest = distance;
            }

            var t = 1 - Mathf.InverseLerp(_misslePingDistances.x, _misslePingDistances.y, nearest);
            var delay = Mathf.Lerp(_misslePingDelays.x, _misslePingDelays.y, 1 - t * t);
            if (_lastPingTime + delay < Time.time)
            {
                AudioSystem.Game_Map_MissilePing.PlayOneShot();
                _lastPingTime = Time.time;
            }
        }
    }
}