using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class OnlyText
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public string textcontent { get; set; }
        public int hits { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
