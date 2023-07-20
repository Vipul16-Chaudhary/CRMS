using RCMS._4.O.Common;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Entities.SqlBackupOrRestoreEntity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Core.Component.SqlBackupOrRestoreComponent
{
    public class SqlDatabaseDataAccess
    {
        public List<SqlNameDatabaseEntity> GetAllDatabaseName()
        {
            string SQL = string.Empty;
            DataTableReader dr;
            SqlNameDatabaseEntity objDbName = new SqlNameDatabaseEntity();
            List<SqlNameDatabaseEntity> lstDBName = new List<SqlNameDatabaseEntity>();
            try
            {
                SQL = "sp_GetAllDatabaseName";
                dr = DatabaseHelpers.ExecuteQuery.ExecuteReader(SQL, Enums.ConnectionType.RCMS);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objDbName = new SqlNameDatabaseEntity
                        {
                            //Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]),
                            DatabaseName = Convert.ToString(dr["Name"] == DBNull.Value ? string.Empty : dr["Name"]),
                        };
                        lstDBName.Add(objDbName);
                    }
                }
                dr.Close();
                return lstDBName;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message.ToString());
            }
            finally
            {
                lstDBName = null;
            }
        }

        public List<SqlInstanceEntity> GetAllDbInstanceName()
        {
            string SQL = string.Empty;
            DataTableReader dr;
            SqlInstanceEntity objDbInstance = new SqlInstanceEntity();
            List<SqlInstanceEntity> lstDbInstance = new List<SqlInstanceEntity>();
            try
            {
                SQL = "sp_GetAllDbInstanceName";
                dr = DatabaseHelpers.ExecuteQuery.ExecuteReader(SQL, Enums.ConnectionType.RCMS);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        objDbInstance = new SqlInstanceEntity
                        {
                            //Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]),
                            DbInstanceName = Convert.ToString(dr["Name"] == DBNull.Value ? string.Empty : dr["Name"]),
                        };
                        lstDbInstance.Add(objDbInstance);
                    }
                }
                dr.Close();
                return lstDbInstance;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message.ToString());
            }
            finally
            {
                lstDbInstance = null;
            }
        }

        public bool StartDatabaseBackUp()
        {
            string SQL = string.Empty;
            SqlHelpers.ParameterList param = new SqlHelpers.ParameterList();
            SQL = "sp_TakeBackupDatabase";
            //param.Add(new SQLParameter("@DBName", DBName));  //As per request
            //param.Add(new SQLParameter("@InstanceName", InstanceName)); //As per request
            bool result = DatabaseHelpers.ExecuteQuery.ExecuteNonQueryWithStatus(SQL, param, Enums.ConnectionType.RCMS);
            return result;
        }

        public bool RestoreDatabaseBackUp()
        {
            string SQL = string.Empty;
            SqlHelpers.ParameterList param = new SqlHelpers.ParameterList();
            SQL = "sp_TakeBackupRestoreDatabase";
            //param.Add(new SQLParameter("@DBName", DBName));  //As per request
            //param.Add(new SQLParameter("@InstanceName", InstanceName)); //As per request
            bool result = DatabaseHelpers.ExecuteQuery.ExecuteNonQueryWithStatus(SQL, param, Enums.ConnectionType.RCMS);
            return result;
        }
    }
}
