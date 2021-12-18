using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface ICleaningType
    {
        void Clean(RestorePoint oldRestorePoint, RestorePoint youngRestorePoint);
    }
}