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
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSmallTitle { get; set; }
        public decimal ProductPrice { get; set; }
        private DateTime _createTime = DateTime.Now;
        public DateTime GMTCreate
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        public virtual Member Member { get; set; }
        public virtual Products Products { get; set; }
    }
}
