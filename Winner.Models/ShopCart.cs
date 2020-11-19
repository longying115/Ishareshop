using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ShopCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        public string Area { get; set; }

        public virtual Member Member { get; set; }
    }
}
