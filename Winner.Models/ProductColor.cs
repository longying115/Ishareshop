using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductColor
    {
        public ProductColor()
        {
            ProductPicture = new HashSet<ProductPicture>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int sort { get; set; }
        public int productid { get; set; }
        public string colorname { get; set; }
        public string colorpicture { get; set; }
        public int counts { get; set; }
        public float price { get; set; }

        public virtual Products Products { get; set; }
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
    }
}
