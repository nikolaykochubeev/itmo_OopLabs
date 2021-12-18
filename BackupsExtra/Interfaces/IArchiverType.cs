using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IArchiverType
    {
        void Archivate(Storage storage, string folderPath);
    }
}