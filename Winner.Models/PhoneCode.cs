using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class PhoneCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string phone { get; set; }
        public DateTime addtime { get; set; }
        public string machineip { get; set; }
        public string codenum { get; set; }
    }
}
