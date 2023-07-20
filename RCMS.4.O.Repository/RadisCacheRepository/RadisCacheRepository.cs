using Newtonsoft.Json;
using RCMS._4.O.Core.Component;
using RCMS._4.O.Interfaces.RadisCacheInterface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Repository.RadisCacheRepository
{
    /// <summary>
    /// Author: Yogesh
    /// Creation Date: 05-07-2023
    /// Description: Radis cache interface for method implementation
    /// </summary>
    public class RadisCacheRepository : IRadisCacheInterface
    {
        private IDatabase _db;
        public RadisCacheRepository()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = RadisConnectionHelper.Connection.GetDatabase();
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        /// <summary>
        /// Set Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return Task.FromResult(isSet);
        }

        /// <summary>
        /// Remove data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
