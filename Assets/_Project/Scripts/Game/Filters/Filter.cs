namespace Project.Game
{
    public abstract class Filter
    {
        private IFilterHandler _handler;

        public void SetHandler(IFilterHandler handler) => _handler = handler;
        protected void SendComplete() => _handler?.OnFilterComplete();
    }
}