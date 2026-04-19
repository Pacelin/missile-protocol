namespace Project.Game
{
    public class SignalScreenSetupState : SignalScreenState
    {
        public SignalScreenSetupState(SignalScreenController screenController) : base(screenController)
        {
        }

        public override void OnEnter()
        {
            ScreenController.BitFilterController.gameObject.SetActive(false);
            ScreenController.WaveFilterController.gameObject.SetActive(false);
            ScreenController.Missles.gameObject.SetActive(false);
            ScreenController.WhenNothing.SetActive(false);
            ScreenController.WhenTargetError.SetActive(false);
            ScreenController.WhenTargetDestroyed.SetActive(false);
            ScreenController.WhenTargetLost.SetActive(false);
            
            ScreenController.SwitchState(new SignalScreenIdleState(ScreenController));
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}