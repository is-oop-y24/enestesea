using Banks.Classes;

namespace Banks.Services
{
    public interface ISubscribed
    {
        public void AddSubscriber(ISubscriber subscriber);
        public void RemoveSubscriber(ISubscriber subscriber);
        public void NotifySubscribers(string notification);
    }
}