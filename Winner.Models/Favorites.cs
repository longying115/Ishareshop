using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class Favorites
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int uid { get; set; }
        public int productid { get; set; }
        public string producttitle { get; set; }
        public string productsmalltitle { get; set; }
        public decimal productprice { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime {
            get { return _addtime; }
            set { _addtime = value; }
        }

        public virtual Member Member { get; set; }
        public virtual Products Products { get; set; }
    }
}
