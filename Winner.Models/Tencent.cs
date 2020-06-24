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
        public int id { get; set; }
        public int sort { get; set; }
        public string tencentnum { get; set; }
        public string tencentname { get; set; }
        public bool isshow { get; set; }
    }
}
