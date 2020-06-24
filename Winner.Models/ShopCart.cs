using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ShopCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int buyuid { get; set; }
        public int productid { get; set; }
        public string productname { get; set; }
        public string productcolor { get; set; }
        public decimal productprice { get; set; }
        public int productcount { get; set; }
        public DateTime addtime { get; set; }
        public string area { get; set; }

        public virtual Member Member { get; set; }
    }
}
