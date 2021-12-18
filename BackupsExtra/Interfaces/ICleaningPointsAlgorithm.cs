using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface ICleaningPointsAlgorithm
    {
        void Clean(List<RestorePoint> restorePoints);
    }
}