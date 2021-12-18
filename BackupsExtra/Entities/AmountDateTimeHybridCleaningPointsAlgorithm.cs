using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class AmountDateTimeHybridCleaningPointsAlgorithm : ICleaningPointsAlgorithm
    {
        private readonly uint _amount;
        private readonly bool _atLeastOneLimit;
        private readonly ICleaningType _cleaningType;
        private DateTime _dateTime;

        public AmountDateTimeHybridCleaningPointsAlgorithm(uint amount, DateTime dateTime, ICleaningType cleaningType, bool atLeastOneLimit)
        {
            _amount = amount;
            _dateTime = dateTime;
            _cleaningType = cleaningType;
            _atLeastOneLimit = atLeastOneLimit;
        }

        public void Clean(List<RestorePoint> restorePoints)
        {
            int restorePointsSizeAfterCleaning = restorePoints.Count;
            foreach (RestorePoint unused in restorePoints.Where(restorePoint =>
                         (restorePoints.Count - restorePoint.Number < _amount &&
                          restorePoint.CreationTime >= _dateTime) ||
                         (restorePoints.Count - restorePoint.Number < _amount && _atLeastOneLimit) ||
                         (restorePoint.CreationTime >= _dateTime && _atLeastOneLimit)))
            {
                restorePointsSizeAfterCleaning--;
            }

            if (restorePointsSizeAfterCleaning == 0)
                throw new BackupsException("all points must be deleted to match the limit");

            for (int i = 0; i < restorePoints.Count; i++)
            {
                if ((restorePoints.Count - restorePoints[i].Number >= _amount ||
                     restorePoints[i].CreationTime < _dateTime) &&
                    (restorePoints.Count - restorePoints[i].Number >= _amount || !_atLeastOneLimit) &&
                    (restorePoints[i].CreationTime < _dateTime || !_atLeastOneLimit)) continue;
                _cleaningType.Clean(restorePoints[i], restorePoints[i + 1]);
                restorePoints.Remove(restorePoints[i]);
            }
        }
    }
}