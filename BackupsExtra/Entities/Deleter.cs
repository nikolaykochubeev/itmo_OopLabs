using System.IO;
using BackupsExtra.Interfaces;
using BackupsExtra.Tools;

namespace BackupsExtra.Entities
{
    public class Deleter : ICleaningType
    {
        public void Clean(RestorePoint oldRestorePoint, RestorePoint youngRestorePoint, bool isMergeable)
        {
            foreach (Storage storage in oldRestorePoint.Storages)
            {
                File.Delete(storage.ArchivePath);
            }
        }
    }
}