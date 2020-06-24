using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public string contactname { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public string tencent { get; set; }
        public bool isshow { get; set; }
    }
}
