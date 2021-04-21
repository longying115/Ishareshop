using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Winner.Extends.Interfaces
{
    public interface IRedisHelper
    {
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetStringByKeyAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?));
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetStringByKeyAsync(string key);
        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetObjectByKeyAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?));
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetObjectByKeyAsync<T>(string key);
        /// <summary>
        /// 获取或添加后获取key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiry = default(TimeSpan?));
        /// <summary>
        /// 删除key及对应的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> RemoveValueByKeyAsync(string key);
    }
}
