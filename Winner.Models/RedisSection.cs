using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Models
{
    public class RedisSection
    {
        /// <summary>
        /// 连接IP地址和接口
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InstanceName { get; set; }
        /// <summary>
        /// 默认数据库
        /// </summary>
        public string DefaultDB { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
