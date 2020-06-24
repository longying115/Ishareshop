using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models.ModelClass
{
    /// <summary>
    /// 会员注册实体
    /// </summary>
    public class MoRegisterMember
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号为手机号码或邮箱")]
        [Display(Prompt = "手机号/邮箱")]
        //[RegularExpression(@"(^[1][3-8]+\d{9}$)|(^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$)", ErrorMessage = "格式不正确")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string UserPwd { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围6个字符")]
        [Display(Prompt = "验证码长度范围6个字符")]
        [RegularExpression(@"[^\s]{6}", ErrorMessage = "验证码长度范围6个字符")]
        public string CheckCode { get; set; }

        public bool ReadStatus { get; set; }
    }

    /// <summary>
    /// 会员登录实体
    /// </summary>
    public class MoLoginMember
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号为手机号码或邮箱")]
        [Display(Prompt = "手机号/邮箱")]
        //[RegularExpression(@"^[1][3-8]+\d{9}$", ErrorMessage = "账号为手机号码")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string UserPwd { get; set; }
        public bool Remember { get; set; }
        /// <summary>
        /// 回跳地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// 会员保存Session信息
    /// </summary>
    public class MoMemberInfo
    {
        public int id { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string phone { get; set; }
        public string qqcode { get; set; }
        public string weibocode { get; set; }
        public string weixincode { get; set; }
        public string userpicture { get; set; }
        public bool ispass { get; set; }
        public Decimal account { get; set; }
        public Decimal allbrokerage { get; set; }
        public int xinpoint { get; set; }
        public int memberlevel { get; set; }
        public DateTime addtime { get; set; }
        public DateTime lastlogintime { get; set; }
        public string lastloginip { get; set; }
        public int sex { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public int parentid { get; set; }
        public bool status { get; set; }
    }
    /// <summary>
    /// 会员修改密码
    /// </summary>
    public class MoPassWord
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string OldPassWord { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string NewPassWord { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string ConfirmPassWord { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围5个字符")]
        [Display(Prompt = "验证码长度范围5个字符")]
        [RegularExpression(@"[^\s]{5}", ErrorMessage = "验证码长度范围5个字符")]
        public string CheckCode { get; set; }
    }
    /// <summary>
    /// 会员修改安全邮箱
    /// </summary>
    public class MoNewSafeMail
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正确的电子邮箱")]
        [Display(Prompt = "电子邮箱")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "请输入正确的电子邮箱")]
        public string NewEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围5个字符")]
        [Display(Prompt = "验证码长度范围5个字符")]
        [RegularExpression(@"[^\s]{5}", ErrorMessage = "验证码长度范围5个字符")]
        public string MailCheckCode { get; set; }
    }
    /// <summary>
    /// 安全邮箱验证码
    /// </summary>
    public class MoSafeMailCode
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正确的电子邮箱")]
        [Display(Prompt = "电子邮箱")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage = "请输入正确的电子邮箱")]
        public string NewEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围6个字符")]
        [Display(Prompt = "验证码长度范围6个字符")]
        [RegularExpression(@"[^\s]{6}", ErrorMessage = "验证码长度范围6个字符")]
        public string EmailCode { get; set; }
    }
    /// <summary>
    /// 会员修改安全手机
    /// </summary>
    public class MoNewSafePhone
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正确的手机号码")]
        [Display(Prompt = "手机号码")]
        [RegularExpression(@"^1((3|5|8){1}\d{1}|70)\d{8}$", ErrorMessage = "请输入正确的手机号码")]
        public string NewPhone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围5个字符")]
        [Display(Prompt = "验证码长度范围5个字符")]
        [RegularExpression(@"[^\s]{5}", ErrorMessage = "验证码长度范围5个字符")]
        public string PhoneCheckCode { get; set; }
    }
    /// <summary>
    /// 安全手机号验证码
    /// </summary>
    public class MoSafePhoneCode
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请输入正确的手机号码")]
        [Display(Prompt = "手机号码")]
        [RegularExpression(@"^1((3|5|8){1}\d{1}|70)\d{8}$", ErrorMessage = "请输入正确的手机号码")]
        public string NewPhone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围6个字符")]
        [Display(Prompt = "验证码长度范围6个字符")]
        [RegularExpression(@"[^\s]{6}", ErrorMessage = "验证码长度范围6个字符")]
        public string PhoneCode { get; set; }
    }
    /// <summary>
    /// 安全问题密保
    /// </summary>
    public class MoSafeQuestion
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择您的提示问题")]
        [Display(Prompt = "第一个提示问题")]
        public string Question1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "问题答案长度范围2-20个字符")]
        [Display(Prompt = "长度范围2-20个字符")]
        [RegularExpression(@"[^\s]{2,20}", ErrorMessage = "问题答案长度范围2-20个字符")]
        public string Answer1 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择您的提示问题")]
        [Display(Prompt = "第一个提示问题")]
        public string Question2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "问题答案长度范围2-20个字符")]
        [Display(Prompt = "长度范围2-20个字符")]
        [RegularExpression(@"[^\s]{2,20}", ErrorMessage = "问题答案长度范围2-20个字符")]
        public string Answer2 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择您的提示问题")]
        [Display(Prompt = "第一个提示问题")]
        public string Question3 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "问题答案长度范围2-20个字符")]
        [Display(Prompt = "长度范围2-20个字符")]
        [RegularExpression(@"[^\s]{2,20}", ErrorMessage = "问题答案长度范围2-20个字符")]
        public string Answer3 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "请选择您的提示问题")]
        [Display(Prompt = "第一个提示问题")]
        public string Question4 { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "问题答案长度范围2-20个字符")]
        [Display(Prompt = "长度范围2-20个字符")]
        [RegularExpression(@"[^\s]{2,20}", ErrorMessage = "问题答案长度范围2-20个字符")]
        public string Answer4 { get; set; }
    }
    /// <summary>
    /// 会员基本信息
    /// </summary>
    public class MoUserInfo
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "姓名长度范围2-20个字符")]
        [Display(Prompt = "长度范围2-20个字符")]
        [RegularExpression(@"[^\s]{2,20}", ErrorMessage = "姓名长度范围2-20个字符")]
        public string RealyName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "生日格式不正确")]
        [Display(Prompt = "日期格式10-20个字符")]
        [RegularExpression(@"[^\s]{10,20}", ErrorMessage = "生日格式不正确")]
        public string Birthday { get; set; }
        public int Sex { get; set; }
    }
    /// <summary>
    /// 找回密码实体
    /// </summary>
    public class MoCheckUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号为手机号码或邮箱")]
        [Display(Prompt = "手机号/邮箱")]
        //[RegularExpression(@"^[1][3-8]+\d{9}$", ErrorMessage = "账号为手机号码")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围6个字符")]
        [Display(Prompt = "验证码长度范围6个字符")]
        [RegularExpression(@"[^\s]{6}", ErrorMessage = "验证码长度范围6个字符")]
        public string CheckCode { get; set; }
    }
    /// <summary>
    /// 找回密码输入新密码
    /// </summary>
    public class MoEditPassWord
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string NewPassWord { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-16字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "密码长度范围6-16字符")]
        [RegularExpression(@"[^\s]{6,16}", ErrorMessage = "密码长度范围6-16字符")]
        public string ConfirmPassWord { get; set; }
    }
}
