using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Game.Map
{
    public class MapRepairZone : MapZoneHandler
    {
        [SerializeField] private float _repairDuration;
        [SerializeField] private CircleFill _fill;

        protected override void OnEnterZone(CancellationToken cancellationToken)
        {
            UniTask.Void(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.NextFrame(cancellationToken: cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                    if (G.ShipModel.Durability == G.ShipModel.MaxDurability)
                        continue;

                    float time = 0f;
                    while (!cancellationToken.IsCancellationRequested && time < _repairDuration)
                    {
                        await UniTask.NextFrame(cancellationToken: cancellationToken);
                        cancellationToken.ThrowIfCancellationRequested();
    
                        time += Time.deltaTime;
                        _fill.FillAmount = time / _repairDuration;
                    }
                    
                    G.ShipModel.Heal(1);
                }
            });
        }

        protected override void OnExitZone()
        {
            _fill.FillAmount = 0;
        }
    }
}