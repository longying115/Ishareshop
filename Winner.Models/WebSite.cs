using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class WebSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 简要描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 座机电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQNumber { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// ICP备案号
        /// </summary>
        public string Icp { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
