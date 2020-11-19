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
        public string Id { get; set; }
        /// <summary>
        /// 购买者会员账户
        /// </summary>
        public int BuyerId { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime GMTPay{ get; set; }
        /// <summary>
        /// 配货时间
        /// </summary>
        public DateTime GMTPeiHuo { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime GMTChuKu
        {
            get;
            set;
        }
        /// <summary>
        /// 订单完成时间，确认收货时间
        /// </summary>
        public DateTime GMTDone { get; set; }
        /// <summary>
        /// 订单当前完成状态,下单0，付款1，配货2，出库3，确认收货4
        /// </summary>
        private int _orderType = 0;
        public int OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }

        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string RealyName { get; set; }
        /// <summary>
        /// 订单分配区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 收货联系人手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 收货联系人电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 收货人电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 订单备注说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 订单支付类型：在线支付（0），货到付款（1）
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 订单支付方式:支付宝(0)，微信(1)，银联卡(2)
        /// </summary>
        public int PayClass { get; set; }
        /// <summary>
        /// 订单总商品需要支付款额
        /// </summary>
        public Decimal AllMoney { get; set; }
        /// <summary>
        /// 优惠券抵扣支付款额
        /// </summary>
        public Decimal CouponMoney { get; set; }
        /// <summary>
        /// 订单剩余需要支付款额
        /// </summary>
        public Decimal PayMoney { get; set; }
        /// <summary>
        /// 配送方式：快递，物流
        /// </summary>
        public int SendType { get; set; }
        /// <summary>
        /// 运输费用
        /// </summary>
        public Decimal SendMoney { get; set; }
        /// <summary>
        /// 配送时间：不限配送时间，周一至周五，双休日，节假日
        /// </summary>
        public int SendDate { get; set; }
        /// <summary>
        /// 发票类型：电子发票，普通发票
        /// </summary>
        public int InvoiceType { get; set; }
        /// <summary>
        /// 发票分类：个人，企业
        /// </summary>
        public int InvoiceClass { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceName { get; set; }
        /// <summary>
        /// 发票税号
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// 收票人手机号
        /// </summary>
        public string InvoicePhone { get; set; }
        /// <summary>
        /// 收票人邮箱
        /// </summary>
        public string InvoicEmail { get; set; }
        /// <summary>
        /// 订单是否关闭
        /// </summary>
        public bool IsClose { get; set; }
        /// <summary>
        /// 快递类型
        /// </summary>
        public string ExpressType { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressCode { get; set; }

        public virtual Member Member { get; set; }

        public virtual Express Express { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
