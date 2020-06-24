using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class CashValueLog
    {
        [Key]
        public int uid { get; set; }
        public Decimal money { get; set; }
        public string remark { get; set; }
        public DateTime addtime { get; set; }

        public virtual Member Member { get; set; }
    }
}
