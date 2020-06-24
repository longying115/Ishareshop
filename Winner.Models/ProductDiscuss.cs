using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Winner.Models
{
    public class ProductDiscuss
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int productid { get; set; }
        public string productcolor { get; set; }
        public decimal productprice { get; set; }
        public int memberid { get; set; }
        public string discuss { get; set; }
        public bool tag1 { get; set; }
        public bool tag2 { get; set; }
        public bool tag3 { get; set; }
        public bool tag4 { get; set; }
        public bool tag5 { get; set; }
        public bool tag6 { get; set; }
        public bool tag7 { get; set; }
        public bool tag8 { get; set; }
        public int score { get; set; }
        private DateTime _addtime = DateTime.Now;
        public DateTime addtime {
            get { return _addtime; }
            set { _addtime = value; }
        }
        public bool isreply { get; set; }
        public int hits { get; set; }
        public string replycontent { get; set; }
        public bool isshow { get; set; }

        public virtual Products Products { get; set; }
        public virtual Member Member { get; set; }
    }
}
