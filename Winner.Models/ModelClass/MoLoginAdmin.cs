using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models.ModelClass
{
    /// <summary>
    /// 总管理系统登录实体
    /// </summary>
    public class MoLoginAdmin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号长度范围3-30字符")]
        [Display(Prompt = "管理账户/3-30字符")]
        [RegularExpression(@"[^\s]{3,30}", ErrorMessage = "账号长度范围3-30字符")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-20字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "管理密码6-20字符")]
        [RegularExpression(@"[^\s]{6,20}", ErrorMessage = "密码长度范围6-20字符")]
        public string UserPwd { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码长度范围4个字符")]
        [Display(Prompt = "验证码4个字符")]
        [RegularExpression(@"[^\s]{4}", ErrorMessage = "验证码长度范围4个字符")]
        public string CheckCode { get; set; }
        /// <summary>
        /// 回跳地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }

    /// <summary>
    /// 总管理系统信息
    /// </summary>
    public class MoAdminInfo
    {
        public int id { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string telephone { get; set; }
        public string qqnumber { get; set; }
        public string weixin { get; set; }
        public string lastloginip { get; set; }
        public DateTime lastlogintime { get; set; }
        public int power { get; set; }
        public string flag { get; set; }
        public bool isadd { get; set; }
        public bool isdel { get; set; }
        public bool isedit { get; set; }
        public bool status { get; set; }
    }
}
