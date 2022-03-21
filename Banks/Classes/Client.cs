using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Services;

namespace Banks.Classes
{
    public class Client : ISubscriber, IEquatable<Client>
    {
        private readonly PersonalData _personalData;
        public Client(PersonalData personalData)
        {
            _personalData = personalData;
            Notifications = new List<string>();
            ClientId = Guid.NewGuid();
        }

        public List<string> Notifications { get; private set; }
        public Guid ClientId { get; }
        public PersonalData GetPersonalData()
        {
            return _personalData;
        }

        public void HandleEvent(string notfication)
        {
            Notifications.Add(notfication);
        }

        public bool Equals(Client other)
        {
            return other != null && this._personalData == other._personalData;
        }

        public bool CheckClient()
        {
            return !(_personalData.Address == null || _personalData.Passport == null);
        }
    }
}
