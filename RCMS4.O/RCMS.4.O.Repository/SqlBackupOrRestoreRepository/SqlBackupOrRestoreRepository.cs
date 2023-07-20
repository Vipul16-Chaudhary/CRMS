using RCMS._4.O.Core.Component;
using RCMS._4.O.Core.Component.SqlBackupOrRestoreComponent;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Entities.SqlBackupOrRestoreEntity;
using RCMS._4.O.Interfaces.SqlBackupOrRestoreInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Repository.SqlBackupOrRestoreRepository
{
    public class SqlBackupOrRestoreRepository : ISqlBackupOrRestoreInterface
    {
        public Task<List<SqlNameDatabaseEntity>> GetAllDatabaseName()
        {
            try
            {
                return Task.FromResult(new SqlDatabaseDataAccess().GetAllDatabaseName());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<SqlNameDatabaseEntity>());
            }
        }

        public Task<List<SqlInstanceEntity>> GetAllDbInstanceName()
        {
            try
            {
                return Task.FromResult(new SqlDatabaseDataAccess().GetAllDbInstanceName());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<SqlInstanceEntity>());
            }
        }

        public Task<bool> StartDatabaseBackUp()
        {
            bool resut = false;
            try
            {
                return Task.FromResult(new SqlDatabaseDataAccess().StartDatabaseBackUp());
            }
            catch (Exception ex)
            {
                return Task.FromResult(resut);
            }
        }

        public Task<bool> RestoreDatabaseBackUp()
        {
            bool resut = false;
            try
            {
                return Task.FromResult(new SqlDatabaseDataAccess().RestoreDatabaseBackUp());
            }
            catch (Exception ex)
            {
                return Task.FromResult(resut);
            }
        }
    }
}
