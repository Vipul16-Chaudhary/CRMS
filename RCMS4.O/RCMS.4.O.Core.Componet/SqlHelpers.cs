using Microsoft.Data.SqlClient;
using RCMS._4.O.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using  RCMS._4.O.Common;


namespace RCMS._4.O.Core.Componet
{
    /// <summary>
    /// Author: Yogesh
    /// Guide By: Vipul
    /// Creation Date: 21-06-2023
    /// Description: Common SQL Fuctions
    /// </summary>
    public class SqlHelpers
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public class SQLParameter
        {
            string _parameterName = "";
            object _parameterValue = null;
            ParameterDirection _direction = ParameterDirection.Input;
            SqlDbType? _dbType = null;
            int _size = -1;
            private static Enums.ConnectionType _conType = Enums.ConnectionType.RCMS;

            public SQLParameter()
            {

            }


            public SQLParameter(string parameterName, object parameterValue)
            {
                _parameterName = parameterName;
                _parameterValue = parameterValue;
            }

            public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType)
            {
                _parameterName = parameterName;
                _parameterValue = parameterValue;
                _dbType = dbType;
            }

            public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType, int size)
            {
                _parameterName = parameterName;
                _parameterValue = parameterValue;
                _dbType = dbType;
                _size = size;
            }

            public SQLParameter(string parameterName, object parameterValue, ParameterDirection paramDirection)
            {
                _parameterName = parameterName;
                _parameterValue = parameterValue;
                _direction = paramDirection;
            }

            public SQLParameter(string parameterName, object parameterValue, SqlDbType dbType, ParameterDirection paramDirection)
            {
                _parameterName = parameterName;
                _parameterValue = parameterValue;
                _dbType = dbType;
                _direction = paramDirection;
            }

            public string ParameterName
            {
                get { return _parameterName; }
                set { _parameterName = value; }
            }

            public object ParameterValue
            {
                get { return _parameterValue; }
                set { _parameterValue = value; }
            }

            public SqlDbType? DBType
            {
                get { return _dbType; }
                set { _dbType = value; }
            }

            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }

            public ParameterDirection ParameterDirection
            {
                get { return _direction; }
                set { _direction = value; }
            }

            public static Enums.ConnectionType ConType
            {
                get { return _conType; }
                set { _conType = value; }
            }

            public Enums.ConnectionType ConnectionType
            {
                get { return SQLParameter.ConType; }
            }

        }

        public class ParameterList : List<SQLParameter>
        {
            public ParameterList()
            {

            }

            public SQLParameter GetParameterByName(string parameterName)
            {
                return this.Find(delegate (SQLParameter p) { return p.ParameterName.ToUpper() == parameterName.ToUpper(); });
            }

            public int GetParameterIndexByName(string parameterName)
            {
                return this.FindIndex(delegate (SQLParameter p) { return p.ParameterName.ToUpper() == parameterName.ToUpper(); });
            }
        }

        /// <summary>
        /// Encapsulates System.Data.SqlClient.SqlParameter
        /// to create a generic list. Use this to pass a
        /// list of SqlParameters to various static functions 
        /// of ExecuteQuery class.
        /// Now it is obsolete.
        /// </summary>
        //public class SQLParameterList : List<SqlParameter>
        //{
        //    public SQLParameterList()
        //    {

        //    }
        //}

        public class Key
        {
            string _key;

            public Key()
            {

            }

            public Key(string keyName)
            {
                _key = keyName;
            }

            public string KeyName
            {
                get { return _key; }
                set { _key = value; }
            }
        }

        public class ForeignKey
        {
            Key _sourceKey = new Key();
            Key _targetKey = new Key();
            int _primaryKeyQueryIndex = -1;

            public ForeignKey()
            {

            }

            public ForeignKey(string sourceKey, string targetKey)
            {
                _sourceKey.KeyName = sourceKey;
                _targetKey.KeyName = targetKey;
            }

            public ForeignKey(string sourceKey, string targetKey, int primaryKeyQueryIndex)
            {
                _sourceKey.KeyName = sourceKey;
                _targetKey.KeyName = targetKey;
                _primaryKeyQueryIndex = primaryKeyQueryIndex;
            }

            public string SourceKey
            {
                get { return _sourceKey.KeyName; }
                set { _sourceKey.KeyName = value; }
            }

            public string TargetKey
            {
                get { return _targetKey.KeyName; }
                set { _targetKey.KeyName = value; }
            }

            public int PrimaryKeyQueryIndex
            {
                get { return _primaryKeyQueryIndex; }
                set { _primaryKeyQueryIndex = value; }
            }
        }

        public class ForeignKeyList : List<ForeignKey>
        {
            public ForeignKeyList()
            {

            }
        }

        public class PrimaryKeyList : List<Key>
        {
            public PrimaryKeyList()
            {

            }
        }

        public class TransactionQuery
        {
            string _procedureName;
            ParameterList _parameterList = new ParameterList();
            PrimaryKeyList _primaryKeyList = new PrimaryKeyList();
            ForeignKeyList _foreignKeyList = new ForeignKeyList();
            int _primaryKeyQueryIndex = -1;

            public TransactionQuery()
            {

            }

            public string ProcedureName
            {
                get { return _procedureName; }
                set { _procedureName = value; }
            }

            public ParameterList SQLParameters
            {
                get { return _parameterList; }
                set { _parameterList = value; }
            }

            public PrimaryKeyList PrimaryKeyList
            {
                get { return _primaryKeyList; }
                set { _primaryKeyList = value; }
            }

            public ForeignKeyList ForeignKeyList
            {
                get { return _foreignKeyList; }
                set { _foreignKeyList = value; }
            }

            public int PrimaryKeyQueryIndex
            {
                get { return _primaryKeyQueryIndex; }
                set { _primaryKeyQueryIndex = value; }
            }
        }

        public class TransactionQueryList : List<TransactionQuery>
        {
            public TransactionQueryList()
            {

            }
        }
    }

    
}

