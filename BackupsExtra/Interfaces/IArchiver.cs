using System.Collections.Generic;
using BackupsExtra.Entities;

namespace BackupsExtra.Interfaces
{
    public interface IArchiver
    {
        bool IsMergeble { get; }
        List<Storage> Run(string directoryPath, IRepository repository, IEnumerable<JobObject> jobObjects);
    }
}