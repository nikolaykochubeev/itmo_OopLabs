using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class DateTimeCleaningPointsAlgorithm : ICleaningPointsAlgorithm
    {
        private readonly ICleaningType _cleaningType;

        public DateTimeCleaningPointsAlgorithm(DateTime time, ICleaningType cleaningType)
        {
            Time = time;
            _cleaningType = cleaningType;
        }

        public DateTime Time { get; private set; }

        public void Clean(List<RestorePoint> restorePoints, bool isMergeable)
        {
            for (int i = 0; i < restorePoints.Count; i++)
            {
                if (restorePoints[i].CreationTime >= Time) continue;
                _cleaningType.Clean(restorePoints[i], restorePoints[i + 1], isMergeable);
                restorePoints.Remove(restorePoints[i]);
            }
        }

        public void ChangeTime(DateTime dateTime)
        {
            Time = dateTime;
        }
    }
}