using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class OrderItem
    {
        [Key]
        public string Id { get; set; }
        public string OrderCode { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 选择的产品颜色id
        /// </summary>
        public int ColorId { get; set; }
        /// <summary>
        /// 选择的产品颜色
        /// </summary>
        public string ProductColor { get; set; }
        /// <summary>
        /// 选择的产品的单价
        /// </summary>
        public Decimal ProductPrice { get; set; }
        /// <summary>
        /// 选择的产品的购买数量
        /// </summary>
        public int ProductCount { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        public virtual Order Order { get; set; }
    }
}
