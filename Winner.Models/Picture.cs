using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int tid { get; set; }
        public int sort { get; set; }
        public string smallpicture { get; set; }
        public string picturename { get; set; }
        public string pictureremark { get; set; }
        public DateTime addtime { get; set; }
        public bool isshow { get; set; }
        public bool ishead { get; set; }

        public virtual WebColumn WebColumn { get; set; }
    }
}
