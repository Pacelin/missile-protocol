using Project.Game.Map;

namespace Project.Game
{
    public class SignalScreenSolveFilterState : SignalScreenState, IFilterHandler
    {
        private IFilterController _filterController;
        private readonly MapMissle _missle;
        
        public SignalScreenSolveFilterState(SignalScreenController screenController,
            MapMissle missle) : base(screenController)
        {
            _missle = missle;
        }

        public override void OnEnter()
        {
            var filter = _missle.Filter;
            if (filter == EFilter.Bit)
                _filterController = ScreenController.BitFilterController;
            else
                _filterController = ScreenController.WaveFilterController;
            
            _filterController.StartFilter(this);
        }

        public override void OnExit()
        {
            _filterController.StopFilter();
        }

        public override void OnUpdate()
        {
            if (!_missle || ScreenController.CheckSliderMove())
                ScreenController.SwitchTargetLost();
        }

        public void OnFilterComplete()
        {
            ScreenController.SwitchState(new SignalScreenMessageState(ScreenController,
                ScreenController.WhenSolveFilter, () =>
                {
                    if (!_missle || ScreenController.CheckSliderMove())
                        ScreenController.SwitchTargetLost();
                    else
                        ScreenController.SwitchState(new SignalScreenSolveMissleState(
                            ScreenController, _missle));
                }));
        }
    }
}