using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapFillFuelZone : MapZoneHandler
    {
        [SerializeField] private float _fillSpeed;

        protected override void OnEnterZone(CancellationToken cancellationToken)
        {
            UniTask.Void(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.NextFrame(cancellationToken: cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                    G.ShipModel.Fuel.Fill(_fillSpeed * Time.deltaTime);
                }
            });
        }

        protected override void OnExitZone()
        {
        }
    }
}