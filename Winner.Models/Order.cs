using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Order
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Key]
        public string ordernum { get; set; }
        /// <summary>
        /// 购买者会员账户
        /// </summary>
        public int buyuid { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string paytime{ get; set; }
        /// <summary>
        /// 配货时间
        /// </summary>
        public string peihuotime { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public string chukutime
        {
            get;
            set;
        }
        /// <summary>
        /// 订单完成时间，确认收货时间
        /// </summary>
        public string donetime { get; set; }
        /// <summary>
        /// 订单当前完成状态,下单0，付款1，配货2，出库3，确认收货4
        /// </summary>
        private int _ordertype = 0;
        public int ordertype
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string realyname { get; set; }
        /// <summary>
        /// 订单分配区域
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 收货联系人手机
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 收货联系人电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 收货人电子邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 订单备注说明
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 订单支付类型：在线支付（0），货到付款（1）
        /// </summary>
        public int paytype { get; set; }
        /// <summary>
        /// 订单支付方式:支付宝(0)，微信(1)，银联卡(2)
        /// </summary>
        public int payclass { get; set; }
        /// <summary>
        /// 订单总商品需要支付款额
        /// </summary>
        public Decimal allmoney { get; set; }
        /// <summary>
        /// 优惠券抵扣支付款额
        /// </summary>
        public Decimal couponmoney { get; set; }
        /// <summary>
        /// 订单剩余需要支付款额
        /// </summary>
        public Decimal paymoney { get; set; }
        /// <summary>
        /// 配送方式：快递，物流
        /// </summary>
        public int sendtype { get; set; }
        /// <summary>
        /// 运输费用
        /// </summary>
        public Decimal sendmoney { get; set; }
        /// <summary>
        /// 配送时间：不限配送时间，周一至周五，双休日，节假日
        /// </summary>
        public int senddate { get; set; }
        /// <summary>
        /// 发票类型：电子发票，普通发票
        /// </summary>
        public int invoicetype { get; set; }
        /// <summary>
        /// 发票分类：个人，企业
        /// </summary>
        public int invoiceclass { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string invoicename { get; set; }
        /// <summary>
        /// 发票税号
        /// </summary>
        public string invoiceshuihao { get; set; }
        /// <summary>
        /// 收票人手机号
        /// </summary>
        public string invoicephone { get; set; }
        /// <summary>
        /// 收票人邮箱
        /// </summary>
        public string invoicemail { get; set; }
        /// <summary>
        /// 订单是否关闭
        /// </summary>
        public bool isclose { get; set; }
        public string expresstype { get; set; }
        public string expressnum { get; set; }

        public virtual Member Member { get; set; }

        public virtual Express Express { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
