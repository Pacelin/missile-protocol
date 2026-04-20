using Plugins.Audio;
using Project.Game.Map;

namespace Project.Game
{
    public class SignalScreenSolveMissleState : SignalScreenState
    {
        private readonly MapMissle _missle;
        
        public SignalScreenSolveMissleState(SignalScreenController screenController,
            MapMissle missle) : base(screenController)
        {
            _missle = missle;
        }

        public override void OnEnter()
        {
            ScreenController.Missles.Setup(_missle.MissleConfiguration);
            ScreenController.Missles.gameObject.SetActive(true);
            
            ScreenController.SolvePanel.ApplyButton.OnClick += OnApplyClick;
        }

        public override void OnExit()
        {
            ScreenController.Missles.gameObject.SetActive(false);
            
            ScreenController.SolvePanel.ApplyButton.OnClick -= OnApplyClick;
        }

        public override void OnUpdate()
        {
            if (!_missle || ScreenController.CheckSliderMove())
                ScreenController.SwitchTargetLost();
        }

        private void OnApplyClick()
        {
            if (ScreenController.SolvePanel.IsCorrect(_missle.MissleConfiguration))
            {
                AudioSystem.Game_Machines_SignalSend.PlayOneShot();
                _missle.Explode(false);
                ScreenController.SwitchState(new SignalScreenMessageState(ScreenController,
                    ScreenController.WhenTargetDestroyed, false,
                    () =>
                    {
                        ScreenController.SwitchState(new SignalScreenIdleState(ScreenController));
                    }));
            }
            else
            {
                AudioSystem.Game_Machines_SignalSendFail.PlayOneShot();
                ScreenController.SwitchState(new SignalScreenMessageState(ScreenController,
                    ScreenController.WhenTargetError, false,
                    () =>
                    {
                        if (!_missle || ScreenController.CheckSliderMove())
                            ScreenController.SwitchTargetLost();
                        else
                            ScreenController.SwitchState(this);
                    }));
            }
        }
    }
}