using RCMS._4.O.Entities.SqlBackupOrRestoreEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.SqlBackupOrRestoreInterface
{
    public interface ISqlBackupOrRestoreInterface
    {
        Task<List<SqlNameDatabaseEntity>> GetAllDatabaseName();
        Task<List<SqlInstanceEntity>> GetAllDbInstanceName();
        Task<bool> StartDatabaseBackUp();
        Task<bool> RestoreDatabaseBackUp();
    }
}
