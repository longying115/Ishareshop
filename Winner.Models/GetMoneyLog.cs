using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class GetMoneyLog
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int MemberId { get; set; }
        /// <summary>
        /// 获得金额
        /// </summary>
        public Decimal Money { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 忘记干嘛得了，用到时再说
        /// </summary>
        public int Mid { get; set; }
        /// <summary>
        /// 全部金额
        /// </summary>
        public Decimal AllMoney { get; set; }

        public virtual Member Member { get; set; }
    }
}
