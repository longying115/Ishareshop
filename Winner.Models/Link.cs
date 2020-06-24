using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Link
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public string linkname { get; set; }
        public string linkpic { get; set; }
        public string linkurl { get; set; }
        public bool ispicture { get; set; }
        public bool isshow { get; set; }
    }
}
