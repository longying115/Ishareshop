using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class GiftClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int pid { get; set; }
        public int sort { get; set; }
        public string classname { get; set; }
        public string classremark { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string keydescription { get; set; }
        public string smallpicture { get; set; }
        public bool isshow { get; set; }

        public virtual ICollection<Gifts> Gifts { get; set; }
    }
}
