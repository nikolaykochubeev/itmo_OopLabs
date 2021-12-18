using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class Deleter : ICleaningType
    {
        public void Clean(RestorePoint oldRestorePoint, RestorePoint youngRestorePoint)
        {
            foreach (Storage storage in oldRestorePoint.Storages)
            {
                storage.DeleteArchive();
            }
        }
    }
}