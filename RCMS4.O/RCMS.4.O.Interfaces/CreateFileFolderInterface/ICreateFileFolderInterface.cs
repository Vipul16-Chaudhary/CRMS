using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.CreateFileFolderInterface
{
    public interface ICreateFileFolderInterface
    {
        Task<string> CreateFileFolder();
    }
}
