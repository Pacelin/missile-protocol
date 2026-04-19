namespace Project.Game
{
    public abstract class SignalScreenState
    {
        protected readonly SignalScreenController ScreenController;
        protected SignalScreenState(SignalScreenController screenController)
        {
            ScreenController = screenController;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
    }
}