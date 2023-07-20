using Microsoft.Extensions.Configuration;
using RCMS._4.O.Common;
using System;

namespace RCMS._4.O.ConfigurationManager
{
    /// <summary>
    /// Author: Yogesh
    /// Guide By: Vipul
    /// Creation Date: 21-06-2023
    /// Description: All Database Connection Strings
    /// </summary>
    public static class ConnectionManager
    {
        private static IConfiguration _configuration;
        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ConnectionString(Enums.ConnectionType contype)
        {
            string connectionString = _configuration["ConnectionStrings:RCMSConnection"];
            try
            {
                switch (contype)
                {
                    case Enums.ConnectionType.RCMS:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.Namantran:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.WebGIS:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.GIS:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.Batwara:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.Bhoolekh:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.Land:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    case Enums.ConnectionType.Simankan:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];
                        break;
                    default:
                        connectionString = _configuration["ConnectionStrings:RCMSConnection"];

                        break;




                }
            }
            catch (Exception)
            {

            }
            return connectionString;
        }
        public static string ConnectionString()
        {
            string connectionString = _configuration["ConnectionStrings:RCMSConnection"];

            return connectionString;
        }
    }
}
