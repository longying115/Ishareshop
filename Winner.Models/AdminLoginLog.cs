using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class AdminLoginLog
    {
        [Key]
        public int adminid { get; set; }
        public string ips { get; set; }
        public DateTime addtime { get; set; }
        public string remark { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
