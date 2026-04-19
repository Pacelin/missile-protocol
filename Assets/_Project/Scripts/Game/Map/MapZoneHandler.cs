using System.Threading;
using UnityEngine;

namespace Project.Game.Map
{
    [RequireComponent(typeof(MapZone))]
    public abstract class MapZoneHandler : MonoBehaviour
    {
        protected float MapZoneRadius => _zone.Radius;
        
        [SerializeField] private MapZone _zone;

        private bool _isInZone;
        private CancellationTokenSource _cts;
        
        private void OnValidate()
        {
            if (!_zone)
                _zone = GetComponent<MapZone>();
        }

        private void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void FixedUpdate()
        {
            var playerPosition = G.ShipModel.Position;
            var zonePosition = (Vector2) transform.position;
            var distanceToPlayer = Vector2.Distance(playerPosition, zonePosition);

            bool isInZone = distanceToPlayer <= _zone.Radius;
            if (_isInZone != isInZone)
            {
                _isInZone = isInZone;
                if (_isInZone)
                {
                    _cts = new CancellationTokenSource();
                    OnEnterZone(_cts.Token);
                }
                else
                {
                    if (_cts != null)
                    {
                        _cts.Cancel();
                        _cts.Dispose();
                        _cts = null;
                    }
                    OnExitZone();
                }
            }
        }

        protected abstract void OnEnterZone(CancellationToken cancellationToken);
        protected abstract void OnExitZone();
    }
}