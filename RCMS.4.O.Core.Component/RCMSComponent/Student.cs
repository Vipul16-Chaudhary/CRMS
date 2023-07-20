using RCMS._4.O.Entities.RCMSEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RCMS._4.O.Common;
using RCMS._4.O.Core.Component;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static RCMS._4.O.Core.Component.SqlHelpers;
using RCMS._4.O.Core.Componet.RCMSProcComponet;

namespace RCMS._4.O.Core.Component.RCMSComponent
{
    public class Student
    {
        public  List<StudentEntity> GetStudentDetails()
        {
            DataTableReader dr;
            StudentEntity student = new StudentEntity();
            List<StudentEntity> studentEntities = new List<StudentEntity>();
            try
            {
                dr = DatabaseHelpers.ExecuteQuery.ExecuteReader(ProcedureConstant.SP_GETALLDETAILS, Enums.ConnectionType.RCMS);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        student = new StudentEntity
                        {
                            Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]),
                            Name = Convert.ToString(dr["Name"] == DBNull.Value ? string.Empty : dr["Name"]),
                            Address = Convert.ToString(dr["Address"] == DBNull.Value ? string.Empty : dr["Address"]),
                            Email = Convert.ToString(dr["Email"] == DBNull.Value ? string.Empty : dr["Email"]),
                            Phone = Convert.ToString(dr["Phone"] == DBNull.Value ? string.Empty : dr["Phone"]),
                            PostalCode = Convert.ToString(dr["PostalCode"] == DBNull.Value ? string.Empty : dr["PostalCode"]),
                            Region = Convert.ToString(dr["Region"] == DBNull.Value ? string.Empty : dr["Region"]),
                            Country = Convert.ToString(dr["Country"] == DBNull.Value ? String.Empty : dr["Country"]),
                            City = Convert.ToString(dr["City"] == DBNull.Value ? String.Empty : dr["City"]),
                        };
                        studentEntities.Add(student);
                    }
                }
                dr.Close();
                return  studentEntities;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message.ToString());
            }
            finally
            {
                studentEntities = null;
            }
        }

        public bool AddEmployee(StudentEntity studentEntity)
        {
            SqlHelpers.ParameterList param = new SqlHelpers.ParameterList();
            param.Add(new SQLParameter("@Name", studentEntity.Name));
            param.Add(new SQLParameter("@Address", studentEntity.Address));
            param.Add(new SQLParameter("@City", studentEntity.City));
            param.Add(new SQLParameter("@Region", studentEntity.Region));
            param.Add(new SQLParameter("@PostalCode", studentEntity.PostalCode));
            param.Add(new SQLParameter("@Country", studentEntity.Country));
            param.Add(new SQLParameter("@Phone", studentEntity.Phone));
            param.Add(new SQLParameter("@Email", studentEntity.Email));
            bool result= DatabaseHelpers.ExecuteQuery.ExecuteNonQueryWithStatus(ProcedureConstant.SP_ADDEMPLOYEE, param, Enums.ConnectionType.RCMS);
            return result;
        }
    }

    
}
