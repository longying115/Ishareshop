using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int pid { get; set; }
        public int classlevel { get; set; }
        public int sort { get; set; }
        public string classname { get; set; }
        public string classremark { get; set; }
        public string keytitle { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string smallpicture { get; set; }
        public bool isshow { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
