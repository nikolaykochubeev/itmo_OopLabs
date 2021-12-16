using System.IO;
using System.Linq;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class Merger : ICleaningType
    {
        public void Clean(RestorePoint oldRestorePoint, RestorePoint youngRestorePoint, bool isMergeable)
        {
            if (!isMergeable)
                new Deleter().Clean(oldRestorePoint, youngRestorePoint, false);
            foreach (Storage storage in oldRestorePoint.Storages)
            {
                Storage youngStorage = youngRestorePoint.Storages.FirstOrDefault(youngStorage => youngStorage.ArchivedObjects.First().Id == storage.ArchivedObjects.First().Id);
                if (youngStorage is null)
                {
                    youngRestorePoint.AddStorage(storage);
                }
                else
                {
                    File.Delete(storage.ArchivePath);
                }
            }
        }
    }
}