using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools.Exceptions;

namespace BackupsExtra.Classes
{
    public class RestorePoint
    {
        private readonly List<Storage> _storages;
        private readonly DateTime _dateTime;
        private readonly int _number;
        private readonly IWayOfStorage _wayOfStorage;

        public RestorePoint(DateTime dateTime, int number, IWayOfStorage wayOfStorage, List<Storage> storages)
        {
            _dateTime = dateTime;
            _number = number;
            _wayOfStorage = wayOfStorage;
            _storages = storages;
        }

        public int Number => _number;
        public DateTime DateTime => _dateTime;
        public IWayOfStorage WayOfStorage => _wayOfStorage;
        public List<Storage> ListStorages => _storages.ToList();

        public void AddStorage(Storage storage)
        {
            if (storage != null)
            {
                _storages.Add(storage);
            }
            else
            {
                throw new NullException("Storage is invalid");
            }
        }

        public void AddStorages(List<Storage> storages)
        {
            _storages.AddRange(storages);
        }
    }
}