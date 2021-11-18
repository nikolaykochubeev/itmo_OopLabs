using System.Collections.Generic;
using Backups.Entities;
using Backups.Services;

namespace Backups.Interfaces
{
    public interface IArchiver
    {
        List<Storage> Store(string directoryPath, params JobObject[] jobObjects);
    }
}