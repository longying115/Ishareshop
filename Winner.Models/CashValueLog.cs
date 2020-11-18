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
        public int Uid { get; set; }
        public Decimal Money { get; set; }
        public string Remark { get; set; }
        public DateTime Addtime { get; set; }

        public virtual Member Member { get; set; }
    }
}
