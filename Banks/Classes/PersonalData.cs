using System;
using System.Collections.Immutable;

namespace Banks.Classes
{
    public class PersonalData : IEquatable<PersonalData>
    {
        public PersonalData(string name, string lastName, string address, string passport)
        {
            Name = name;
            Passport = passport;
            LastName = lastName;
            Address = address;
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Passport { get;  set; }
        public string Address { get;  set; }

        public bool Equals(PersonalData other)
        {
            return other != null && this.Name == other.Name && this.Address == other.Address && this.Passport == other.Passport && this.LastName == other.LastName;
        }
    }
}