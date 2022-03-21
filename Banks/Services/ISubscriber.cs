namespace Banks.Services
{
    public interface ISubscriber
    {
        public void HandleEvent(string notfication);
    }
}