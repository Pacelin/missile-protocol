using System;
using Project.Game.Map;
using UnityEngine;

namespace Project.Game
{
    public class RadarModel
    {
        public event Action OnAngleChanged;
        public event Action<MapLocation, Vector2> OnPingPosition;

        public MachineSliderHandle SatelliteHandle => _satelliteHandle;
        public float Angle => _angle;
        public float ScanDistance => _scanDistance;
        
        private float _angle;

        private readonly float _speed;
        private readonly float _scanDistance;
        private readonly float _scanAngle;
        private readonly float _lockAngle;
        private readonly float _satelliteAngle;
        private readonly MachineSliderHandle _satelliteHandle;
        
        public RadarModel(float startAngle, float speed, float scanDistance, float scanAngle,
            float lockAngle, float satelliteAngle, MachineSliderHandle satelliteHandle)
        {
            _angle = startAngle;
            _speed = speed;
            _scanDistance = scanDistance;
            _scanAngle = scanAngle;
            _lockAngle = lockAngle;
            _satelliteAngle = satelliteAngle;
            _satelliteHandle = satelliteHandle;
        }
        
        public void Tick(float deltaTime)
        {
            float prevAngle = _angle;
            _angle = NormalizeAngle(_angle + _speed * deltaTime);
            OnAngleChanged?.Invoke();

            UpdatePings(prevAngle);
        }

        public MapLocation TryLockTarget()
        {
            var halfLockAngle = _lockAngle * 0.5f;
            var halfSatelliteAngle = _satelliteAngle * 0.5f;
            var satelliteDelta = Mathf.Lerp(-halfSatelliteAngle, halfSatelliteAngle, 
                _satelliteHandle.NormalizedValue);
            var scanAngle1 = NormalizeAngle(satelliteDelta - halfLockAngle);
            var scanAngle2 = NormalizeAngle(satelliteDelta + halfLockAngle);
            foreach (var mapZone in MapLocation.AvailableLocations)
            {
                if (mapZone)
                {
                    if (IsPositionAvailable(mapZone.Position, scanAngle1, scanAngle2, out _))
                        return mapZone;
                }
            }

            return null;
        }

        public bool CanLockOnTarget(Vector2 position)
        {
            var halfLockAngle = _lockAngle * 0.5f;
            var halfSatelliteAngle = _satelliteAngle * 0.5f;
            var satelliteDelta = Mathf.Lerp(-halfSatelliteAngle, halfSatelliteAngle, 
                _satelliteHandle.NormalizedValue);
            var scanAngle1 = NormalizeAngle(satelliteDelta - halfLockAngle);
            var scanAngle2 = NormalizeAngle(satelliteDelta + halfLockAngle);
            return IsPositionAvailable(position, scanAngle1, scanAngle2, out _);
        }

        private void UpdatePings(float prevAngle)
        {
            var halfScanAngle = _scanAngle * 0.5f; // 315 - 45
            var scanAngle1 = NormalizeAngle(-halfScanAngle); // -45 -> 315
            var scanAngle2 = NormalizeAngle(halfScanAngle); // 45 -> 45

            if (IsInSector(_angle, scanAngle1, scanAngle2) ||
                IsInSector(prevAngle, scanAngle1, scanAngle2))
                CheckMapZones(prevAngle);
        }

        private bool IsInSector(float angle, float start, float end)
        {
            if (start < end)
                return angle >= start && angle <= end;

            return angle >= start || angle <= end;
        }
        
        private float NormalizeAngle(float angle) => (angle % 360 + 360) % 360;
        
        private void CheckMapZones(float firstAngle)
        {
            var secondAngle = _angle;
            if (_speed < 0)
                (firstAngle, secondAngle) = (secondAngle, firstAngle);
            
            var scanAngle1 = NormalizeAngle(G.ShipModel.Rotation + firstAngle);
            var scanAngle2 = NormalizeAngle(G.ShipModel.Rotation + secondAngle);

            foreach (var mapZone in MapLocation.AvailableLocations)
            {
                if (mapZone)
                {
                    if (IsPositionAvailable(mapZone.Position, scanAngle1, scanAngle2,
                            out var vector))
                        OnPingPosition?.Invoke(mapZone, vector);
                }
            }
        }

        private bool IsPositionAvailable(Vector2 position, float scanAngle1, float scanAngle2,
            out Vector2 vector)
        {
            var shipPosition = G.ShipModel.Position;
            vector = position - shipPosition;
            if (vector.sqrMagnitude > _scanDistance * _scanDistance)
                return false;

            var angle = NormalizeAngle(Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90);

            return IsInSector(angle, scanAngle1, scanAngle2);
        }
    }
}