using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IStorageAlgorithm
    {
        List<Storage> Run(string directoryPath, IRepository repository, IArchiverType archiverType, IEnumerable<JobObject> jobObjects);
    }
}