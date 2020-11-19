using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ColumnType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 类型链接
        /// </summary>
        public string TypeLink { get; set; }
        /// <summary>
        /// 打开方式,新窗口打开，本窗口打开，父窗口打开
        /// </summary>
        public string OpenWay { get; set; }
        /// <summary>
        /// 类型状态，是否启用
        /// </summary>
        public bool TypeState { get; set; }

        public virtual ICollection<WebColumn> WebColumn { get; set; }
    }
}
