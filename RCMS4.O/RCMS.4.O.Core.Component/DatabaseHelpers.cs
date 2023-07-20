using Microsoft.Data.SqlClient;
using RCMS._4.O.Common;
using RCMS._4.O.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RCMS._4.O.Core.Component
{
    /// <summary>
    /// Author: Yogesh
    /// Guide By: Vipul
    /// Creation Date: 21-06-2023
    /// Description: Database Fuctions
    /// </summary>
    public class DatabaseHelpers
    {
        
        #region SQL Helper
        /// <summary>
        /// Summary description for ExecuteQuery
        /// </summary>
        /// 

        public class ExecuteQuery
        {
            private static string _connectionString = "";

            public ExecuteQuery()
            {
                //
                // TODO: Add constructor logic here
                //
            }

            #region Public Properties

            public static string ConnectionString
            {
                get { return _connectionString; }
                set { _connectionString = value; }
            }

            #endregion

            #region Private Methods

            private static SqlCommand BuildBaseSqlCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = cmdType;
                cmd.Connection = conn;
                cmd.Transaction = transaction;
                cmd.CommandText = procedureName;
                foreach (SqlHelpers.SQLParameter param in SQLParam)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.ParameterValue));

                    if (param.DBType != null)
                        cmd.Parameters[cmd.Parameters.Count - 1].SqlDbType = (SqlDbType)param.DBType;

                    if (param.Size != -1)
                        cmd.Parameters[cmd.Parameters.Count - 1].Size = param.Size;

                    cmd.Parameters[cmd.Parameters.Count - 1].Direction = param.ParameterDirection;
                }
                return cmd;
            }

            private static SqlCommand BuildSqlCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                SqlCommand cmd = BuildBaseSqlCommand(conn, transaction, cmdType, procedureName, SQLParam);
                return cmd;
            }

            private static SqlCommand BuildSqlCommand(SqlConnection conn, SqlTransaction transaction, CommandType cmdType, string procedureName, SqlHelpers.ParameterList SQLParam, int timeOutDuration)
            {
                SqlCommand cmd = BuildBaseSqlCommand(conn, transaction, cmdType, procedureName, SQLParam);
                cmd.CommandTimeout = timeOutDuration;

                return cmd;
            }

            private static void StoreParameterOutputValues(SqlCommand cmd, SqlHelpers.ParameterList SQLParam)
            {
                foreach (SqlParameter param in cmd.Parameters)
                {
                    if (param.Direction != ParameterDirection.Input)
                        SQLParam.GetParameterByName(param.ParameterName).ParameterValue = param.Value;
                }
            }
            #endregion

            #region Public Methods

            public static  SqlDataAdapter ExecuteDataAdapter(string SQL)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString());
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    conn.Close();
                    return ad;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            //Added by Vipul and Yogesh
            public static SqlDataAdapter ExecuteDataAdapter(string SQL, Enums.ConnectionType contype)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString(contype));
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    conn.Close();
                    return ad;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }

            public static SqlDataAdapter ExecuteDataAdapter(string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString());
                    conn.Open();
                    ad = new SqlDataAdapter();
                    ad.SelectCommand = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam);
                    conn.Close();
                    return ad;
                }

                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }

            public static DataTableReader ExecuteReader(string SQL)
            {
                DataTableReader reader;
                DataSet ds;
                try
                {
                    ds = ExecuteDataSet(SQL);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Added by Vipul and Yogesh
            public static DataTableReader ExecuteReader(string SQL, Enums.ConnectionType contype)
            {
                DataTableReader reader;
                DataSet ds;

                try
                {
                    ds = ExecuteDataSet(SQL, contype);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static DataTableReader ExecuteReader(string SQL, int timeOut)
            {
                DataTableReader reader;
                DataSet ds;

                try
                {
                    ds = ExecuteDataSet(SQL, timeOut);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Added by Vipul and Yogesh
            public static DataTableReader ExecuteReader(string SQL, int timeOut, Enums.ConnectionType contype)
            {
                DataTableReader reader;
                DataSet ds;

                try
                {
                    ds = ExecuteDataSet(SQL, timeOut, contype);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            public static DataTableReader ExecuteReader(string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                DataTableReader reader;
                DataSet ds;

                try
                {
                    ds = ExecuteDataSet(procedureName, SQLParam);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }


            //Added by Vipul and Yogesh
            public static DataTableReader ExecuteReader(string procedureName, SqlHelpers.ParameterList SQLParam, int timeOut)
            {
                DataTableReader reader;
                DataSet ds;

                try
                {
                    ds = ExecuteDataSet(procedureName, SQLParam, timeOut);
                    reader = ds.CreateDataReader();
                    return reader;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Added by Vipul and Yogesh

            public static DataSet ExecuteDataSet(string SQL)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString());
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    ad.SelectCommand.CommandTimeout = 120;
                    ad.Fill(ds);
                    conn.Close();
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                    SqlHelpers.SQLParameter.ConType = Enums.ConnectionType.RCMS;
                }
            }
            //Added by Vipul and Yogesh

            public static DataSet ExecuteDataSet(string SQL, Enums.ConnectionType conType)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString(conType));
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    ad.Fill(ds);
                    conn.Close();

                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                }
            }

            //Added by Vipul and Yogesh
            public static DataSet ExecuteDataSet(string SQL, int timeOut)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString());
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    ad.SelectCommand.CommandTimeout = timeOut;
                    ad.Fill(ds);
                    conn.Close();
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            //Added by Vipul and Yogesh
            public static DataSet ExecuteDataSet(string SQL, int timeOut, Enums.ConnectionType conType)
            {
                SqlConnection conn = null;
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    conn = new SqlConnection(ConnectionManager.ConnectionString(conType));
                    conn.Open();
                    ad = new SqlDataAdapter(SQL, conn);
                    ad.SelectCommand.CommandTimeout = timeOut;
                    ad.Fill(ds);
                    conn.Close();
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();

                }
            }



            public static DataSet ExecuteDataSet(string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    ad = ExecuteDataAdapter(procedureName, SQLParam);
                    ad.Fill(ds);
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Added by Vipul and Yogesh
            public static DataSet ExecuteDataSet(string procedureName, SqlHelpers.ParameterList SQLParam, int timeOut)
            {
                SqlDataAdapter ad;
                DataSet ds = new DataSet();

                try
                {
                    ad = ExecuteDataAdapter(procedureName, SQLParam);
                    ad.SelectCommand.CommandTimeout = timeOut;

                    ad.Fill(ds);
                    return ds;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static DataTable ExecuteDataTable(string SQL)
            {
                SqlDataAdapter ad;
                DataTable dt = new DataTable();

                try
                {
                    ad = ExecuteDataAdapter(SQL);
                    ad.Fill(dt);
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            //Added by Vipul and Yogesh
            public static DataTable ExecuteDataTable(string SQL, Enums.ConnectionType conType)
            {
                SqlDataAdapter ad;
                DataTable dt = new DataTable();

                try
                {
                    ad = ExecuteDataAdapter(SQL, conType);
                    ad.Fill(dt);
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static DataTable ExecuteDataTable(string SQL, SqlHelpers.ParameterList SQLParam)
            {
                SqlDataAdapter ad;
                DataTable dt = new DataTable();

                try
                {
                    ad = ExecuteDataAdapter(SQL, SQLParam);
                    ad.Fill(dt);
                    return dt;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static void ExecuteNonQuery(string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                SqlConnection conn = new SqlConnection();

                try
                {
                    using (conn = new SqlConnection(ConnectionManager.ConnectionString()))
                    {
                        conn.Open();
                        using (SqlCommand cmd = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam))
                        {
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, SQLParam);
                        }
                        conn.Close();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            //Added by Vipul and Yogesh
            public static void ExecuteNonQuery(string procedureName, SqlHelpers.ParameterList SQLParam, Enums.ConnectionType conType)
            {
                SqlConnection conn = new SqlConnection();

                try
                {
                    using (conn = new SqlConnection(ConnectionManager.ConnectionString(conType)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam))
                        {
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, SQLParam);
                        }
                        conn.Close();

                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }

            //Added by Vipul and Yogesh
            public static bool ExecuteNonQueryWithStatus(string procedureName, SqlHelpers.ParameterList SQLParam, Enums.ConnectionType conType)
            {
                SqlConnection conn = new SqlConnection();
                bool isDataSaved = false;
                try
                {
                    using (conn = new SqlConnection(ConnectionManager.ConnectionString(conType)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam))
                        {
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, SQLParam);
                        }
                        conn.Close();
                        return isDataSaved=true;
                    }
                }
                catch (Exception)
                {
                    return isDataSaved;
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
                return isDataSaved;
            }
            //Added by Vipul and Yogesh 
            public static void ExecuteNonQuery(string procedureName, SqlHelpers.ParameterList SQLParam, int timeOutDuration, Enums.ConnectionType conType)
            {
                SqlConnection conn = new SqlConnection();

                try
                {
                    using (conn = new SqlConnection(ConnectionManager.ConnectionString(conType)))
                    {
                        conn.Open();
                        using (SqlCommand cmd = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam))
                        {
                            cmd.CommandTimeout = timeOutDuration;
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, SQLParam);
                        }
                        conn.Close();

                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            public static void ExecuteNonQuery(SqlHelpers.TransactionQueryList transactionQueryList)
            {
                SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString());
                SqlCommand cmd;
                SqlTransaction transaction;
                SqlHelpers.TransactionQuery transactionQuery;
                SqlHelpers.ForeignKeyList fkList;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < transactionQueryList.Count; i++)
                        {
                            transactionQuery = transactionQueryList[i];

                            // Set the direction of all primary keys in current transaction
                            // query
                            // Assumption: No two parameter/PK names are same
                            if (transactionQuery.PrimaryKeyList.Count > 0)
                            {
                                // for each primary keys of current query
                                for (int j = 0; j < transactionQuery.PrimaryKeyList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of primary key in the list
                                        // parameters of current query by comparing their names
                                        if (transactionQuery.PrimaryKeyList[j].KeyName.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // Set the direction of selected parameter
                                            transactionQuery.SQLParameters[k].ParameterDirection = ParameterDirection.InputOutput;
                                            // Work done for selected primary key. 
                                            // Break out of the loop.
                                            break;
                                        }
                                    }
                                }
                            }

                            // Set the value of foreign keys with the values of
                            // primary keys (returned by output parameter) of previous
                            // parent table
                            if (transactionQuery.PrimaryKeyQueryIndex != -1 && transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of "Source" key in the list
                                        // parameters of current query by comparing their names
                                        if (fkList[j].SourceKey.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // if "Source" key index is found
                                            // then loop through all the parameters of previous
                                            // parent table 
                                            for (int l = 0; l < transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters.Count; l++)
                                            {
                                                // to find the index of "Target" key in the
                                                // list of parameters by comparing their names
                                                if (fkList[j].TargetKey.ToUpper() == transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterName.ToUpper())
                                                {
                                                    // if "Target" key is found, then assign
                                                    // the value of selected key of previous parent
                                                    // table (returned by output parameter) to the
                                                    // selected key of parameters of current
                                                    // query
                                                    transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                                    // Work done for selected foreign key.
                                                    // Break out of the loop
                                                    break;
                                                }
                                            }
                                            // Work done for selected foreign key.
                                            // Break out of the loop
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    if (fkList[j].PrimaryKeyQueryIndex == -1)
                                        throw new System.Exception("Primary key query index is missing a value in foreign key definition.");

                                    // Find the the index of "Source" key in the list of
                                    // parameters of current query by comparing their names
                                    int k = -1;
                                    int l = -1;
                                    k = transactionQuery.SQLParameters.GetParameterIndexByName(fkList[j].SourceKey.ToUpper());
                                    // if "Source" key index is found
                                    if (k != -1)
                                    {
                                        // then find the index of "Target" key in the
                                        // list of parameters of previous parent table 
                                        // by comparing their names
                                        l = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters.GetParameterIndexByName(fkList[j].TargetKey.ToUpper());
                                        // if "Target" key is found, then assign
                                        // the value of selected key of previous parent
                                        // table (returned by output parameter) to the
                                        // selected key of parameters of current
                                        // query
                                        if (l != -1)
                                            transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                    }
                                }
                            }

                            // Build command and execute query
                            cmd = BuildSqlCommand(conn, transaction, CommandType.StoredProcedure, transactionQuery.ProcedureName, transactionQuery.SQLParameters);
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, transactionQuery.SQLParameters);
                        }

                        // All went well. Now commit the transaction and
                        // close the database connection
                        transaction.Commit();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            //Added by Vipul and Yogesh
            public static void ExecuteNonQuery(SqlHelpers.TransactionQueryList transactionQueryList, Enums.ConnectionType conType)
            {
                SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString(conType));
                SqlCommand cmd;
                SqlTransaction transaction;
                SqlHelpers.TransactionQuery transactionQuery;
                SqlHelpers.ForeignKeyList fkList;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < transactionQueryList.Count; i++)
                        {
                            transactionQuery = transactionQueryList[i];

                            // Set the direction of all primary keys in current transaction
                            // query
                            // Assumption: No two parameter/PK names are same
                            if (transactionQuery.PrimaryKeyList.Count > 0)
                            {
                                // for each primary keys of current query
                                for (int j = 0; j < transactionQuery.PrimaryKeyList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of primary key in the list
                                        // parameters of current query by comparing their names
                                        if (transactionQuery.PrimaryKeyList[j].KeyName.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // Set the direction of selected parameter
                                            transactionQuery.SQLParameters[k].ParameterDirection = ParameterDirection.InputOutput;
                                            // Work done for selected primary key. 
                                            // Break out of the loop.
                                            break;
                                        }
                                    }
                                }
                            }

                            // Set the value of foreign keys with the values of
                            // primary keys (returned by output parameter) of previous
                            // parent table
                            if (transactionQuery.PrimaryKeyQueryIndex != -1 && transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of "Source" key in the list
                                        // parameters of current query by comparing their names
                                        if (fkList[j].SourceKey.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // if "Source" key index is found
                                            // then loop through all the parameters of previous
                                            // parent table 
                                            for (int l = 0; l < transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters.Count; l++)
                                            {
                                                // to find the index of "Target" key in the
                                                // list of parameters by comparing their names
                                                if (fkList[j].TargetKey.ToUpper() == transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterName.ToUpper())
                                                {
                                                    // if "Target" key is found, then assign
                                                    // the value of selected key of previous parent
                                                    // table (returned by output parameter) to the
                                                    // selected key of parameters of current
                                                    // query
                                                    transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                                    // Work done for selected foreign key.
                                                    // Break out of the loop
                                                    break;
                                                }
                                            }
                                            // Work done for selected foreign key.
                                            // Break out of the loop
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    if (fkList[j].PrimaryKeyQueryIndex == -1)
                                        throw new System.Exception("Primary key query index is missing a value in foreign key definition.");

                                    // Find the the index of "Source" key in the list of
                                    // parameters of current query by comparing their names
                                    int k = -1;
                                    int l = -1;
                                    k = transactionQuery.SQLParameters.GetParameterIndexByName(fkList[j].SourceKey.ToUpper());
                                    // if "Source" key index is found
                                    if (k != -1)
                                    {
                                        // then find the index of "Target" key in the
                                        // list of parameters of previous parent table 
                                        // by comparing their names
                                        l = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters.GetParameterIndexByName(fkList[j].TargetKey.ToUpper());
                                        // if "Target" key is found, then assign
                                        // the value of selected key of previous parent
                                        // table (returned by output parameter) to the
                                        // selected key of parameters of current
                                        // query
                                        if (l != -1)
                                            transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                    }
                                }
                            }

                            // Build command and execute query
                            cmd = BuildSqlCommand(conn, transaction, CommandType.StoredProcedure, transactionQuery.ProcedureName, transactionQuery.SQLParameters);
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, transactionQuery.SQLParameters);
                        }

                        // All went well. Now commit the transaction and
                        // close the database connection
                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            //Added by Vipul and Yogesh
            public static void ExecuteNonQuery(SqlHelpers.TransactionQueryList transactionQueryList, int timeOutDuration, Enums.ConnectionType conType)
            {
                SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString(conType));
                SqlCommand cmd;
                SqlTransaction transaction;
                SqlHelpers.TransactionQuery transactionQuery;
                SqlHelpers.ForeignKeyList fkList;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < transactionQueryList.Count; i++)
                        {
                            transactionQuery = transactionQueryList[i];

                            // Set the direction of all primary keys in current transaction
                            // query
                            // Assumption: No two parameter/PK names are same
                            if (transactionQuery.PrimaryKeyList.Count > 0)
                            {
                                // for each primary keys of current query
                                for (int j = 0; j < transactionQuery.PrimaryKeyList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of primary key in the list
                                        // parameters of current query by comparing their names
                                        if (transactionQuery.PrimaryKeyList[j].KeyName.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // Set the direction of selected parameter
                                            transactionQuery.SQLParameters[k].ParameterDirection = ParameterDirection.InputOutput;
                                            // Work done for selected primary key. 
                                            // Break out of the loop.
                                            break;
                                        }
                                    }
                                }
                            }

                            // Set the value of foreign keys with the values of
                            // primary keys (returned by output parameter) of previous
                            // parent table
                            if (transactionQuery.PrimaryKeyQueryIndex != -1 && transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of "Source" key in the list
                                        // parameters of current query by comparing their names
                                        if (fkList[j].SourceKey.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // if "Source" key index is found
                                            // then loop through all the parameters of previous
                                            // parent table 
                                            for (int l = 0; l < transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters.Count; l++)
                                            {
                                                // to find the index of "Target" key in the
                                                // list of parameters by comparing their names
                                                if (fkList[j].TargetKey.ToUpper() == transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterName.ToUpper())
                                                {
                                                    // if "Target" key is found, then assign
                                                    // the value of selected key of previous parent
                                                    // table (returned by output parameter) to the
                                                    // selected key of parameters of current
                                                    // query
                                                    transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                                    // Work done for selected foreign key.
                                                    // Break out of the loop
                                                    break;
                                                }
                                            }
                                            // Work done for selected foreign key.
                                            // Break out of the loop
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    if (fkList[j].PrimaryKeyQueryIndex == -1)
                                        throw new System.Exception("Primary key query index is missing a value in foreign key definition.");

                                    // Find the the index of "Source" key in the list of
                                    // parameters of current query by comparing their names
                                    int k = -1;
                                    int l = -1;
                                    k = transactionQuery.SQLParameters.GetParameterIndexByName(fkList[j].SourceKey.ToUpper());
                                    // if "Source" key index is found
                                    if (k != -1)
                                    {
                                        // then find the index of "Target" key in the
                                        // list of parameters of previous parent table 
                                        // by comparing their names
                                        l = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters.GetParameterIndexByName(fkList[j].TargetKey.ToUpper());
                                        // if "Target" key is found, then assign
                                        // the value of selected key of previous parent
                                        // table (returned by output parameter) to the
                                        // selected key of parameters of current
                                        // query
                                        if (l != -1)
                                            transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                    }
                                }
                            }

                            // Build command and execute query
                            cmd = BuildSqlCommand(conn, transaction, CommandType.StoredProcedure, transactionQuery.ProcedureName, transactionQuery.SQLParameters, timeOutDuration);
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, transactionQuery.SQLParameters);
                        }

                        // All went well. Now commit the transaction and
                        // close the database connection
                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }
            public static void ExecuteNonQuery(SqlHelpers.TransactionQueryList transactionQueryList, int timeOutDuration)
            {
                SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString());
                SqlCommand cmd;
                SqlTransaction transaction;
                SqlHelpers.TransactionQuery transactionQuery;
                SqlHelpers.ForeignKeyList fkList;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < transactionQueryList.Count; i++)
                        {
                            transactionQuery = transactionQueryList[i];

                            // Set the direction of all primary keys in current transaction
                            // query
                            // Assumption: No two parameter/PK names are same
                            if (transactionQuery.PrimaryKeyList.Count > 0)
                            {
                                // for each primary keys of current query
                                for (int j = 0; j < transactionQuery.PrimaryKeyList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of primary key in the list
                                        // parameters of current query by comparing their names
                                        if (transactionQuery.PrimaryKeyList[j].KeyName.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // Set the direction of selected parameter
                                            transactionQuery.SQLParameters[k].ParameterDirection = ParameterDirection.InputOutput;
                                            // Work done for selected primary key. 
                                            // Break out of the loop.
                                            break;
                                        }
                                    }
                                }
                            }

                            // Set the value of foreign keys with the values of
                            // primary keys (returned by output parameter) of previous
                            // parent table
                            if (transactionQuery.PrimaryKeyQueryIndex != -1 && transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    // loop through all the parameters of current query
                                    for (int k = 0; k < transactionQuery.SQLParameters.Count; k++)
                                    {
                                        // to find the the index of "Source" key in the list
                                        // parameters of current query by comparing their names
                                        if (fkList[j].SourceKey.ToUpper() == transactionQuery.SQLParameters[k].ParameterName.ToUpper())
                                        {
                                            // if "Source" key index is found
                                            // then loop through all the parameters of previous
                                            // parent table 
                                            for (int l = 0; l < transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters.Count; l++)
                                            {
                                                // to find the index of "Target" key in the
                                                // list of parameters by comparing their names
                                                if (fkList[j].TargetKey.ToUpper() == transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterName.ToUpper())
                                                {
                                                    // if "Target" key is found, then assign
                                                    // the value of selected key of previous parent
                                                    // table (returned by output parameter) to the
                                                    // selected key of parameters of current
                                                    // query
                                                    transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[transactionQuery.PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                                    // Work done for selected foreign key.
                                                    // Break out of the loop
                                                    break;
                                                }
                                            }
                                            // Work done for selected foreign key.
                                            // Break out of the loop
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (transactionQuery.ForeignKeyList.Count > 0)
                            {
                                // Find the list of foreign keys of current query
                                fkList = transactionQuery.ForeignKeyList;

                                // loop for all items of foreign key
                                for (int j = 0; j < fkList.Count; j++)
                                {
                                    if (fkList[j].PrimaryKeyQueryIndex == -1)
                                        throw new System.Exception("Primary key query index is missing a value in foreign key definition.");

                                    // Find the the index of "Source" key in the list of
                                    // parameters of current query by comparing their names
                                    int k = -1;
                                    int l = -1;
                                    k = transactionQuery.SQLParameters.GetParameterIndexByName(fkList[j].SourceKey.ToUpper());
                                    // if "Source" key index is found
                                    if (k != -1)
                                    {
                                        // then find the index of "Target" key in the
                                        // list of parameters of previous parent table 
                                        // by comparing their names
                                        l = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters.GetParameterIndexByName(fkList[j].TargetKey.ToUpper());
                                        // if "Target" key is found, then assign
                                        // the value of selected key of previous parent
                                        // table (returned by output parameter) to the
                                        // selected key of parameters of current
                                        // query
                                        if (l != -1)
                                            transactionQuery.SQLParameters[k].ParameterValue = transactionQueryList[fkList[j].PrimaryKeyQueryIndex].SQLParameters[l].ParameterValue;
                                    }
                                }
                            }

                            // Build command and execute query
                            cmd = BuildSqlCommand(conn, transaction, CommandType.StoredProcedure, transactionQuery.ProcedureName, transactionQuery.SQLParameters, timeOutDuration);
                            cmd.ExecuteNonQuery();
                            StoreParameterOutputValues(cmd, transactionQuery.SQLParameters);
                        }

                        // All went well. Now commit the transaction and
                        // close the database connection
                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                }
            }

            //Added by Vipul and Yogesh For Bulk Import
            public static void SqlBulkCopyImport(DataTable dtExcel, string destabname, string[] destcol, Enums.ConnectionType conType)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString(conType)))
                {
                    // Open the connection.
                    conn.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        // Specify the destination table name.
                        bulkCopy.DestinationTableName = destabname;
                        string[] desttablecol = destcol;
                        int i = 0;
                        int j = desttablecol.Length;
                        foreach (DataColumn dc in dtExcel.Columns)
                        {
                            // Because the number of the test Excel columns is not 
                            // equal to the number of table columns, we need to map 
                            // columns.
                            if (i < j)
                            {
                                bulkCopy.ColumnMappings.Add(dc.ColumnName, desttablecol[i].ToString());
                                i++;
                            }
                            //break;

                        }

                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(dtExcel);
                    }
                }
            }
            #endregion

            //Added by Vipul and Yogesh for Getting First Row First Column only
            public static dynamic ExecuteScaler(string procedureName, SqlHelpers.ParameterList SQLParam, Enums.ConnectionType contype)
            {
                dynamic Return = "";
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString(contype)))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        Return = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam).ExecuteScalar();
                    }
                    return Return;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            public static dynamic ExecuteScaler(string procedureName, SqlHelpers.ParameterList SQLParam)
            {
                dynamic Return = "";
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionManager.ConnectionString()))
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        Return = BuildSqlCommand(conn, null, CommandType.StoredProcedure, procedureName, SQLParam).ExecuteScalar();
                    }
                    return Return;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        #endregion
    }
}
