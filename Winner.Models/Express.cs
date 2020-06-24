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
        public string expresscode { get; set; }
        public string expressname { get; set; }
        public string expresstelephone { get; set; }
        public int sort { get; set; }
        public bool isshow { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ReturnGoods> ReturnGoods { get; set; }

    }
}
