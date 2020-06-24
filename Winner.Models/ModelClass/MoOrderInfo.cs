using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models.ModelClass
{
    /// <summary>
    /// 前端订单添加信息
    /// </summary>
    public class MoOrder
    {
        /// <summary>
        /// 送货地址
        /// </summary>
        public int addressid { get; set; }
        /// <summary>
        /// 订单支付类型：在线支付，货到付款
        /// </summary>
        public int paytype { get; set; }
        /// <summary>
        /// 订单支付方式:支付宝(0)，微信(1)，银联卡(2)
        /// </summary>
        public int payclass { get; set; }
        /// <summary>
        /// 配送方式：快递，物流
        /// </summary>
        public int sendtype { get; set; }
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
        /// 优惠券编号
        /// </summary>
        public string couponnum { get; set; }
    }
    /// <summary>
    /// 前端购物车添加信息
    /// </summary>
    public class MoOrderInfo
    {
        public int productid { get; set; }
        public int productcolorid { get; set; }
        public int productcounts { get; set; }
        public string area { get; set; }
    }
    public class MoShopCart
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public string smallpicture { get; set; }
        public decimal price { get; set; }
        public int productcounts { get; set; }
        public int colorid { get; set; }
        public string colorname { get; set; }
        public int colorcount { get; set; }
        public string area { get; set; }
    }
    public class MoPayReture
    {
        public string ordernum { get; set; }
        public string ordermessage { get; set; }
        public bool issuccess { get; set; }
    }
    public class MoOrderAddress
    {
        public string ordernum { get; set; }
        public string realyname { get; set; }
        public string phone { get; set; }
        public string area { get; set; }
        public string address { get; set; }
        public string remark { get; set; }
    }
    public class MoSend
    {
        public string ordernum { get; set; }
        public int sendtype { get; set; }
        public int senddate { get; set; }
    }
}
