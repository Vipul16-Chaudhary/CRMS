using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.RadisCacheInterface
{
    /// <summary>
    /// Author: Yogesh
    /// Creation Date: 05-07-2023
    /// Description: Radis cache interface for method declaration only
    /// </summary>
    public interface IRadisCacheInterface
    {
        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetData<T>(string key);

        /// <summary>
        /// Set Data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);

        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object RemoveData(string key);
    }
}
