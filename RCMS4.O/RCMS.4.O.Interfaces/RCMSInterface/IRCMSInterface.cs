using RCMS._4.O.Entities.RCMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.RCMSInterface
{
    public interface IRCMSInterface
    {
        Task<List<string>> GetAll();
        Task<List<StudentEntity>> GetStudentDetails();
        Task<bool> AddEmployee(StudentEntity studentEntity);

    }
}
