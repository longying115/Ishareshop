using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ReturnGoods
    {
        [Key]
        public string ReturnCode { get; set; }
        public int MemberId { get; set; }
        public string OrderCode { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int ColorId { get; set; }
        public string ProductColor { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int ReturnType { get; set; }
        public string Remark { get; set; }

        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public int ReturnState { get; set; }
        /// <summary>
        /// 退货邮寄时间
        /// </summary>
        public DateTime GMTReturn { get; set; }
        /// <summary>
        /// 快递类型
        /// </summary>
        public string ExpressType { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressCode { get; set; }
        public string Area { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneePhone { get; set; }
        public string ConsigneeTelephone { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeEmail { get; set; }
        public DateTime GMTChuKu { get; set; }
        public DateTime GMTDone { get; set; }

        public virtual Member Member { get; set; }

        public virtual Express Express { get; set; }
        public virtual ICollection<ReturnPicture> ReturnPicture { get; set; }
    }
}
