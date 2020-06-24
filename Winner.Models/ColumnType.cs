using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ColumnType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string typename { get; set; }
        public string typelink { get; set; }
        public string opentype { get; set; }
        public bool typestate { get; set; }

        public virtual ICollection<WebColumn> WebColumn { get; set; }
    }
}
