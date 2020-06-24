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
        public int orderid { get; set; }
        public string ordernum { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int productid { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string productname { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string producttype { get; set; }
        /// <summary>
        /// 选择的产品颜色id
        /// </summary>
        public int colorid { get; set; }
        /// <summary>
        /// 选择的产品颜色
        /// </summary>
        public string productcolor { get; set; }
        /// <summary>
        /// 选择的产品的单价
        /// </summary>
        public Decimal productprice { get; set; }
        /// <summary>
        /// 选择的产品的购买数量
        /// </summary>
        public int productcount { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public bool iscomment { get; set; }
        public int commentid { get; set; }

        public virtual Order Order { get; set; }
    }
}
