using RCMS._4.O.Core.Component;
using RCMS._4.O.Core.Component.RCMSComponent;
using RCMS._4.O.Entities.RCMSEntities;
using RCMS._4.O.Interfaces.RCMSInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Repository.RCMSRepository
{
    public class RCMSRepository : IRCMSInterface
    {
        public Task<List<string>> GetAll()
        {
            List<string> list = new List<string>();
            list.Add("Yogesh");
            list.Add("Vipul");
            return Task.FromResult(list);
        }

                
        public  Task<List<StudentEntity>> GetStudentDetails()
        {
            try
            {
                return  Task.FromResult(new Student().GetStudentDetails());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new List<StudentEntity>());
            }
        }

        public Task<bool> AddEmployee(StudentEntity studentEntity)
        {
            bool result = false;
            try
            {
                return Task.FromResult(new Student().AddEmployee(studentEntity));
            }
            catch (Exception ex)
            {
                return Task.FromResult(result);
            }
        }
    }
}
