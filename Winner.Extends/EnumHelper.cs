using System;
using System.Collections.Generic;
using System.Text;

namespace Winner.Extends
{
    public class EnumHelper
    {
        /// <summary>
        /// 广告图片显示栏目
        /// </summary>
        public enum EmBanner
        {
            网站首页Banner = 0,
            商城首页Banner = 1,
            商城首页横条广告 = 2,
            商城首页快速导航 = 3,
            手机网站Banner = 4,
            手机网站热门产品 = 5,
            手机商城Banner = 6,
            手机商城热门产品 = 7,
            手机商城分类横条广告 = 8
        }
        /// <summary>
        /// 产品分类等级划分
        /// </summary>
        public enum EmProductClass
        {
            一级分类=1,
            二级分类=2,
            三级分类=3
        }
        /// <summary>
        /// 订单支付类型：在线支付，货到付款
        /// </summary>
        public enum EmPayType
        {
            在线支付 = 0,
            货到付款 = 1
        }
        /// <summary>
        /// 订单支付方式:支付宝，微信，银联卡
        /// </summary>
        public enum EmPayClass
        {
            支付宝 = 0,
            微信支付 = 1,
            网银支付 = 2
        }
        /// <summary>
        /// 订单完成状态
        /// </summary>
        public enum EmOrderType
        {
            等待付款 = 0,
            等待配货 = 1,
            等待出库 = 2,
            等待收货 = 3,
            确认收货 = 4,
            已经评价 = 5
        }
        /// <summary>
        /// 配送类型
        /// </summary>
        public enum EmSendType
        {
            快递配送 = 0,
            物流配送 = 1
        }
        /// <summary>
        /// 配送时间
        /// </summary>
        public enum EmSendDate
        {
            //a="不限送货时间：周一至周日",
            //b="工作日送货:周一至周五",
            //c="双休日、假日送货：周六至周日"
            周一至周日 = 0,
            周一至周五 = 1,
            周六至周日 = 2
        }
        /// <summary>
        /// 发票类型：电子发票，纸质发票
        /// </summary>
        public enum EmInvoiceType
        {
            电子发票 = 0,
            普通发票 = 1
        }
        /// <summary>
        /// 发票种类：个人，企业
        /// </summary>
        public enum EmInvoiceClass
        {
            个人 = 0,
            单位 = 1
        }
        public enum EmMemberLevel
        {
            普通会员 = 0,
            银牌会员 = 1,
            金牌会员 = 2
        }
        public enum EmDiscussTag
        {
            商品不错 = 0,
            质量很好 = 1,
            价格实惠 = 2,
            美观大气 = 3,
            非常喜欢 = 4,
            快递给力 = 5,
            服务到位 = 6,
            值得推荐 = 7

        }
        public enum EmReturnType
        {
            申请退货 = 0,
            申请换货 = 1
        }
        public enum EmReturnState
        {
            申请售后 = 0,
            驳回申请 = 1,
            受理业务 = 2,
            售后发货 = 3,
            收货退款 = 4
        }
    }
}
