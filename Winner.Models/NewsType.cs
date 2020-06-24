using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class NewsType
    {
        public NewsType()
        {
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public string typename { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
