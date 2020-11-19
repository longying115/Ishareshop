using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Tencent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Sort { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string QQNumber { get; set; }
        /// <summary>
        /// 网站显示称呼
        /// </summary>
        public string QQName { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
