using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.ConfigurationManager
{
    public class RadisConnectionManager
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static RadisConnectionManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}
