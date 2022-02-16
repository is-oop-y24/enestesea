using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Classes;

public class RestorePoint
{
    private readonly List<Storage> _storages;
    private readonly DateTime _dateTime;

    public RestorePoint(DateTime dateTime)
    {
        _storages = new List<Storage>();
        _dateTime = dateTime;
    }

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