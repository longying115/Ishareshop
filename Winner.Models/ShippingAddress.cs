using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ShippingAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int uid { get; set; }
        public string realyname { get; set; }
        public string area { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string telephone { get; set; }
        public string post { get; set; }
        public string email { get; set; }
        public string othername { get; set; }
        public DateTime addtime { get; set; }
        public bool isdefault { get; set; }

        public virtual Member Member { get; set; }
    }
}
