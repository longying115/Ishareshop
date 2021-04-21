using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using Winner.Extends.Interfaces;
using Winner.Models;

namespace Winner.Extends
{
    public class RedisHelper : IRedisHelper
    {
        private ConnectionMultiplexer _redis { get; set; }
        private IDatabase _db { get; set; }

        //public RedisHelper(IOptions<RedisSection> redisSection)
        //{
        //    var connection = redisSection.Value.Connection;//通过配置文件读取方法一
        //    var instanceName = redisSection.Value.InstanceName;
        //    var password = redisSection.Value.Password;
        //    var defaultDB = redisSection.Value.DefaultDB;

        //    var connectionString = connection+ ",defaultDatabase=" + defaultDB + ",password="+password;//通过配置文件读取方法二
        //    redis = ConnectionMultiplexer.Connect(connectionString);
        //    db = redis.GetDatabase();
        //}

        public RedisHelper(IConfiguration configuration)
        {
            var connection = configuration.GetSection("Redis:Connection").Value;
            var instanceName = configuration.GetSection("Redis:InstanceName").Value;
            var password = configuration.GetSection("Redis:Password").Value;
            var defaultDB = configuration.GetSection("Redis:DefaultDB").Value;

            var connectionString = connection + ",password=" + password;//通过配置文件读取方法一

            //var connectionString1 = configuration.GetConnectionString("RedisConnection");//通过配置文件读取方法二
            //var connectionString2 = "127.0.0.1:6379,password=123456,allowadmin=true,ConnectTimeout=65535,syncTimeout=65535";//或者这种直接写地址
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();//这里可以根据入参不同库编号0,-1等,默认是0
        }

        #region
        /// <summary>
        /// 存储字符串key-value到指定的redis数据库中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <param name="dbNumber"></param>
        /// <returns></returns>
        public async Task<bool> SetStringValueToSelectDb(string key, string value, TimeSpan? expiry = default(TimeSpan?), int dbNumber = -1)
        {
            var ret = false;
            try
            {
                IDatabase _db = _redis.GetDatabase(dbNumber);
                ret = await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }
        /// <summary>
        /// 指定数据库中获取指定key的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbNumber"></param>
        /// <returns></returns>
        public async Task<dynamic> GetValueSelectDb(string key, int dbNumber = -1)
        {
            dynamic ret = null;
            try
            {
                IDatabase _db = _redis.GetDatabase(dbNumber);
                var newobj = await _db.StringGetAsync(key, CommandFlags.None);
                ret = JsonConvert.DeserializeObject<dynamic>(newobj);
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }
        /// <summary>
        /// 指定数据库中删除指定key的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbNumber"></param>
        /// <returns></returns>
        public async Task<bool> DeleteValueSelectDb(string key, int dbNumber = -1)
        {
            var ret = false;
            try
            {
                IDatabase _db = _redis.GetDatabase(dbNumber);
                ret = await _db.KeyDeleteAsync(key);
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }
        #endregion
        #region
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        public async Task<bool> SetStringByKeyAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return await _db.StringSetAsync(key, value, expiry);
        }
        /// <summary>
        /// 获取单个key的值
        /// </summary>
        public async Task<string> GetStringByKeyAsync(string key)
        {
            var result = await _db.StringGetAsync(key);

            return result.ToString();
        }
        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <param name="obj"></param>
        public async Task<bool> SetObjectByKeyAsync<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            if (_db == null)
            {
                return false;
            }
            string json = JsonConvert.SerializeObject(obj);
            return await _db.StringSetAsync(key, json, expiry);
        }
        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        public async Task<T> GetObjectByKeyAsync<T>(string key)
        {
            if (_db == null)
            {
                return default;
            }
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// 获取或添加后获取key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> func, TimeSpan? expiry = default(TimeSpan?))
        {
            if (_db == null)
            {
                return default;
            }
            if (_db.KeyExists(key))
            {
                var value = await _db.StringGetAsync(key);
                if (value.IsNullOrEmpty)
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(value);
            }
            else
            {
                var obj = await func();
                string json = JsonConvert.SerializeObject(obj);
                var result = await _db.StringSetAsync(key, json, expiry);
                if (result)
                {
                    return obj;
                }
                else
                {
                    return default;
                }
            }
        }
        /// <summary>
        /// 删除key及对应的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveValueByKeyAsync(string key)
        {
            var ret = false;
            try
            {
                ret = await _db.KeyDeleteAsync(key);
            }
            catch (Exception)
            {

            }
            return ret;
        }
        #endregion
    }
}
