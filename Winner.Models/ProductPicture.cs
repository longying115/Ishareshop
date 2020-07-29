using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductPicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public int colorid { get; set; }
        public int productid { get; set; }
        public string picture { get; set; }
        public virtual Products Products { get; set; }
        public virtual ProductColor ProductColor { get; set; }
    }
}
