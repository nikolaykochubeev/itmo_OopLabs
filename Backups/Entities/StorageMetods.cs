using System;

namespace Backups.Entities
{
    public enum StorageMetods
    {
        /// <summary>
        /// Create archive with password
        /// </summary>
        WithPassword = 1,

        /// <summary>
        /// Create archive without password
        /// </summary>
        WithoutPassword = -1,
    }
}