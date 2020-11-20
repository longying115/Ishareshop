using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="账号不能为空")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="密码不能为空")]
        public string Password { get; set; }
        public string PassString { get; set; }
        public string NickName { get; set; }
        public string QQCode { get; set; }
        public string WeChatCode { get; set; }
        public string WeiboCode { get; set; }
        public string UserPicture { get; set; }
        public bool IsPass { get; set; }
        [Required(ErrorMessage ="账户余额不能为空")]
        public Decimal Account { get; set; }
        /// <summary>
        /// 佣金、回扣
        /// </summary>
        [Required(ErrorMessage ="佣金必须大于等于零")]
        public Decimal AllBrokerage { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public int MemberLevel { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        private DateTime _lastLoginTime = DateTime.Now;
        public DateTime GMTLastLogin
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }
        public string LastLoginIp { get; set; }
        private int _sex = 0;
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public string Birthday { get; set; }
        [Phone(ErrorMessage ="手机号码格式不正确")]
        [RegularExpression(@"^1[234689]/d{9}*$", ErrorMessage = "")]
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        [EmailAddress(ErrorMessage ="邮箱格式不正确")]
        public string Email { get; set; }
        public string Post { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public int ParentId { get; set; }
        public bool Status { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }

        public virtual ICollection<GetPointLog> GetPointLog { get; set; }
        public virtual ICollection<GetMoneyLog> GetMoneyLog { get; set; }
        public virtual ICollection<CashFlowLog> CashFlowLog { get; set; }
        public virtual ICollection<MemberLog> MemberLog { get; set; }

        public virtual ICollection<Favorites> Favorites { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        public virtual ICollection<ShopCart> ShopCart { get; set; }

        public virtual ICollection<ProductDiscuss> ProductDiscuss { get; set; }

        public virtual ICollection<ReturnGoods> ReturnGoods { get; set; }
    }
}
