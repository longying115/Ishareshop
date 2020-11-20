using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class MemberLog
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 使用类型，枚举
        /// </summary>
        public int UseType { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ips { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }

        public virtual Member Member { get; set; }
    }
}
