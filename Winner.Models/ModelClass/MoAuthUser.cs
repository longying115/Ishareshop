using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models.ModelClass
{
    public class MoAuthUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号长度范围3-30字符")]
        [Display(Prompt = "角色账户/3-30字符")]
        [RegularExpression(@"[^\s]{3,30}", ErrorMessage = "账号长度范围3-30字符")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码长度范围6-20字符")]
        [DataType(DataType.Password)]
        [Display(Prompt = "角色密码6-20字符")]
        [RegularExpression(@"[^\s]{6,20}", ErrorMessage = "密码长度范围6-20字符")]
        public string PassWord { get; set; }
    }
}
