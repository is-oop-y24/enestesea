using System;
using System.Collections.Generic;
using BackupsExtra.Classes;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Tools;
[Serializable]
public class AmountAlgorithm : ICleaner
{
    private int _amount;

    public AmountAlgorithm(int amount)
    {
        _amount = amount;
    }

    public List<RestorePoint> Selection(List<RestorePoint> restorePoints)
    {
        List<RestorePoint> listOfPoints = new List<RestorePoint>();
        for (int i = 0; i <= restorePoints.Count - _amount; i++)
        {
           listOfPoints.Add(restorePoints[i]);
        }

        return listOfPoints;
    }
}