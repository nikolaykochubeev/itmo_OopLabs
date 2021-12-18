using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra.Entities
{
    public class AmountCleaningPointsAlgorithm : ICleaningPointsAlgorithm
    {
        private readonly uint _amount;
        private readonly ICleaningType _cleaningType;
        public AmountCleaningPointsAlgorithm(uint amount, ICleaningType cleaningType)
        {
            _amount = amount;
            _cleaningType = cleaningType;
        }

        public void Clean(List<RestorePoint> restorePoints)
        {
            for (int i = 0; i < restorePoints.Count - (_amount - 1); i++)
            {
                _cleaningType.Clean(restorePoints[i], restorePoints[i + 1]);
                restorePoints.Remove(restorePoints[i]);
            }
        }
    }
}