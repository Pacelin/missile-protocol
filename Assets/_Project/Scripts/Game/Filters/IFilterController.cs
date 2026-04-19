namespace Project.Game
{
    public interface IFilterController
    {
        void StartFilter(IFilterHandler filterHandler);
        void StopFilter();
    }
}