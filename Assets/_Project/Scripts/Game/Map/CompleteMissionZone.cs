using System.Threading;

namespace Project.Game.Map
{
    public class CompleteMissionZone : MapZoneHandler
    {
        protected override void OnEnterZone(CancellationToken cancellationToken)
        {
            G.ResultCanvas.Show(true);
        }

        protected override void OnExitZone()
        {
        }
    }
}