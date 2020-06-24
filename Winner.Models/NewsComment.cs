using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Winner.Models
{
    public class NewsComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int newsid { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string contents { get; set; }
        public DateTime addtime { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual News News { get; set; }
    }
}
