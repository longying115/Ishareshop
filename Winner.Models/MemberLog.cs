using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class MemberLog
    {
        [Key]
        public int memberid { get; set; }
        public int usetype { get; set; }
        public string ips { get; set; }
        public DateTime addtime { get; set; }
        public string remark { get; set; }

        public virtual Member Member { get; set; }
    }
}
