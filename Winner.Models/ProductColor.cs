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
        public int Id { get; set; }
        public int Sort { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Counts { get; set; }
        public decimal Price { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public int CreateAdminId { get; set; }
        private DateTime _modifiedTime = DateTime.Now;
        public DateTime GMTModified
        {
            get { return _modifiedTime; }
            set { _modifiedTime = value; }
        }
        public int ModifiedAdminId { get; set; }
        public string ModifiedIp { get; set; }

        public virtual Products Products { get; set; }
        public virtual ICollection<ProductPicture> ProductPicture { get; set; }
    }
}
