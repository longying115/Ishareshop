using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Down
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public string title { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydesciption { get; set; }
        public string textcontent { get; set; }
        public string filename { get; set; }
        public string source { get; set; }
        public int hits { get; set; }
        public DateTime addtime { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
