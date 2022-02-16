using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Tools
{
    [Serializable]
    public class DateAlgorithm : ICleaner
    {
        private DateTime _dateTime;

        public DateAlgorithm(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public List<RestorePoint> Selection(List<RestorePoint> restorePoints)
        {
            List<RestorePoint> listOfPoints = restorePoints.TakeWhile(t => t.DateTime <= _dateTime).ToList();
            return listOfPoints;
        }
    }
}