using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Express
    {
        [Key]
        public string ExpressCode { get; set; }
        public string ExpressName { get; set; }
        public string ExpressTelephone { get; set; }
        public int Sort { get; set; }
        public bool IsShow { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ReturnGoods> ReturnGoods { get; set; }

    }
}
