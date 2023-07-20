using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Common
{
    public class CreateFileFolderHelper
    {
        public static string CreateFileFolder(string foldername, string creationtime, string folderPath)
        {
            string msg = string.Empty;
            string path = folderPath + foldername;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                msg = foldername + " created. Created on :" + DateTime.Now;
                return msg;
            }
            else
            {
                msg = foldername + " already exists in system. Created on :" + creationtime;
                return msg;
            }

        }
    }
}
