using System.Collections.Generic;
using BackupsExtra.Classes;

namespace BackupsExtra.Interfaces
{
    public interface ICleaner
    {
        public List<RestorePoint> Selection(List<RestorePoint> restorePoints);
    }
}