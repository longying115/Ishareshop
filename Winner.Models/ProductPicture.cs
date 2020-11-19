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
        public int Id { get; set; }
        public int Sort { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public string Picture { get; set; }

        public virtual Products Products { get; set; }
        public virtual ProductColor ProductColor { get; set; }
    }
}
