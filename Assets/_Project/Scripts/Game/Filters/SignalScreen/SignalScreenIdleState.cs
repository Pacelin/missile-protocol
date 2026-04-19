using Project.Game.Map;

namespace Project.Game
{
    public class SignalScreenIdleState : SignalScreenState
    {
        public SignalScreenIdleState(SignalScreenController screenController) : base(screenController)
        {
        }

        public override void OnEnter()
        {
            ScreenController.SnapTargetButton.OnClick += OnSnapTargetClick;
            ScreenController.WhenNothing.SetActive(true);
        }

        public override void OnExit()
        {
            ScreenController.SnapTargetButton.OnClick -= OnSnapTargetClick;
            ScreenController.WhenNothing.SetActive(false);
            ScreenController.ResetSliderHandleMoved();
        }

        public override void OnUpdate() { }

        private void OnSnapTargetClick()
        {
            var lockTarget = G.RadarModel.TryLockTarget();
            if (lockTarget && lockTarget.TryGetComponent<MapMissle>(out var missle))
                ScreenController.SwitchState(new SignalScreenSolveFilterState(ScreenController, missle));
        }
    }
}