using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public int sort { get; set; }
        public string title { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public DateTime jobbegindate { get; set; }
        public DateTime jobenddate { get; set; }
        public string jobcount { get; set; }
        public string jobmoney { get; set; }
        public string jobcondition { get; set; }
        public string jobduty { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
