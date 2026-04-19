using System.Threading;
using UnityEngine;

namespace Project.Game.Map
{
    public class NoCollisionZone : MapZoneHandler
    {
        [SerializeField] private float _force;
        
        private bool _applyForce = false;

        protected override void OnEnterZone(CancellationToken cancellationToken) => _applyForce = true;
        protected override void OnExitZone() => _applyForce = false;

        private void Update()
        {
            if (_applyForce)
            {
                var vector = G.ShipModel.Position - (Vector2)transform.position;
                var distance = vector.magnitude;
                var t = distance / MapZoneRadius;
                var force = Mathf.Lerp(0, _force, t * t);
                G.ShipModel.LinearVelocity.ApplyForce(vector.normalized * force, Time.deltaTime);
            }
        }
    }
}